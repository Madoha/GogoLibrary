import { useState } from 'react';
import { useNavigate, NavLink } from 'react-router-dom';
import axios from '../../api/axiosConfig';
import styles from './Login.module.css';

const Login = () => {
    const navigate = useNavigate();
    const [credentials, setCredentials] = useState({ username: '', password: '' });
    const [error, setError] = useState('');

    const addTokenToHeaders = (token) => {
        if (token) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        } else {
            delete axios.defaults.headers.common['Authorization'];
        }
    };

    const handleLogin = async (credentials) => {
        try {
            const response = await axios.post('http://localhost:8080/api/auth/login/', credentials);
            console.log(response);
    
            const { accessToken } = response.data.data; // Extract userId from the response
            console.log(accessToken);
    
            if (accessToken) {
                addTokenToHeaders(accessToken);
                return { token: accessToken }; // Return both token and userId
            } else {
                throw new Error('Token not received');
            }
        } catch (error) {
            console.error('Login error', error);
            throw error;
        }
    };
    

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCredentials({ ...credentials, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            // Assuming handleLogin returns an object with both token and userId
            const { token } = await handleLogin(credentials);
    
            console.log('Login successful', token);
    
            // Store the token and userId in local storage
            localStorage.setItem('authToken', token);
    
            // Navigate to the profile page with userId
            navigate(`/profile`, { replace: true });
        } catch (error) {
            setError('Login failed. Please check your credentials.');
            console.error('Login error', error);
        }
    };
    

    return (
        <div className={styles.login}>
            <div className={styles.wrapper}>
                <form onSubmit={handleSubmit}>
                    <h1>Login</h1>
                    <div className={styles['input-box']}>
                        <input type="text" name="username" placeholder="Username" value={credentials.username} onChange={handleChange} />
                    </div>
                    <div className={styles['input-box']}>
                        <input type="password" name="password" placeholder="Password" value={credentials.password} onChange={handleChange} />
                    </div>
                    <button type="submit">Login</button>
                    {error && <p>{error}</p>}
                    <div className={styles['register-link']}>
                        <p>Do not have an account? <NavLink to="/register">Register</NavLink></p>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Login;
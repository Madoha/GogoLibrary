import { useState } from 'react';
import { useNavigate, NavLink } from 'react-router-dom';
import axios from '../../api/axiosConfig';
import styles from './Register.module.css';

export const Register = () => {
    const navigate = useNavigate();
    const [user, setUser] = useState({ username: "", email: "", firstName: "", lastName: "", phoneNumber: "", password: "", confirmPassword: "" });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setUser({ ...user, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        console.log('Submitting user data:', user);
        try {
            const response = await axios.post('http://localhost:8080/api/auth/register/', user);
            alert(response.data.message);
            navigate('/login', { replace: true });
        } catch (error) {
            console.error('Registration error', error);
        }
    };

    return (
        <div className={styles.register}>
            <div className={styles.wrapper}>
                <form onSubmit={handleSubmit}>
                    <h1>Register</h1>
                    <div className={styles['input-box']}>
                        <input type="text" name="username" placeholder="username" value={user.username} onChange={handleChange} />
                    </div>
                    <div className={styles['input-box']}>
                        <input type="email" name="email" placeholder="email" value={user.email} onChange={handleChange} />
                    </div>
                    <div className={styles['input-box']}>
                        <input type="text" name="firstName" placeholder="firstName" value={user.firstName} onChange={handleChange} />
                    </div>
                    <div className={styles['input-box']}>
                        <input type="text" name="lastName" placeholder="lastName" value={user.lastName} onChange={handleChange} />
                    </div>
                    <div className={styles['input-box']}>
                        <input type="text" name="phoneNumber" placeholder="phoneNumber" value={user.phoneNumber} onChange={handleChange} />
                    </div>
                    <div className={styles['input-box']}>
                        <input type="password" name="password" placeholder="Password" value={user.password} onChange={handleChange} />
                    </div>
                    <div className={styles['input-box']}>
                        <input type="password" name="confirmPassword" placeholder="Password Confirm" value={user.confirmPassword} onChange={handleChange} />
                    </div>
                    <button type="submit">Register</button>
                    <div className={styles['login-link']}>
                        <p>Already have an account? <NavLink to="/login">Login</NavLink></p>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Register;

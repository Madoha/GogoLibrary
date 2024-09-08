import { useState, useEffect } from 'react';
import axios from '../../api/axiosConfig';
import styles from './Profile.module.css';
import gogol from '../../../assets/gogol.png'

const Profile = () => {
  const [userInfo, setUserInfo] = useState(null);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchUserProfile = async () => {
      try {
        const token = localStorage.getItem('authToken');
        if (!token) {
          setError('No token found. Please log in.');
          return;
        }

        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        const response = await axios.get('http://localhost:8080/api/user/get-profile');
        setUserInfo(response.data.data);
      } catch (error) {
        console.error('Error fetching user profile:', error);
        setError('Failed to fetch user profile.');
      }
    };

    fetchUserProfile();
  }, []);

  return (
    <div className={styles.profileContainer}>
      {error && <p className={styles.errorMessage}>{error}</p>}
      {userInfo ? (
        <div className={styles.profileContent}>
          <h1 className={styles.profileTitle}>Профиль</h1>
          <div className={styles.profileTabs}>
            <span className={styles.activeTab}>Личная информация</span>
            <span className={styles.inactiveTab}>Моя книжная полка</span>
          </div>
          <div className={styles.profileInfo}>
            <img
              src={gogol}
              alt="User Avatar"
              className={styles.profileAvatar}
            />
            <div className={styles.profileDetails}>
              <h2>{userInfo.userName}</h2>
              <p className={styles.profileEmail}>{userInfo.email}</p>
              <button className={styles.editProfileButton}>редактировать профиль</button>
            </div>
          </div>
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
};

export default Profile;

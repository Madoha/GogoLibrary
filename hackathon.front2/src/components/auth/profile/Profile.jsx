import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from '../../api/axiosConfig';
import styles from './Profile.module.css';
import gogol from '../../../assets/gogol.png';

const Profile = () => {
  const [userInfo, setUserInfo] = useState(null);
  const [error, setError] = useState('');
  const [books, setBooks] = useState([]);
  const [activeTab, setActiveTab] = useState('profile'); // 'profile' or 'books'
  const navigate = useNavigate();

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

  const fetchFavoriteBooks = async () => {
    try {
      const token = localStorage.getItem('authToken');
      if (!token) {
        setError('No token found. Please log in.');
        return;
      }

      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      const response = await axios.get('http://localhost:8080/api/user/get-favorites');
      setBooks(response.data.data);
    } catch (error) {
      console.error('Error fetching favorite books:', error);
      setError('Failed to fetch favorite books.');
    }
  };

  const handleTabClick = (tab) => {
    setActiveTab(tab);
    if (tab === 'books') {
      fetchFavoriteBooks();
    }
  };

  return (
    <div className={styles.profileContainer}>
      {error && <p className={styles.errorMessage}>{error}</p>}
      {userInfo ? (
        <div className={styles.profileContent}>
          <h1 className={styles.profileTitle}>Профиль</h1>
          <div className={styles.profileTabs}>
            <span
              className={activeTab === 'profile' ? styles.activeTab : styles.inactiveTab}
              onClick={() => handleTabClick('profile')}
            >
              Личная информация
            </span>
            <span
              className={activeTab === 'books' ? styles.activeTab : styles.inactiveTab}
              onClick={() => handleTabClick('books')}
            >
              Моя книжная полка
            </span>
          </div>
          {activeTab === 'profile' && (
            <div className={styles.profileInfo}>
              <img src={gogol} alt="User Avatar" className={styles.profileAvatar} />
              <div className={styles.profileDetails}>
                <h2>{userInfo.userName}</h2>
                <p className={styles.profileEmail}>{userInfo.email}</p>
                <button className={styles.editProfileButton}>редактировать профиль</button>
              </div>
            </div>
          )}
          {activeTab === 'books' && (
            <div className={styles.booksList}>
              <h3 className='booksss'>Моя книжная полка</h3>
              {books.length > 0 ? (
                <ul className={styles.prof}>
                  {books.map((book) => (
                    <li className={styles.profl} key={book.id}>{book.bookTitle} написана {book.bookAuthor}. {book.yearOfPublication} года выпуска</li>
                  ))}
                </ul>
              ) : (
                <p>No books found.</p>
              )}
            </div>
          )}
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
};

export default Profile;

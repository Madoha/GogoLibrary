import { useState } from 'react';
import axios from '../../api/axiosConfig';
import styles from './AddBook.module.css';

const AddBookModal = ({ isOpen, onClose }) => {
  const [bookDetails, setBookDetails] = useState({
    isbn: '',
    title: '',
    author: '',
    yearOfPublication: '',
    description: '',
    publisher: '',
    imageUrl: '',
    link: '',
  });

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setBookDetails((prevDetails) => ({
      ...prevDetails,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post('http://localhost:8080/api/Book/add-book', bookDetails);
      alert('Book added successfully!');
      onClose(); // Close modal on success
    } catch (error) {
      console.error('Error adding book:', error);
      alert('Failed to add book.');
    }
  };

  if (!isOpen) return null; // Render nothing if modal is not open

  return (
    <div className={styles.modalOverlay}>
      <div className={styles.modalContent}>
        <button className={styles.closeButton} onClick={onClose}>✖</button>
        <h2>Добавить книгу</h2>
        <form onSubmit={handleSubmit} className={styles.form}>
          {Object.keys(bookDetails).map((key) => (
            <div className={styles.formGroup} key={key}>
              <label htmlFor={key}>{key}</label>
              <input
                type="text"
                className={styles.inputstic}
                id={key}
                name={key}
                value={bookDetails[key]}
                onChange={handleInputChange}
              />
            </div>
          ))}
          <button type="submit" className={styles.submitButton}>
            Добавить книгу
          </button>
        </form>
      </div>
    </div>
  );
};

export default AddBookModal;

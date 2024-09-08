import { useEffect, useState } from 'react';
import axios from 'axios';
import Modal from 'react-modal';
import Club from './Club';

Modal.setAppElement('#root');

const ClubsList = () => {
  const [clubs, setClubs] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [clubName, setClubName] = useState('');
  const [bookTitle, setBookTitle] = useState('');
  const [description, setDescription] = useState('');
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  const loadToken = () => {
    const token = localStorage.getItem('authToken');
    if (token) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    }
  };

  useEffect(() => {
    loadToken();
  }, []);


  useEffect(() => {
    const fetchClubs = async () => {
      try {
        const response = await axios.get('http://localhost:8080/api/Club/get-all-clubs');
        const data = response.data;
        if (data.isSuccess) {
          setClubs(data.data);
        } else {
          throw new Error(data.errorMessage || 'Ошибка при загрузке клубов');
        }
      } catch (error) {
        setError(error);
      } finally {
        setLoading(false);
      }
    };

    fetchClubs();
  }, []);

  const openModal = () => setIsModalOpen(true);
  const closeModal = () => setIsModalOpen(false);

  const handleCreateClub = async () => {
    try {
      const response = await axios.post('http://localhost:8080/api/Club/create-club', {
        name: clubName,
        bookTitle: bookTitle,
        description: description
      });
      const data = response.data;
      if (data.isSuccess) {
        // Обновите список клубов
        const updatedResponse = await axios.get('http://localhost:8080/api/Club/get-all-clubs');
        const updatedData = updatedResponse.data;
        if (updatedData.isSuccess) {
          setClubs(updatedData.data);
          closeModal();
        } else {
          throw new Error(updatedData.errorMessage || 'Ошибка при обновлении списка клубов');
        }
      } else {
        throw new Error(data.errorMessage || 'Ошибка при создании клуба');
      }
    } catch (error) {
      setError(error);
    }
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error.message}</div>;

  return (
    <div className="clubs-list">
      <h1>КЛУБЫ</h1>
      <button onClick={openModal}>Создать клуб</button>
      {clubs.map((club) => (
        <Club key={club.id} club={club} />
      ))}
      <Modal
        isOpen={isModalOpen}
        onRequestClose={closeModal}
        contentLabel="Создать клуб"
        className="Modal"
        overlayClassName="Overlay"
      >
        <h2>Создать клуб</h2>
        <form onSubmit={(e) => { e.preventDefault(); handleCreateClub(); }}>
          <label>
            Название клуба:
            <input
              type="text"
              value={clubName}
              onChange={(e) => setClubName(e.target.value)}
              required
            />
          </label>
          <label>
            Название книги:
            <input
              type="text"
              value={bookTitle}
              onChange={(e) => setBookTitle(e.target.value)}
              required
            />
          </label>
          <label>
            Описание:
            <textarea
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              required
            />
          </label>
          {error && <div className="error">{error.message}</div>}
          <button type="submit">Создать</button>
          <button type="button" onClick={closeModal}>Закрыть</button>
        </form>
      </Modal>
    </div>
  );
};

export default ClubsList;

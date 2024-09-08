import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const Club = ({ club }) => {
  const navigate = useNavigate();

  const handleJoinClick = async () => {
    try {
      const response = await axios.get(`http://localhost:8080/api/Club/join-to-club?clubId=${club.id}`);
      const data = response.data;
      if (data.isSuccess) {
        navigate(`/club/${club.id}`);
      } else {
        alert(data.errorMessage || 'Ошибка при вступлении в клуб');
      }
    } catch (error) {
      console.error('Ошибка при вступлении в клуб:', error);
    }
  };

  if (!club) {
    return <div>Клуб не найден</div>;
  }

  return (
    <div style={{
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'space-between',
      marginBottom: '20px',
      padding: '20px',
      border: '1px solid #e0e0e0',
      borderRadius: '10px'
    }}>
      <div>
        <h2>{club.name}</h2>
        <p>{club.topic}</p>
        <button style={{
          background: 'linear-gradient(90deg, #9400D3 0%, #FF1493 100%)',
          color: 'white',
          border: 'none',
          padding: '10px 20px',
          borderRadius: '5px',
          cursor: 'pointer'
        }} onClick={handleJoinClick}>Вступить в клуб</button>
      </div>
      <div>
        <img src={club.imageUrl} alt={club.name} style={{ maxWidth: '150px', borderRadius: '10px' }} />
      </div>
    </div>
  );
};

export default Club;

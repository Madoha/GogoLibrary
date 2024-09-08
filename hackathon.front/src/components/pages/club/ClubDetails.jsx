import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';

const ClubDetails = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [club, setClub] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchClubDetails = async () => {
      try {
        const response = await axios.get(`http://localhost:8080/api/Club/get-club-details?clubId=${id}`);
        const data = response.data;
        if (data.isSuccess) {
          setClub(data.data);
        } else {
          throw new Error(data.errorMessage || 'Error fetching club details');
        }
      } catch (error) {
        setError(error);
      }
    };

    fetchClubDetails();
  }, [id]);

//   const handleJoin = async () => {
//     try {
//       const response = await axios.post(`https://localhost:44307/api/Club/join-to-club?clubId=${id}`);
//       const data = response.data;
//       if (data.isSuccess) {
//         alert('Вы вступили в клуб!');
//       } else {
//         throw new Error(data.errorMessage || 'Error joining club');
//       }
//     } catch (error) {
//       setError(error);
//     }
//   };

  const handleLeave = async () => {
    try {
      const response = await axios.get(`http://localhost:8080/api/Club/leave-from-club?clubId=${id}`);
      const data = response.data;
      if (data.isSuccess) {
        alert('Вы вышли из клуба!');
        navigate('/clubs');
      } else {
        throw new Error(data.errorMessage || 'Error leaving club');
      }
    } catch (error) {
      setError(error);
    }
  };

  if (error) return <div>Error: {error.message}</div>;
  if (!club) return <div>Loading...</div>;

  return (
    <div>
      <h1>{club.name}</h1>
      <p><strong>Название книги:</strong> {club.bookTitle}</p>
      <p><strong>Описание:</strong> {club.description}</p>
      <button>Открыть чат</button> 
      {/* onClick={handleJoin} */}
      <button onClick={handleLeave}>Выйти из клуба</button>
    </div>
  );
};

export default ClubDetails;

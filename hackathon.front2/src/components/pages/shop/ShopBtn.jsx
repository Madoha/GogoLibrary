import { useNavigate } from 'react-router-dom';

const ShopButton = () => {
  const navigate = useNavigate();

  const handleNavigate = () => {
    window.location.href = 'https://2gis.kz/karaganda/firm/11822477302874725';
  };

  return (
    <button className='buy-sec-btn' onClick={handleNavigate}>Наше местоположения</button>
  );
};

export default ShopButton;

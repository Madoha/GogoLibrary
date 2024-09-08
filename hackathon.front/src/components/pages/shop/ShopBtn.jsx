import { useNavigate } from 'react-router-dom';

const ShopButton = () => {
  const navigate = useNavigate();

  const handleNavigate = () => {
    navigate('/shop');
  };

  return (
    <button className='buy-sec-btn' onClick={handleNavigate}>перейти в магазин</button>
  );
};

export default ShopButton;

import { useState } from 'react';
import one from '../../assets/111.png';
import two from '../../assets/222.png';
import three from '../../assets/333.png';

const Maps = () => {
    // Состояние для хранения текущего выбранного этажа
    const [currentFloor, setCurrentFloor] = useState(1);

    // Массив с путями к картинкам этажей
    const floors = [one, two, three];

    // Функция для изменения текущего этажа
    const handleFloorChange = (floor) => {
        setCurrentFloor(floor);
    };

    return (

        <div style={{ height: '100vh' }}>
            <div style={{ textAlign: 'center', justifyContent: 'center' }}>
                <h1>Выберите этаж</h1>

                {/* Картинка текущего этажа */}
                <img
                    src={floors[currentFloor - 1]} // Выбираем картинку соответствующего этажа
                    alt={`Этаж ${currentFloor}`}
                    style={{ width: 'autp', height: '400px', marginBottom: '20px' }}
                />

                {/* Кнопки для выбора этажа */}
                <div>
                    <button onClick={() => handleFloorChange(1)}>Этаж 1</button>
                    <button onClick={() => handleFloorChange(2)}>Этаж 2</button>
                    <button onClick={() => handleFloorChange(3)}>Этаж 3</button>
                </div>
            </div>
        </div>
    );
};

export default Maps;

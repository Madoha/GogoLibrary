import bookitem1 from '../../../assets/bookitem1.png'
import bookitem2 from '../../../assets/bookitem2.png'

const BookSection = () => {
  return (
    <div className="boogs-section">
      <div className="boog-item1">
        <img src={bookitem1} alt="Шри Чайтанья Чаритамрита" className="boog-image" />
        <div className="boog-details">
          <div className='boog-details-1'>
            <h2>ШРИ ЧАЙТАНЬЯ ЧАРИТАМРИТА</h2>
            <p>
              Аспирантура духовной жизни Шри Чайтанья Чаритамрита повествует о жизни Шри Чайтанья
              Махапрабху, жившего в Индии около 500 лет назад, религиозного...
            </p>
          </div>
          <button className="details-button">Подробнее</button>
        </div>
      </div>

      <div className="boog-item2">
        <div className="boog-details">
          <div className='boog-details-2'>
            <h2>ШРИМАД БХАГАВАТАМ</h2>
            <p>
              Перед Вами представлен весь комплект книг Шримад Бхагаватам, переведённый на русский язык,
              состоящий из 26 томов, с 1 по 12 песнь. Изначально знан...
            </p>
          </div>
          <button className="details-button">Подробнее</button>
        </div>
        <img src={bookitem2} alt="Шримад Бхагаватам" className="boog-image" />
      </div>
    </div>
  );
};

export default BookSection;

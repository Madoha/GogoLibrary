import logo from '../../assets/logo2.png'

export default function Footer() {
    return (
        <div className="footer">
            <div className="footer-wrapper">
                <div className='footer-pos'>
                    <div className="footer-top">
                        <ul className="footer-top-list">
                            <li className="footer-top-top-logo"><a href="/" className="header-logo"><img className='footer-logo-img' src={logo} alt='' /></a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Новости</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Чат с менеджером</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Группа ВК</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Ютуб канал</a></li>
                        </ul>
                        <ul className="footer-top-list">
                            <li className="footer-top-top"><a href="#!" className="footer-top-link">Интересное</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Кто мы</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Наш блог</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Наша команда</a></li>
                        </ul>
                        <ul className="footer-top-list">
                            <li className="footer-top-top"><a href="#!" className="footer-top-link">Меню</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Об авторе</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Книги</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Комплекты</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Блог</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">Медиа</a></li>
                            <li className="footer-top-item"><a href="#!" className="footer-top-link">О нас</a></li>
                        </ul>
                    </div>
                    <div className="footer-mid">
                        <a href='#!' className='footer-mid-link'><button className='footer-mid-btn'>Перейти в магазин</button></a>
                        <a href='#!' className='footer-mid-link'><button className='footer-mid-btn'>О доставке</button></a>
                    </div>
                    <div className="footer-btm">
                        <div className='footer-btm-texts'>
                            <p className='footer-btm-text'>Разработчик сайта: AITU Students</p>
                            <p className='footer-btm-text'>gogoLibrary © 2024</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

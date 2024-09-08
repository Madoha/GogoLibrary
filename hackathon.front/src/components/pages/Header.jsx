import logo from '../../assets/logo1.png'
import login from '../../assets/login.png'
import shop from '../../assets/shop.png'

export default function Header() {
    return (
        <div className="header">
            <div className="header-wrapper">
                <div className="header-parts">
                    <div className='header-logo'>
                        <a href="/" className="header-logo"><img className='header-logo-img' src={logo} alt=''/></a>
                    </div>
                    <div className='header-nav'>
                        <ul className='header-nav-list'>
                            <li className='header-nav-item'><a href='#!' className='header-nav-link'>Советы</a></li>
                            <li className='header-nav-item'><a href='#!' className='header-nav-link'>Книги</a></li>
                            <li className='header-nav-item'><a href='#!' className='header-nav-link'>Поиск</a></li>
                            <li className='header-nav-item'><a href='#!' className='header-nav-link'>Блог</a></li>
                            <li className='header-nav-item'><a href='#!' className='header-nav-link'>Интересное</a></li>
                            <li className='header-nav-item'><a href='#!' className='header-nav-link'>О нас</a></li>
                        </ul>
                    </div>
                    <div className="header-icons">
                        <a href="/login" className="header-icons-link"><img src={login} alt='' /></a>
                        <a href="/shop" className="header-icons-link"><img src={shop} alt='' /></a>
                    </div>
                </div>
            </div>
        </div>
    )
}

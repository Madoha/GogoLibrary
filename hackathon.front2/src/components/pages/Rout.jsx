import { Route, Routes, useLocation } from 'react-router-dom';
import Home from './Home'
import Footer from './Footer'
import Header from './Header'
import NotFound from './NotFound';
import Layout from './Layout';
import Register from '../auth/register/Register'
import Login from '../auth/login/Login'
import Comment from '../pages/Comment'
import Profile from '../auth/profile/Profile'
import SearchByTitle from './search/SearchByTitle';
import SearchByDescription from './search/SearchByDesc'
import Maps from './Maps'
import Game from './game/MapComponent'
import AddBook from './shop/AddBook'
import Club from './club/ClubList'
import ClubDetails from './club/ClubDetails'


function AppContent() {
    const location = useLocation();

    const hideNavAndFooter = location.pathname === '/login' || location.pathname === '/register';

    return (
        <div className="App">
            {!hideNavAndFooter && <Header />}
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Home />} />
                    <Route path="*" element={<NotFound />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/login" element={<Login />} />
                    <Route path="/profile" element={<Profile />} /> 
                    <Route path="/search1" element={<SearchByTitle />} />
                    <Route path="/search2" element={<SearchByDescription />} />
                    <Route path="/map" element={<Maps />} />
                    <Route path="/game" element={<Game />} />
                    <Route path="/add" element={<AddBook />} />
                    <Route path="/club" element={<Club />} />
                    <Route path="/club/:id" element={<ClubDetails />} />
                    <Route path="/comment" element={<Comment />} />
                    <Route path="/comment" element={<Comment />} />
                </Route>
            </Routes>
            {!hideNavAndFooter && <Footer />}
        </div>
    );
}

export default AppContent;
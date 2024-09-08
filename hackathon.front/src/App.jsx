import './App.css';//-
import axios from './components/api/axiosConfig'//-
import { BrowserRouter } from 'react-router-dom';//-
import AppContent from './components/pages/Rout';//-
import { useEffect } from 'react';

function App() {
  const loadToken = () => {
    const token = localStorage.getItem('authToken');
    if (token) {
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    }
  };

  useEffect(() => {
    loadToken();
  }, []);

  return (
    <BrowserRouter>
      <AppContent />
    </BrowserRouter>
  );
}

export default App;

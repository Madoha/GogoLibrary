import { useState } from "react";
import axios from "axios";
import SearchIcon from '../../../assets/03.png';

function SearchByDescription() {
  const [description, setDescription] = useState(""); // State for the description input
  const [film, setFilm] = useState({ bookTitle: "", bookAuthor: "" }); // State for the single film found by description
  const [error, setError] = useState(null); // State to handle errors
  const [selectedBookId, setSelectedBookId] = useState(null); // State for the selected book's ID

  // Search by description (returns single film)
  const handleSearch = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post('http://localhost:8080/api/Book/find-by-description/', {
        description: description,
      });

      const filmData = response.data.data;

      const parts = filmData.split('\n');
      const bookTitle = parts[0].replace('BookTitle: ', '').trim();
      const bookAuthor = parts[1].replace('BookAuthor: ', '').trim();

      setFilm({ bookTitle, bookAuthor });
    } catch (error) {
      console.error("Error fetching film:", error);
      setError("Error fetching film details. Please try again.");
    }
  };

  return (
    <div>
      <div className="search-container">
        <h1 className='booksrc-title'>ПОИСК КНИГ ПО ОПИСАНИЮ</h1>

        <div className="search-area">
          <form className="search-cont" onSubmit={handleSearch}>
            <input
              type="text"
              placeholder="Film Description"
              className="search-input"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />
            <button type="submit">
              <img src={SearchIcon} alt="Search" />
            </button>
          </form>
        </div>
      </div>

      {error && <p>{error}</p>} {/* Display error messages if any */}

      {/* Display the result based on description search */}
      {film && film.bookTitle && (
        <div className="book-container">
          <div className="book2-cont">
            <p><strong>Название:</strong> {film.bookTitle}</p>
            <p><strong>Автор:</strong> {film.bookAuthor}</p>
           </div>
        </div>
      )}
    </div>
  );
}

export default SearchByDescription;

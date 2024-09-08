import { useState } from "react";
import axios from "axios";

function FilmDescription() {
  const [description, setDescription] = useState("");
  const [film, setFilm] = useState({ bookTitle: "", bookAuthor: "" });

  const handleSubmit = async (e) => {
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
    }
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <label>
          Film Description:
          <input
            type="text"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
        </label>
        <button type="submit">Get Film</button>
      </form>

      {film && (
        <div>
          <h2>Film Details:</h2>
          <p>{film.bookTitle}</p>
          <p>{film.bookAuthor}</p>
        </div>
      )}
    </div>
  );
}

export default FilmDescription;

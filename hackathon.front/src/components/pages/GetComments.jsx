import { useState, useEffect } from "react";
import axios from "axios";

function GetComments({ bookId }) {
  const [comments, setComments] = useState([]);  // State for storing comments
  const [error, setError] = useState(null);  // State for handling errors

  // Fetch comments whenever the bookId changes
  useEffect(() => {
    const fetchComments = async () => {
      try {
        const response = await axios.get(`http://localhost:8080/api/comment/get-comments?bookId=${bookId}`);
        setComments(response.data.data);  // Assuming the response is in `data.data`
      } catch (error) {
        console.error("Error fetching comments:", error);
        setError("Could not fetch comments. Please try again later.");
      }
    };

    if (bookId) {
      fetchComments();
    }
  }, [bookId]);

  return (
    <div>
      <h5>Комментарии для данной книги: </h5>
      {error && <p>{error}</p>}
      
      {comments.length > 0 ? (
        <ul className="listlist">
          {comments.map((comment) => (
            <li key={comment.commentId}>
              <p><em>Posted on: {new Date(comment.createdAt).toLocaleString()}</em> <br/>{comment.content} </p>
            </li>
          ))}
        </ul>
      ) : (
        <p>No comments available for this book.</p>
      )}
    </div>
  );
}

export default GetComments;

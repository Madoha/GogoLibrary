import { useState } from "react";
import axios from "axios";
import Comments from './GetComments';  // Assuming there's a component to display comments

function CommentForm({ bookId }) {  // Accept bookId as prop
  const [comment, setComment] = useState({
    bookId: bookId,  // Set the bookId from the prop
    userId: 1,  // Replace with dynamic user ID if necessary
    content: "",  // Comment content
  });
  const [responseMessage, setResponseMessage] = useState(null);

  // Handle input change
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setComment({ ...comment, [name]: value });
  };

  // Handle form submission
  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!comment.bookId || !comment.userId || !comment.content.trim()) {
      setResponseMessage("All fields are required.");
      return;
    }

    try {
      console.log("Payload:", comment);

      const response = await axios.post('http://localhost:8080/api/comment/create/', comment);
      setResponseMessage("Comment posted successfully!");
      console.log("Response:", response.data.data);

      // Optionally, clear the form after successful submission
      setComment({
        bookId: bookId,  // Reset bookId
        userId: 1,  // Keep userId the same, adjust if necessary
        content: "",  // Clear content
      });
    } catch (error) {
      console.error("Error posting comment:", error);
      if (error.response) {
        console.log("Response data:", error.response.data);
        setResponseMessage(`Error posting comment: ${error.response.data.message || "Invalid request."}`);
      } else {
        setResponseMessage("Error posting comment. Please try again.");
      }
    }
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <textarea
        className="commentt-input"
          name="content"
          placeholder="Write your comment here..."
          value={comment.content}
          onChange={handleInputChange}
          rows={4}
          cols={50}
        />
        <button className="add-btn" type="submit">Post Comment</button>
      </form>

      {/* Display the response message */}
      {responseMessage && <p>{responseMessage}</p>}

      {/* Render existing comments for this book */}
      <Comments bookId={bookId} /> 
    </div>
  );
}

export default CommentForm;

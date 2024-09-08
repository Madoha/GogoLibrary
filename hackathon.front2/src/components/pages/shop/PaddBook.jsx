import { useState } from 'react';
import AddBookModal from './AddBook';

const ParentComponent = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const openModal = () => setIsModalOpen(true);
  const closeModal = () => setIsModalOpen(false);

  return (
    <div>
      <button className="read-btn" onClick={openModal}>Add Book</button>
      <AddBookModal isOpen={isModalOpen} onClose={closeModal} />
    </div>
  );
};

export default ParentComponent;

import axios from 'axios';

const bookUrl: string = 'https://localhost:5050/api/Book/';
const borrowingUrl: string = 'https://localhost:5050/api/Borrowing/';

export const fetchBooks = () => axios.get(`${bookUrl}allbooks`);
export const createBorrowing = (newBorrowing) => axios.post(`${borrowingUrl}`, newBorrowing);


export const fetchBorrowing = () => axios.get(`${borrowingUrl}allborrowings`);
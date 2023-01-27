import axios from 'axios';

const url: string = 'https://localhost:5050/api/Book/';

export const fetchBooks = () => axios.get(url + 'allbooks');
export const createTradeDetail = (newTrade) => axios.post(url, newTrade);
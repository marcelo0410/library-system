export const getFullDate = (date: string): string => {
    const dateAndTime = date.split('T');

    return dateAndTime[0].split('-').reverse().join('-');
};

export const generateBorrowNumber = () => {
    const date = new Date();
    return 'B1000' + date.getFullYear() + date.getMonth() + date.getDate() + date.getHours() + date.getMinutes() + date.getSeconds(); 
}

export const generateDueDate = () => {
    return new Date().setDate(new Date().getDate() + 14);
}
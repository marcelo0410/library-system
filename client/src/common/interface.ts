export interface Book {
    id: string , 
    title: string , 
    publisher: string,
    dateOfPublication: string,
    isBorrowed: boolean
}

export interface Borrowing {
    id: string,
    borrowNo: string,
    userId: string,
    bookId: string,
    dateOfBorrow: string,
    dueDate: string,
    bookForeignKey: Object,
    borrowings: object
}
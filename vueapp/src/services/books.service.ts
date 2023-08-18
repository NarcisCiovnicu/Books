import axios from "axios"
import processError from './utils'
import Book from "@/models/book";

export default class BooksService {

    async fetchAllBooks() : Promise<Book[]> {
        return await axios.get("/api/Books")
            .then(response => response.data)
            .catch(error => {
                    processError(error);
                    return [];
                });
    }

    async fetchBook(id: number) : Promise<Book | null> {
        return await axios.get(`/api/Books/${id}`)
            .then(response => response.data)
            .catch(error => {
                    processError(error);
                    return null;
                });
    }

    async addBook(book: Book) : Promise<boolean> {
        return await axios.post("/api/Books/Add", book)
            .then(response => true)
            .catch(error => {
                processError(error);
                return false;
            });
    }

    async updateBook(book: Book) : Promise<boolean> {
        return await axios.put("/api/Books/Update", book)
            .then(response => true)
            .catch(error => {
                processError(error);
                return false;
            });
    }

    async deleteBook(id: number) : Promise<boolean> {
        return await axios.delete(`/api/Books/Delete/${id}`)
            .then(response => {
                return true;
            })
            .catch(error => {
                processError(error);
                return false;
            });
    }
}

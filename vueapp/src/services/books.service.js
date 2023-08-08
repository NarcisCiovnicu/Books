import axios from "axios"
import processError from './utils.js'

export default class BooksService {

    async fetchAllBooks() {
        return await axios.get("/api/Books")
            .then(response => response.data)
            .catch(error => {
                    processError(error);
                    return [];
                });
    }

    async fetchBook(id) {
        return await axios.get(`/api/Books/${id}`)
            .then(response => response.data)
            .catch(error => {
                    processError(error);
                    return null;
                });
    }

    async addBook(book) {
        return await axios.post("/api/Books/Add", book)
            .then(response => true)
            .catch(error => {
                processError(error);
                return false;
            });
    }

    async updateBook(book) {
        return await axios.put("/api/Books/Update", book)
            .then(response => true)
            .catch(error => {
                processError(error);
                return false;
            });
    }

    async deleteBook(id) {
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

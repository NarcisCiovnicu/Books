import axios from "axios"
import processError from './utils.js'

export default class AuthorsService {
    
    async fetchAllAuthors() {
        return await axios.get("/api/Authors")
            .then(response => response.data)
            .catch(error => {
                processError(error);
                    return [];
            });
    }
}
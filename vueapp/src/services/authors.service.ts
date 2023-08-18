import axios from "axios"
import processError from './utils'
import Author from "@/models/author";

export default class AuthorsService {
    
    async fetchAllAuthors() : Promise<Author[]> {
        return await axios.get("/api/Authors")
            .then(response => response.data)
            .catch(error => {
                processError(error);
                    return [];
            });
    }
}
import Author from "./author";

export default class Book {
    id: number = 0;
    title: string = "";
    description: string = "";
    coverPhoto: string | null = null;
    authors: Author[] = [];
}
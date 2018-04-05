import { Author } from "../../author/author.model";

export class Book {
    constructor(
        public id?: number,
        public isbn?: string,
        public capation?: string,
        public additionalData?: string,
        public releaseDate?: string,
        public authors?: Author[],
        public publicationsCount?: number,
        public authorsFormated?: string) { }
}


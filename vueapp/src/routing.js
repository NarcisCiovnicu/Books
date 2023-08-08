import {createRouter, createWebHistory} from 'vue-router'
import BooksView from './views/BooksView.vue'
import AddBookView from './views/AddBookView.vue'
import EditBookView from './views/EditBookView.vue'

let router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/books",
            name: "ListOfBooks",
            component: BooksView
        },
        {
            path: "/books/add",
            name: "AddNewBook",
            component: AddBookView
        },
        {
            path: "/books/edit/:id",
            name: "EditEook",
            component: EditBookView
        },
        {
            path: "/",
            redirect: "/books"
        }
    ]
});

export default router;
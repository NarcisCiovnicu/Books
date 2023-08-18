import { createApp } from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'
import router from './routing'
import BooksService from './services/books.service'
import AuthorsService from './services/authors.service'
import 'vue-toast-notification/dist/theme-sugar.css';

loadFonts()

createApp(App)
  .use(vuetify)
  .use(router)
  .provide("booksService", new BooksService())
  .provide("authorsService", new AuthorsService())
  .mount('#app');

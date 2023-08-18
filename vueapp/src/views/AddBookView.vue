<template>
 
    <MainTitle text="Add new book" />

    <EditBookForm :book="book" :form="form"> 
        <template v-slot:confirm-btn>
            <v-btn @click="addBook" :disabled="!form.isValid" color="primary">
                Add
            </v-btn>
        </template>
    </EditBookForm>
</template>

<script setup lang="ts">
    import Book from '@/models/book';
    import BooksService from '@/services/books.service';
    import MainTitle from '@/components/MainTitle.vue';
    import EditBookForm from '@/components/EditBookForm.vue';
    import router from '@/routing';
    import { inject, ref } from 'vue';

    const booksService = inject<BooksService>("booksService")!;

    const form = ref({
        isValid: false
    });
    const book = ref<Book>(new Book());

    const addBook = async () => {
        let result = await booksService.addBook(book.value);
        if (result) {
            router.push("/Books");
        }
    };

</script>

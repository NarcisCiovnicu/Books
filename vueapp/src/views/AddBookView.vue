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

<script setup lang="js">
    import MainTitle from '@/components/MainTitle.vue';
    import EditBookForm from '@/components/EditBookForm.vue';
    import router from '@/routing';
    import { inject, ref } from 'vue';

    const booksService = inject("booksService");

    const form = ref({
        isValid: false
    });
    const book = ref({
        title: null,
        description: null,
        coverPhoto: null,
        authors: []
    });

    const addBook = async () => {
        let result = await booksService.addBook(book.value);
        if (result) {
            router.push("/Books");
        }
    };

</script>

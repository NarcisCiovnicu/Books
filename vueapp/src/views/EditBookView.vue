
<template>

   <MainTitle text="Edit book" />
   <v-container v-if="isLoading" class="d-flex justify-center">
      <v-progress-circular color="primary" indeterminate></v-progress-circular>
   </v-container>
   <EditBookForm v-else-if="book" :book="book" :form="form">
      <template v-slot:confirm-btn>
         <v-btn @click="saveBook" :disabled="!form.isValid" color="primary">
            Save
         </v-btn>
      </template>
   </EditBookForm>
   <v-container v-else>
      Erorr
   </v-container>
  
</template>
  
<script setup lang="js">
   import MainTitle from '@/components/MainTitle.vue';
   import EditBookForm from '@/components/EditBookForm.vue';
   import router from '@/routing';
   import { inject, ref, onMounted } from 'vue';
   import {useRoute} from "vue-router";

   const booksService = inject("booksService");

   const isLoading = ref(true);
   const form = ref({
      isValid: true
   });

   const book = ref(null);

   onMounted(async () => {
      const route = useRoute();
      const bookId = route.params.id;

      book.value = await booksService.fetchBook(bookId);

      isLoading.value = false;
   });


   const saveBook = async () => {
        let result = await booksService.updateBook(book.value);
        if (result) {
            router.push("/Books");
        }
    };

</script>
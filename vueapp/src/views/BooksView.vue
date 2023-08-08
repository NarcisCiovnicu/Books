 <template>

<MainTitle text="List of books" />

<v-dialog v-model="deleteDialog" width="auto">
      <v-card>
        <v-card-text>
            Are you sure you want to delete this book? '{{ selectedBook.title }}'
        </v-card-text>
        <v-card-actions class="d-flex justify-space-around">
            <v-btn color="primary" @click="deleteBook(selectedBook.id)">Yes</v-btn>
            <v-btn @click="deleteDialog = false">Cancel</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

<v-container v-if="isLoading" class="d-flex justify-center">
    <v-progress-circular color="primary" indeterminate></v-progress-circular>
</v-container>

<v-container v-else class="pa-2 bg-grey-darken-4"> <!-- bg-surface-variant -->
    <v-row no-gutters align="center" class="d-flex"> <!-- Header row -->
        <v-col cols="2">
            Title
        </v-col>
        <v-col cols="4">
            Description
        </v-col>
        <v-col cols="2" class="d-flex justify-center">
            Cover Photo
        </v-col>
        <v-col cols="2" class="d-flex justify-center">
            Authors
        </v-col>
        <v-col cols="2" class="d-flex justify-center">
            <v-btn color="primary" size="small" @click="goToAddPage()">
                Add new
                <v-icon end icon="mdi-plus"></v-icon>
            </v-btn>
        </v-col>
    </v-row>
    
    <v-divider :thickness="3" class="mt-4 mb-1 border-opacity-75" />

    <template v-for="(book, index) in books" :key="book.id">
        <v-row no-gutters align="stretch"> <!-- list of row -->
            <v-col class="" cols="2">
                {{ book.title }}
            </v-col>
            <v-col class="" cols="4">
                {{ book.description }}
            </v-col>
            <v-col class="" cols="2">
                <v-img max-height="20vh" max-width="20vw" title="cover photo" :src="`data:image;base64,${book.coverPhoto}`"/>
            </v-col>
            <v-col class="pl-4" cols="2">
                <div v-for="author in book.authors" :key="author.id">
                    {{ author.name }}
                </div>
            </v-col>
            <v-col class="d-flex flex-column justify-center align-center" cols="2">
                    <v-btn class="w-50 my-1" size="small" color="secondary" @click="goToEditPage(book.id)">
                        Edit
                        <v-icon end icon="mdi-file-edit"></v-icon>
                    </v-btn>
                    <v-btn class="w-50 my-1" size="small" color="red" @click="deleteDialog = true; selectedBook = book">
                        Delete
                        <v-icon end icon="mdi-minus-circle"></v-icon>
                    </v-btn>
            </v-col>
        </v-row>
        <v-divider v-if="index + 1 < books.length" :thickness="2" class="border-opacity-50" />
    </template>
    
</v-container>

</template>

<script setup lang="js">
    import MainTitle from '@/components/MainTitle.vue';
    import router from '@/routing';
    import { ref, onMounted, inject } from 'vue'
    
    const booksService = inject("booksService");
    
    const deleteDialog = ref(false);
    const isLoading = ref(true);
    const books = ref([]);
    const selectedBook = ref(null);

    onMounted(async () => {
        books.value = await booksService.fetchAllBooks();
        isLoading.value = false;
    });

    const deleteBook = async (id) => {
        if (await booksService.deleteBook(id)) {
            books.value = books.value.filter(b => b.id !== id);
        }
        deleteDialog.value = false;
    };

    const goToAddPage = () => {
        router.push("/Books/Add");
    };

    const goToEditPage = (id) => {
        router.push(`/Books/Edit/${id}`);
    };

</script>

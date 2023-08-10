<template>
    <v-container>
        <v-form v-model="form.isValid">
            <v-row>
                <v-text-field :rules="requiredRules" v-model.trim="book.title" label="Title" required />
            </v-row>
            <v-row>
                <v-textarea :rules="requiredRules" v-model.trim="book.description" label="Description" required />
            </v-row>
            <v-row>
                <v-col>
                    <v-file-input @change="uploadCoverPhoto" @click:clear="clearImage" :rules="imageRules" 
                    prepend-icon="mdi-image" show-size accept="image/png, image/jpeg, image/bmp" label="Upload cover photo" chips required />
                </v-col>
                <v-col>
                    <v-img v-if="book.coverPhoto" :src="`data:image;base64,${book.coverPhoto}`" max-width="16vw" max-height="25vh" alt="Cover photo" />
                    <v-img v-else :src="require('../assets/no-image.png')" max-width="16vw" max-height="25vh" alt="No image" />
                </v-col>
            </v-row>
            <v-row>
                <v-select v-model="selectedAuthors" @update:modelValue="addAuthors" :rules="requiredRules" :items="authors" :disabled="!authors.length"
                    chips label="Authors" multiple item-title="name" item-value="id" />
            </v-row>
        </v-form>
        <v-row>
            <v-col align="center">
                <slot name="confirm-btn" />
            </v-col>
            <v-col align="center">
                <v-btn @click="cancel">
                    Cancel
                </v-btn>
            </v-col>
        </v-row>
    </v-container>
</template>

<script setup lang="js">
    import {defineProps, ref, inject, onMounted} from 'vue'
    import router from '@/routing';

    const props = defineProps(['book', 'form']);
    
    const authorsService = inject("authorsService");

    const authors = ref([]);
    const selectedAuthors = ref([]);

    const form = ref(props.form);
    const book = ref(props.book);


    onMounted(async () => {
        authors.value = await authorsService.fetchAllAuthors();
        if (book.value.id) {
            selectedAuthors.value = book.value.authors.map(auth => auth.id);
        }
    });

    const uploadCoverPhoto = (event) => {
        book.value.coverPhoto = null;

        if (event.target.files.length) {
            let file = event.target.files[0];
            
            if (file.size > 2000000) {
                return;
            }

            const reader = new FileReader();
            reader.onload = () => {
                let [_, img] = reader.result.split(',');
                book.value.coverPhoto = img;
            };

            reader.readAsDataURL(file);
        }
    };
    const clearImage = () => {
        book.value.coverPhoto = null;
    };

    const addAuthors = () => {
        book.value.authors = authors.value.filter((auth) => selectedAuthors.value.includes(auth.id));
    };

    const cancel = () => {
        router.push("/Books");
    };

    const imageRules = [
        value => {
            if (book.value.coverPhoto) {
                return true;
            }
            if (!value || !value.length) {
                return "Required";
            }
            else if (value[0].size > 2000000) {
                return "Cover photo size should be less than 2 MB!";
            }
            return true;
        }
    ];
    const requiredRules = [
        value => {
            if (!value || !value.length) {
                return "Required"
            }
            return true;
        }
    ];
</script>
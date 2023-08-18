<template>
    <v-container class="d-flex justify-center">
        <v-card-title class="text-h6 text-md-h5 text-lg-h4">{{ props.text }}</v-card-title>
    </v-container>
</template>

<script setup lang="ts">

const props = defineProps(['text']);

</script>
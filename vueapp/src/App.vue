<template>
  <v-app>
    <v-main>
      <RouterView/>
      <v-container class="py-12">
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup lang="js">
  import { RouterView } from 'vue-router'
</script>

<template>
  <v-app>
    <v-main>
      <RouterView/>
      <v-container class="py-12">
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import { RouterView } from 'vue-router'
</script>
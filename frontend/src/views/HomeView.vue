<script setup lang="ts">
import Login from '../components/Login.vue'
import NoteList from '../components/NoteList.vue'
import { ref } from 'vue';

const isAuthenticated = ref(false);
isAuthenticated.value = localStorage.getItem('jwt_token') != undefined;
</script>

<template>
  <main>
    <Login v-if="!isAuthenticated" />
    <NoteList v-else/>
  </main>
</template>

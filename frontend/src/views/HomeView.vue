<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { userStore } from '@/stores/userStore';
import Login from '../components/Login.vue';
import NoteList from '../components/NoteList.vue';

const store = userStore();
const isAuthenticated = ref(false);
isAuthenticated.value = store.isAuthenticated;

</script>

<template>
  <main>
    <Login v-if="!isAuthenticated" />
    <NoteList v-else />
  </main>
</template>

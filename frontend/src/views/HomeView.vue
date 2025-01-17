<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { userStore } from '@/stores/userStore';
import Login from '../components/Login.vue';
import NoteList from '../components/NoteList.vue';
import Dashboard from '../components/Dashboard.vue'

const store = userStore();
const isAuthenticated = ref(false);
isAuthenticated.value = store.isAuthenticated;

</script>

<template>
  <main>
    <Login v-if="!isAuthenticated" />
    <Dashboard v-else />
  </main>
</template>

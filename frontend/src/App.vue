<template>
  <Header />
  <RouterView />
</template>

<script lang="ts">
import { RouterView } from 'vue-router';
import Header from './components/Header.vue';

export default {
  components: {
    Header,
    RouterView
  }
}
</script>



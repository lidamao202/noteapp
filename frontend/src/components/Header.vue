<template>
    <el-menu mode="horizontal">
      <el-menu-item index="1">
        <RouterLink to="/">Home</RouterLink>
      </el-menu-item>
      <el-menu-item index="2" v-if="isAuthenticated" @click="onLogout">Logout</el-menu-item>
    </el-menu>
  </template>
  
  <script lang="ts">
  import { ref, onMounted } from 'vue';
  import { useRouter } from 'vue-router';
  
  export default {
    setup() {
      const isAuthenticated = ref<boolean>(false);
      const router = useRouter();
  
      const onLogout = () => {
        console.log("logout");
        localStorage.removeItem('jwt_token');
        window.location.href = "/";
        router.push({ name: 'home' });
      };
  
      onMounted(() => {
        console.log(localStorage.getItem('jwt_token'));
        isAuthenticated.value = localStorage.getItem('jwt_token') ?? undefined;
      });
  
      return {
        isAuthenticated,
        onLogout
      };
    }
  }
  </script>
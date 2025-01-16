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
import { userStore } from '@/stores/userStore';
import { is } from '@vee-validate/rules';

export default {
  setup() {
    const router = useRouter();
    const store = userStore();
    const isAuthenticated = ref<boolean>(false);
    

    const onLogout = () => {
      store.logout(); 
      router.push({ name: 'home' });
    };

    onMounted(() => {
        isAuthenticated.value = store.isAuthenticated;
    });

    return {
      isAuthenticated,
      onLogout
    };
  }
}
</script>
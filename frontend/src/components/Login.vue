<template>
  <div class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="w-full max-w-sm p-8 bg-white rounded-lg shadow-lg">
      <h2 class="text-2xl font-semibold text-center text-gray-800 mb-6">Login</h2>
      <el-form label-width="auto" style="max-width: 600px">
        <el-form-item label="User name">
          <el-input v-model="userName" />
        </el-form-item>
        <el-form-item label="Password">
          <el-input v-model="password" type="password" />
        </el-form-item>
        <el-form-item>
          <el-button
            class="w-full py-2 bg-indigo-600 text-white font-semibold rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="primary" @click="onSubmit">Submit</el-button>
          <div class="mt-4 text-center">
            <el-link type="primary" @click="onRegister" href="#" class="text-sm text-indigo-600 hover:underline">Register</el-link>
          </div>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { userStore } from '@/stores/userStore';
import { jwtDecode } from 'jwt-decode';
import { login } from '@/services/apiService';

export default {
  setup() {
    const router = useRouter();
    const store = userStore();
    const userName = ref<string>('');
    const password = ref<string>('');

    const onSubmit = async () => {
      try {
        const response = await login(userName.value, password.value);
        const decoded = jwtDecode(response.data);
        store.setToken(response.data,10);
        store.setUser({ userName: decoded.email });
        //window.location.href = '/notelist';
        router.push({ path: '/notelist' });
      } catch (error) {
        console.log(error);
      }
    };


    const onRegister = () => {
      router.push({ path: '/register' });
    };

    return {
      userName,
      password,
      onSubmit,
      onRegister
    };
  }
}
</script>
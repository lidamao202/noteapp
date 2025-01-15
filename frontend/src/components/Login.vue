<template>
  <div class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="w-full max-w-sm p-8 bg-white rounded-lg shadow-lg">
      <h2 class="text-2xl font-semibold text-center text-gray-800 mb-6">Login</h2>
      <el-form label-width="auto" style="max-width: 600px">
    <el-form-item label="User name">
      <el-input v-model="userName" />
    </el-form-item>
    <el-form-item label="Password">
      <el-input v-model="password" type="password"/>
    </el-form-item>
  
    <el-form-item>
      <el-button 
      class="w-full py-2 bg-indigo-600 text-white font-semibold rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500"
      type="primary" @click="onSubmit">Submit</el-button>
      <!-- <el-button @click="onRegister" class="mt-4 text-center">Register</el-button> -->
     <div class="mt-4 text-center">
      <el-link type="primary"
      @click="onRegister"
        href="#" class="text-sm text-indigo-600 hover:underline"
      >Register</el-link>
     </div>
    </el-form-item>
  </el-form>
    </div>

  </div>

</template>

<script lang="ts" >

import { reactive } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios';
import { userStore } from '@/stores/userStore'
import { jwtDecode } from "jwt-decode"
import { ref } from 'vue';
import axiosInstance from '../JwtInterceptor';

export default {
  setup() {
    const router = useRouter();
    let userName = ref<string>('');
    let password = ref<string>('');



    const onSubmit = (): void => {
      axiosInstance.get(`/account/login?username=${userName.value}&password=${password.value}`)
      .then(response => {

        const decoded = jwtDecode(response.data);

        localStorage.setItem('jwt_token',response.data);
        localStorage.setItem('id',decoded.UserId);
        localStorage.setItem('userName',decoded.email);
    
        router.push({ path: '/noteList' });
      })
      .catch(error => {
        console.log(error);
      });
    };

    const onRegister = () => {
      router.push({ path: '/register' });
    }

    return {
      onSubmit,
      userName,
      password,
      onRegister

    };
  }
}


</script>
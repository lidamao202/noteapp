<template>
  <el-form label-width="auto" style="max-width: 600px">
    <el-form-item label="User name">
      <el-input v-model="userName" />
    </el-form-item>
    <el-form-item label="Password">
      <el-input v-model="password" type="password"/>
    </el-form-item>
  
    <el-form-item>
      <el-button type="primary" @click="onSubmit">Submit</el-button>
      <el-button @click="onRegister" >Register</el-button>
    </el-form-item>
  </el-form>
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
        // console.log(decoded)
        // let user = userStore();
        // user.setId(decoded.Id);
        // user.setUserName=(decoded.email);
        // console.log(user.id)
        localStorage.setItem('jwt_token',response.data);
        localStorage.setItem('id',decoded.Id);
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
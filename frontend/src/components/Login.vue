<template>
  <el-form :model="form" label-width="auto" style="max-width: 600px">
    <el-form-item label="User name">
      <el-input v-model="form.userName" />
    </el-form-item>
    <el-form-item label="Password">
      <el-input v-model="form.password" />
    </el-form-item>
  
    <el-form-item>
      <el-button type="primary" @click="onSubmit">Submit</el-button>
      <el-button>Register</el-button>
    </el-form-item>
  </el-form>
</template>

<script lang="ts" setup>

import { reactive } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios';
import { userStore } from '@/stores/userStore'

const form = reactive(
  {
    userName: "",
    password: ""
  }
)
const router = useRouter();
const onSubmit = () => {

  axios.get(`https://localhost:59916/api/account/login?username=${form.userName}&password=${form.password}`)
  .then(response => {
    console.log(response.data);
    userStore.id=response.data.id;
    userStore.userName=response.data.userName;
    
    router.push({ path: '/noteList' })
  })
  .catch(error => {
    console.log(error);
  });
}
</script>
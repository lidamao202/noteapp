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

<script>

import { reactive } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios';
import { userStore } from '@/stores/userStore'
import { jwtDecode } from "jwt-decode"

export default{
  
  data(){
    
    return{
      userName: "",
      password: ""
    }
  },
  methods:{
    onSubmit:function(){
      axios.get(`https://localhost:59916/api/account/login?username=${this.userName}&password=${this.password}`)
      .then(response => {

        const decoded = jwtDecode(response.data);
        console.log(decoded)
        let user = userStore();
        user.id=decoded.Id;
        user.userName=decoded.email;
        localStorage.setItem('jwt_token',response.data);
        
        this.$router.push({ path: '/noteList' });
      })
      .catch(error => {
        console.log(error);
      });
    },
    onRegister:function(){
      router.push({ path: '/register' });
    }
  }
}
const form = reactive(
  {
    userName: "",
    password: ""
  }
)

</script>
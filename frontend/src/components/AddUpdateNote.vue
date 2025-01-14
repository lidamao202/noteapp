<template>
  <el-form label-width="auto" style="max-width: 600px">
    <el-form-item label="Title">
      <el-input v-model="title" />
    </el-form-item>
    <el-form-item label="Content">
      <el-input v-model="content" />
    </el-form-item>
  
    <el-form-item>
      <el-button type="primary" @click="onSubmit">Submit</el-button>
    </el-form-item>
  </el-form>
</template>

<script >
import { reactive } from 'vue'
import axios from 'axios';
import { useRouter } from 'vue-router'
import { userStore } from '@/stores/userStore'

export default {

  data(){
    const router = useRouter();
    console.log(userStore)

    return{
        id:"",
        title:"",
        content:"",
        //userId:router.currentRoute._value.query.userId
        userId:userStore.id
    }
  },
  mounted(){
    console.log("getAll note")
    axios.get(`https://localhost:59916/api/note/getAll`)
    .then(response => {
      this.tableData = response.data;
      console.log(response.data);
    })
    .catch(error => {
      console.log(error);
    });
  },
  methods:{
    onSubmit: function($event){
        console.log("onSubmit");
        console.log(this.userId)
        axios.post(`https://localhost:59916/api/note`,{
            title:this.title,
            content:this.content,
            userId:this.userId
        },{
            headers: {
              "Content-Type": "application/json"
            }
        })
        .then(function (response) {
                    alert('successfully create');
                    this.$router.push({ path: '/noteList' })
                })
                .catch((error) => { console.log(error) })
     
            }
        }
    }



</script>
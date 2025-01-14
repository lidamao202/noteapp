<template>
  <el-button type="primary" @click="onAddNew">Add New</el-button>
  <el-table :data="tableData" >
    <el-table-column prop="id" label="Id" width="180" />
    <el-table-column prop="title" label="Title" width="180" />
    <el-table-column prop="content" label="Content" />
    <el-table-column prop="date_Created" label="Created Date" />
    <el-table-column prop="date_Updated" label="Update Date" />
  </el-table>
</template>

<script >
import { reactive } from 'vue'
import axios from 'axios';
import { useRouter } from 'vue-router'

export default {
  data(){
    return{
      tableData:[]
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
    onAddNew: function($event){
      console.log("onAddNew");
      this.$router.push({ name: 'AddUpdateNote' , query: { userId:""}})
    }
  }
}



</script>

<style scoped>


@media (min-width: 1024px) {
  .item {
    margin-top: 0;
    padding: 0.4rem 0 1rem calc(var(--section-gap) / 2);
  }

  i {
    top: calc(50% - 25px);
    left: -26px;
    position: absolute;
    border: 1px solid var(--color-border);
    background: var(--color-background);
    border-radius: 8px;
    width: 50px;
    height: 50px;
  }

  .item:before {
    content: ' ';
    border-left: 1px solid var(--color-border);
    position: absolute;
    left: 0;
    bottom: calc(50% + 25px);
    height: calc(50% - 25px);
  }

  .item:after {
    content: ' ';
    border-left: 1px solid var(--color-border);
    position: absolute;
    left: 0;
    top: calc(50% + 25px);
    height: calc(50% - 25px);
  }

  .item:first-of-type:before {
    display: none;
  }

  .item:last-of-type:after {
    display: none;
  }
}
</style>

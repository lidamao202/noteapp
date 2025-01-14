<template>
  <el-button type="primary" @click="onAddNew">Add New</el-button>
  <el-table :data="tableData" >
    <el-table-column prop="title" label="Title" width="180" />
    <el-table-column prop="content" label="Content" />
    <!-- <el-table-column prop="date_Created" label="Created Date" />
    <el-table-column prop="date_Updated" label="Update Date" /> -->
    <el-table-column fixed="right" label="Operations" min-width="120">
      <template #default="scope">
        <el-button
          link
          type="primary"
          size="small"
          @click.prevent="editRow(scope.$index)"
        >
          Edit
        </el-button>
      </template>
    </el-table-column>
    <el-table-column fixed="right" label="Operations" min-width="120">
      <template #default="scope">
        <el-button
          link
          type="primary"
          size="small"
          @click.prevent="deleteRow(scope.$index)"
        >
          Remove
        </el-button>
      </template>
    </el-table-column>

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
    this.getAll();
  },
  methods:{
    onAddNew: function($event){
      console.log("onAddNew");
      this.$router.push({ name: 'AddUpdateNote'})
    },
    deleteRow:function(index)  {
      if(confirm("Are you sure you want to delete?") == true){
        axios.delete(`https://localhost:59916/api/note/${this.tableData[index].id}`)
        .then(response => {
          this.getAll();
        })
        .catch(error => {
          console.log(error);
        });
      }

    },
    editRow:function(index){
      this.$router.push({ name: 'AddUpdateNote', query:{noteId:this.tableData[index].id}})
    },
    getAll: function(){
      axios.get(`https://localhost:59916/api/note/getAll`)
      .then(response => {
        this.tableData = response.data;
      })
      .catch(error => {
        console.log(error);
      });
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

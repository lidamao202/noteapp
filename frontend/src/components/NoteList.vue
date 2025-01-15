<template>
  <el-button type="primary" @click="onAddNew">Add New</el-button>

  <el-input v-model="input" style="max-width: 600px" placeholder="Please input" class="input-with-select">
    <template #prepend>
      <el-button @click="search">Search</el-button>
    </template>
  </el-input>

  <el-table :data="tableData">
    <el-table-column prop="title" label="Title" width="180" />
    <el-table-column prop="content" label="Content" />
    <el-table-column prop="date_Created" label="Created Date" />
    <el-table-column fixed="right" label="Operations" min-width="120">
      <template #default="scope">
        <el-button link type="primary" size="small" @click.prevent="viewRow(scope.$index)">
          View
        </el-button>
        <el-button link type="primary" size="small" @click.prevent="editRow(scope.$index)">
          Edit
        </el-button>
        <el-button link type="primary" size="small" @click.prevent="deleteRow(scope.$index)">
          Remove
        </el-button>
      </template>
    </el-table-column>


  </el-table>
</template>

<script>
import { reactive } from 'vue'
import axios from 'axios';
import { useRouter } from 'vue-router'
import { userStore } from '@/stores/userStore'
import axiosInstance from '../JwtInterceptor';

export default {

  data() {
    return {
      tableData: [],
      input: ""
    }
  },
  mounted() {
    console.log("getAll note")
    this.getAll();
  },
  methods: {
    onAddNew: function ($event) {
      console.log("onAddNew");
      this.$router.push({ name: 'AddUpdateNote', isView: false })
    },
    deleteRow: function (index) {
      if (confirm("Are you sure you want to delete?") == true) {
        axiosInstance.delete(`/note/${this.tableData[index].id}`)
          .then(response => {
            this.getAll();
          })
          .catch(error => {
            console.log(error);
          });
      }

    },
    editRow: function (index) {
      this.$router.push({ name: 'AddUpdateNote', query: { noteId: this.tableData[index].id, isView: false } })
    },
    viewRow: function (index) {
      this.$router.push({ name: 'AddUpdateNote', query: { noteId: this.tableData[index].id, isView: true } })
    },
    getAll: function () {
      // let user = userStore();

      const id = localStorage.getItem('id');

      axiosInstance.get(`/note/getAll/${id}`)
        .then(response => {
          this.tableData = response.data;
        })
        .catch(error => {
          console.log(error);
        });

    },
    search: function () {
      // let user = userStore();
      // console.log(this.userId)
      const id = localStorage.getItem('id');
      axiosInstance.get(`/note/search?userId=${id}&title=${this.input}`)
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

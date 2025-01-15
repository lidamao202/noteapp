<template>
  <div class="flex">
    <div class="w-full p-8">
      <h2 class="text-2xl font-semibold text-center text-gray-800 mb-6">Note</h2>
      <div class="flex items-center space-x-2">
        <!-- Search Input -->
        <input type="text" v-model="input" placeholder="Search..."
          class="w-full py-2 px-4 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500" />

        <!-- Search Button -->
        <el-button type="primary" @click="search"
          class="px-4 py-2 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500">
          Search
        </el-button>
      </div>

      <div class="pt-8 pb-8">
        <el-button type="primary"
          class="p-8 py-2 bg-indigo-600 text-white font-semibold rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500"
          @click="onAddNew">Add New</el-button>
      </div>

      <div class="overflow-x-auto py-4">
        <el-table :data="tableData" style="width: 100%" class="rounded-lg shadow-lg overflow-hidden bg-white">
          <el-table-column prop="title" label="Title" width="180" class="bg-gray-100" />
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
      </div>


    </div>
  </div>

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
      console.log("userid="+id)

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


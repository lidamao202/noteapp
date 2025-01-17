<template>
  <div class="flex">
    <div class="w-full p-8">
      <h2 class="text-2xl font-semibold text-center text-gray-800 mb-6">Note</h2>
      <div class="flex items-center space-x-2">

        <!-- <el-input type="text" v-model="input" placeholder="Search..." class="w-full py-2 px-4 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500
          focus:border-indigo-500" />

        <el-button id="search-id" type="primary" @click="search"
          class="px-4 py-2 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500">
          Search
        </el-button> -->

        <div class="relative text-gray-600">
                  <el-input type="text" name="search" placeholder="Search notes..." class="w-full border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500
                  focus:border-indigo-500"/>
                  <el-button type="submit" class="absolute right-0">
                    <svg class="h-4 w-4 fill-current" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" id="Capa_1" x="0px" y="0px" viewBox="0 0 56.966 56.966" style="enable-background:new 0 0 56.966 56.966;" xml:space="preserve" width="512px" height="512px">
                      <path d="M55.146,51.887L41.588,37.786c3.486-4.144,5.396-9.358,5.396-14.786c0-12.682-10.318-23-23-23s-23,10.318-23,23  s10.318,23,23,23c4.761,0,9.298-1.436,13.177-4.162l13.661,14.208c0.571,0.593,1.339,0.92,2.162,0.92  c0.779,0,1.518-0.297,2.079-0.837C56.255,54.982,56.293,53.08,55.146,51.887z M23.984,6c9.374,0,17,7.626,17,17s-7.626,17-17,17  s-17-7.626-17-17S14.61,6,23.984,6z"/>
                    </svg>
                  </el-button>
                </div>

      </div>

      <!-- <div class="pt-8 pb-8">
        <el-button type="primary"
          class="p-8 py-2 bg-indigo-600 text-white font-semibold rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500"
          @click="onAddNew">Add New</el-button>
      </div> -->

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

<script lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { userStore } from '@/stores/userStore';
import { getAllNotes, deleteNote, searchNotes } from '@/services/apiService';


export default {
  setup() {
    const router = useRouter();
    const store = userStore();
    const tableData = ref([]);
    const input = ref("");
    const note = ref({});

    const getAll = async () => {
      try {
        const response = await getAllNotes();
        tableData.value = response.data;

      } catch (error) {
        console.log(error);
      }
    };

    const viewRow = (index: number) => {
      note.value = tableData.value[index];
      router.push({ name: 'AddUpdateNote', query: { noteId: note.value.id, isView: true } });
    };

    const editRow = (index: number) => {
      note.value = tableData.value[index];
      router.push({ name: 'AddUpdateNote', query: { noteId: note.value.id, isView: false } });
    };

    const deleteRow = async (index: number) => {
      if (confirm("Are you sure you want to delete?")) {
        note.value = tableData.value[index];
        try {
          await deleteNote(note.value.id);
          getAll();
        } catch (error) {
          console.log(error);
        }
      }
    };

    const onAddNew = () => {
      router.push({ name: 'AddUpdateNote', query: { isView: false } });
    };

    const search = async () => {
      const response = await searchNotes(input.value);
      tableData.value = response.data;

    }
    onMounted(() => {
      getAll();
    });

    return {
      tableData,
      input,
      viewRow,
      editRow,
      deleteRow,
      onAddNew,
      search
    };
  },
};
</script>

<template>
    <div class="flex items-center justify-center">
      <div class="w-full max-w-sm p-8 bg-white">
        <el-form label-width="auto" style="max-width: 600px">
          <el-form-item label="Title">
            <el-input v-model="form.title" />
          </el-form-item>
          <el-form-item label="Content">
            <el-input v-model="form.content" />
          </el-form-item>
          <el-form-item>
            <el-button v-if="!isView" type="primary" @click="onSubmit">Submit</el-button>
            <el-button type="primary" @click="onBackClick">Back</el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>
  </template>


<script lang="ts">
import { reactive, onMounted, ref } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { getNote, createNote, updateNote } from '@/services/apiService';

export default {
  setup() {
    const router = useRouter();
    const route = useRoute();
    const form = reactive({
      id: '',
      title: '',
      content: ''
    });
    const isView = ref(route.query.isView === 'true');

    const getUser = async () => {
      const noteId = route.query.noteId;
      if (noteId) {
        try {
          const response = await getNote(noteId);
          form.id = response.data.id;
          form.title = response.data.title;
          form.content = response.data.content;
        } catch (error) {
          console.log(error);
        }
      }
    };

    const onSubmit = async () => {
      try {
        if (form.id) {
          await updateNote(form.id, form);
        } else {
          await createNote({
            ...form
          });
        }
        router.push({ name: 'notelist' });
      } catch (error) {
        console.log(error);
      }
    };

    const onBackClick = () => {
      router.push({ name: 'notelist' });
    };

    onMounted(() => {
      getUser();
    });

    return {
      form,
      isView,
      onSubmit,
      onBackClick
    };
  }
};
</script>
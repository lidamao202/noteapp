<template>
  <div class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="w-full max-w-sm p-8 bg-white rounded-lg shadow-lg">
      <h2 class="text-2xl font-semibold text-center text-gray-800 mb-6">Register</h2>
      <el-form label-width="auto" style="max-width: 600px">
        <el-form-item label="User name">
          <el-input v-model="form.userName" />
        </el-form-item>
        <el-form-item label="Password">
          <el-input v-model="form.password" type="password" />
        </el-form-item>
        <el-form-item label="Confirm Password">
          <el-input v-model="form.confirmPassword" type="password" />
        </el-form-item>
        <el-form-item>
          <el-button
            class="w-full py-2 bg-indigo-600 text-white font-semibold rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="primary" @click="onSubmit">Submit</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script lang="ts">
import { reactive } from 'vue';
import { useRouter } from 'vue-router';
import { userStore } from '@/stores/userStore';
import { register } from '@/services/apiService';

export default {
  setup() {
    const form = reactive({
      userName: '',
      password: '',
      confirmPassword: ''
    });
    const router = useRouter();
    const store = userStore();

    const onSubmit = async () => {
      if (form.password === form.confirmPassword) {
        try {
          const response = await register(form.userName, form.password);
          store.setUser({ id: response.data.id, userName: response.data.userName });
          router.push({ path: '/' });
        } catch (error) {
          console.log(error);
        }
      }
    };

    return {
      form,
      onSubmit
    };
  }
};
</script>
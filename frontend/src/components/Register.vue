<!-- <template>
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
</script> -->

<template>
  <div class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="w-full max-w-sm p-8 bg-white rounded-lg shadow-lg">
      <el-form ref="dynamicFormRef" :model="formData" :rules="validationRules" @submit.prevent="onSubmit">
        <el-form-item label="Username" prop="username">
          <el-input v-model="formData.username" placeholder="Enter your username" />
        </el-form-item>



        <el-form-item label="Password" prop="password">
          <el-input v-model="formData.password" type="password" placeholder="Enter your password" />
        </el-form-item>
        <el-form-item label="Confirm Password" prop="confirmPassword">
          <el-input v-model="formData.confirmPassword" type="password" placeholder="Enter your confirm password" />
        </el-form-item>


        <el-form-item>
          <el-button type="primary" @click="onSubmit">Submit</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>

</template>

<script>
import { ref, reactive, watch } from "vue";
import { useRouter } from 'vue-router';
import { userStore } from '@/stores/userStore';
import { register } from '@/services/apiService';

export default {
  setup() {
    // Form data
    const formData = reactive({
      username: "",
      password: "",
      confirmPassword: "",
    });

    const router = useRouter();
    const store = userStore();

    const save = async () => {
      if (formData.password === formData.confirmPassword) {
        try {
          const response = await register(formData.username, formData.password);
          router.push({ path: '/' });
        } catch (error) {
          console.log(error);
        }
      }
    };

    // Reactive email requirement state
    const isUserNameRequired = ref(false);

    // Validation rules
    const validationRules = reactive({
      username: [
        { required: true, message: "Username is required", trigger: "blur" },
        { min: 3, message: "Username must be at least 3 characters", trigger: "blur" },
      ],
      password: [
        { required: true, message: "Password is required", trigger: "blur" },
        { min: 6, message: "Password must be at least 6 characters", trigger: "blur" },
      ],
      confirmPassword: [
        { required: true, message: "Confirm Password is required", trigger: "blur" }
      ],
    });

    // Watch for email requirement toggle
    watch(isUserNameRequired, (newValue) => {
      if (newValue) {
        validationRules.username.unshift({
          required: true,
          message: "User name is required",
          trigger: "blur",
        });
      } else {
        validationRules.username.shift(); // Remove the "required" rule
      }
    });

    // Form reference
    const dynamicFormRef = ref(null);

    // Form submission
    const onSubmit = () => {
      dynamicFormRef.value.validate((valid) => {
        if (valid) {
          save();
        } else {
          console.error("Form validation failed");
        }
      });
    };

    return {
      formData,
      validationRules,
      isUserNameRequired,
      dynamicFormRef,
      onSubmit,
    };
  },
};
</script>

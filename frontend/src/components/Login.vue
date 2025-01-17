<template>
  <div class="flex items-center justify-center min-h-screen bg-gray-100">
    <div class="w-full max-w-sm p-8 bg-white rounded-lg shadow-lg">
      <h2 class="text-2xl font-semibold text-center text-gray-800 mb-6">Login</h2>
      <el-form ref="dynamicFormRef" :model="formData" :rules="validationRules" @submit.prevent="onSubmit">
        <el-form-item label="Username" prop="username">
          <el-input id="username-id" v-model="formData.userName" placeholder="Enter your username" />
        </el-form-item>

        <el-form-item label="Password" prop="password">
          <el-input id="password-id" v-model="formData.password" type="password" placeholder="Enter your password" />
        </el-form-item>

        <el-form-item>
          <el-button id="login-id"
            class="w-full py-2 bg-indigo-600 text-white font-semibold rounded-md hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="primary" @click="onSubmit">Submit</el-button>
        </el-form-item>
        <div class="mt-4 text-left">
          <el-link type="primary" @click="onRegister" href="#"
            class="text-sm text-indigo-600 hover:underline">Register</el-link>
        </div>
      </el-form>
    </div>
  </div>

</template>

<script>
import { ref, reactive, watch } from "vue";
import { useRouter } from 'vue-router';
import { userStore } from '@/stores/userStore';
import { jwtDecode } from 'jwt-decode';
import { login } from '@/services/apiService';

export default {
  setup() {
    // Form data
    const formData = reactive({
      userName: "",
      password: "",
    });

    const router = useRouter();
    const store = userStore();

    // Reactive email requirement state
    const isUserNameRequired = ref(false);

    // Validation rules
    const validationRules = reactive({
      userName: [
        { required: true, message: "Username is required", trigger: "blur" }
      ],
      password: [
        { required: true, message: "Password is required", trigger: "blur" },
        { min: 6, message: "Password must be at least 6 characters", trigger: "blur" },
      ]
    });

    watch(isUserNameRequired, (newValue) => {
      if (newValue) {
        validationRules.userName.unshift({
          required: true,
          message: "User name is required",
          trigger: "blur",
        });
      } else {
        validationRules.userName.shift(); // Remove the "required" rule
      }
    });

    // Form reference
    const dynamicFormRef = ref(null);

    // Form submission
    const onSubmit = () => {
      dynamicFormRef.value.validate((valid) => {
        if (valid) {
          submit();
        } else {
          console.error("Form validation failed");
        }
      });
    };

    const submit = async () => {
      try {
        const response = await login(formData.userName, formData.password);
        const decoded = jwtDecode(response.data);
        store.setToken(response.data);
        store.setUser({ userName: decoded.email });
        //window.location.href = '/dashboard/notelist';
        router.push({ path: '/dashboard/notelist' });
      } catch (error) {
        console.log(error);
      }
    };

    const onRegister = () => {
      router.push({ path: '/register' });
    };


    return {
      formData,
      validationRules,
      isUserNameRequired,
      dynamicFormRef,
      onSubmit,
      onRegister,
    };
  },
};
</script>

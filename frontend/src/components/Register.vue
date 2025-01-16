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
      onSubmit
    };
  },
};
</script>

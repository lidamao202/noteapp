// src/axios.js
import axios from 'axios';
import { useRouter } from 'vue-router';
import { userStore } from '@/stores/userStore';

const axiosInstance = axios.create({
  baseURL: import.meta.env.VUE_APP_API_BASE_URL || 'https://localhost:52724/api', // Use environment variable for base URL
  timeout: 5000, // 5 seconds
});



// JWT Interceptor
axiosInstance.interceptors.request.use(
  (config) => {
    const store = userStore();
    const token = store.token;
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response Interceptor (optional, for handling errors globally)
axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    const router = useRouter();
    const store = userStore();

    if (error.response && error.response.status === 401) {
      // Handle token expiration or authentication error here
      store.logout();
      router.push({ name: 'login' }); // Redirect to login page
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
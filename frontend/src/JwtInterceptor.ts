// src/axios.js
import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://localhost:59916/api', // Replace with your API's base URL
});

// JWT Interceptor
axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('jwt_token'); // or use sessionStorage depending on your preference
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
    if (error.response && error.response.status === 401) {
      // Handle token expiration or authentication error here
      // For example, redirect to login page
      // router.push('/login');  // You can use Vue Router to navigate to login
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
import { defineStore } from 'pinia';
import { useRouter } from 'vue-router';

export const userStore = defineStore('user', {
  state: () => ({
    user: null as string | null,
    token: null as string | null
  }),
  persist: {
    enabled: true, // Enables persistence for this store
    strategies: [
      {
        key: 'user-store', // Key used in localStorage
        storage: localStorage, // or sessionStorage
      },
    ],
  },
  actions: {

    setUser(user:any) {
      this.user = user;
    },
    setToken(token: string) {
      this.token = token;
      localStorage.setItem('jwt_token',token);
    },
    logout() {
      const router = useRouter();
      this.user = "";
      this.token = "";
      window.location.href = "/";
    }
  },
  getters: {
    isAuthenticated: (state) => !!state.token,
  }
});



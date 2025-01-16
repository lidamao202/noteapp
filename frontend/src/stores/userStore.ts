import { defineStore } from 'pinia';

export const userStore = defineStore('user', {
  state: () => ({
    user: null as string | null,
    token: null as string | null
  }),
  actions: {

    setUser(user:any) {
      this.user = user;
    },
    setToken(token: string) {
      this.token = token;
    },
    logout() {
      this.user = "";
      this.token = "";
      //localStorage.removeItem('jwt_token');
    }
  },
  getters: {
    //getToken: (state) => state.token,
    isAuthenticated: (state) => !!state.token
  }
});

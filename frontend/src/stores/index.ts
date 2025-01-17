import { createStore } from "vuex";

const store = createStore({
    state: {
        sideBarOpen: false
    },
    getters: {
        sideBarOpen: ({ sideBarOpen }) => sideBarOpen
    },
    mutations: {
        toggleSidebar(state) {
            state.sideBarOpen = !state.sideBarOpen
        }
    },
    actions: {
        toggleSidebar({ commit }) {
            commit('toggleSidebar')
        }
    }
  });

  export default store;
import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import NoteListView from '../views/NoteListView.vue'
import Register from '../views/RegisterView.vue'
import AddUpdateNote from '../views/AddUpdateNoteView.vue'
import { userStore } from '@/stores/userStore';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/noteList',
      name: 'notelist',
      component: NoteListView,
      meta: { requiresAuth: true }
    },
    {
      path: '/register',
      name: 'register',
      component: Register,
    },
    {
      path: '/addUpdateNote',
      name: 'AddUpdateNote',
      component: AddUpdateNote,
      meta: { requiresAuth: true }
    }
  ],
});

router.beforeEach((to, from, next) => {
  const store = userStore();
  if (to.matched.some(record => record.meta.requiresAuth) && !store.isAuthenticated) {
    next({ name: 'home' });
  } else {
    next();
  }
});

export default router

import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import NoteListView from '../views/NoteListView.vue'
import AddUpdateNote from '../views/AddUpdateNoteView.vue'

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
    },
    {
      path: '/addUpdateNote',
      name: 'AddUpdateNote',
      component: AddUpdateNote,
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue'),
    },
  ],
})

export default router

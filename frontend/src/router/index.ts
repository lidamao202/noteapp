import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import NoteListView from '../views/NoteListView.vue'
import Register from '../views/RegisterView.vue'
import AddUpdateNote from '../views/AddUpdateNoteView.vue'
import Dashboard from '../components/Dashboard.vue'
import DashboardHome from '../views/Home.vue'


const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    //{ path: '/', redirect: { name: 'DashboardHome' } },
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    { path: '/dashboard', component: Dashboard, children: [
        { path: '/', redirect: { name: 'DashboardHome' } },
        { path: 'home', name: 'DashboardHome', component: DashboardHome },
        { path: 'noteList', name: 'notelist', component: NoteListView },
        { path: 'addUpdateNote', name: 'AddUpdateNote', component: AddUpdateNote },
      ]
    },
    // {
    //   path: '/',
    //   name: 'home',
    //   component: HomeView,
    // },
    // {
    //   path: '/noteList',
    //   name: 'notelist',
    //   component: NoteListView,
    //   meta: { requiresAuth: true }
    // },
    {
      path: '/register',
      name: 'register',
      component: Register,
    },
    // {
    //   path: '/addUpdateNote',
    //   name: 'AddUpdateNote',
    //   component: AddUpdateNote,
    //   meta: { requiresAuth: true }
    // }
  ],
});



export default router

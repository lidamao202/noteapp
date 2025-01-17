//import './assets/main.css'
import './assets/input.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import piniaPluginPersistedState from 'pinia-plugin-persistedstate';
import store from '@/stores/index'


import App from './App.vue'
import router from './router'



const app = createApp(App)
const pinia = createPinia();

app.use(ElementPlus)

pinia.use(piniaPluginPersistedState);
app.use(pinia)
app.use(store)

app.use(router)

app.mount('#app')

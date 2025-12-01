
import { createApp } from 'vue';
import App from './App.vue';
import {createRouter, createWebHistory} from 'vue-router';
import LogIn from './components/LogIn.vue';
import EmpList from './components/EmpList.vue';
const router = createRouter(({
    routes:[
        {path: "/login",component:LogIn,props:true},
        {path: "/",component:EmpList}
    ],history:createWebHistory()
}))

createApp(App).use(router).mount('#app')

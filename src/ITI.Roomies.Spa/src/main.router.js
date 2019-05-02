// Vue router setup
import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);



import requireAuth from './helpers/requireAuth';

// Components
import Home from   './components/Home.vue'
import Login from  './components/Login.vue'
import Logout from './components/Logout.vue'
import Calendar from './components/Calendar.vue'
import Collocation from './components/Collocation.vue'

import RoomiesCreate from './components/Roomies/RoomiesCreate.vue'
import RoomieProfil from './components/Roomies/Roomie.vue'
import checkRoomie from './components/Roomies/checkRoomie.vue'

import TaskColloc from './components/Task/TaskColloc.vue'
import TaskRoomie from './components/Task/TaskRoomie.vue'
import TaskCreate from './components/Task/TaskCreate.vue'

const routes = [
    { path: '/', component: Home, beforeEnter: requireAuth },
    
    { path: '/login', component: Login },
    { path: '/logout', component: Logout, beforeEnter: requireAuth },

    {path: '/roomies/invite', component: Login, beforeEnter: requireAuth},

    {path: '/roomies/calendar', component: Calendar, beforeEnter: requireAuth},

    {path: '/roomies/collocation', component: Collocation, beforeEnter: requireAuth},
    {path: '/roomies/collocation/:id?', component: Collocation, beforeEnter: requireAuth},

    { path: '/roomies/create', component: RoomiesCreate, beforeEnter: requireAuth },
    { path: '/roomies', component: RoomieProfil, beforeEnter: requireAuth },
    { path: '/roomies/:id?', component: RoomieProfil, beforeEnter: requireAuth },
    { path: '/checkRoomie', component: checkRoomie, beforeEnter: requireAuth },

    { path: '/task/colloc', component: TaskColloc, beforeEnter: requireAuth },
    { path: '/task/roomie', component: TaskRoomie, beforeEnter: requireAuth },
    { path: '/task/create', component: TaskCreate, beforeEnter: requireAuth },
    
];

export default new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
});
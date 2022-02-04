import { createWebHistory, createRouter } from 'vue-router'

import Home from './pages/Home.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/employees',
    name: 'Employees',
    component: () => import('./pages/Employee.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router

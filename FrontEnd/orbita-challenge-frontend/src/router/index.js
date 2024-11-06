import { createRouter, createWebHistory } from 'vue-router';
import AcademicModule from '@/views/AcademicModule.vue';
import EditStudent from '@/views/EditStudent.vue';
import AddStudent from '@/views/AddStudent.vue';

const routes = [
  {
    path: '/',
    name: 'AcademicModule',
    component: AcademicModule,
  },
  {
    path: '/edit/:id', 
    name: 'EditStudent',
    component: EditStudent,
  },
  {
    path: '/AddStudent', 
    name: 'AddStudent',
    component: AddStudent,
  },
 
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;

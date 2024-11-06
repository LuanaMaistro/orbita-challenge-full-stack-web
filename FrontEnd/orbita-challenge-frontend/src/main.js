// src/main.js

import { createApp } from 'vue';
import App from './App.vue';
import router from './router'; 
import VueTheMask from 'vue-the-mask';
import './main.css'; // Importa o CSS global
import '@fortawesome/fontawesome-free/css/all.css';



// Importando o Vuetify
import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';

const vuetify = createVuetify({
  components,
  directives,
});

const app = createApp(App);


app.use(vuetify);
app.use(router); 
app.use(VueTheMask);
app.mount('#app');

// src/main.js

import { createApp } from 'vue';
import App from './App.vue';
import router from './router';  // Certifique-se de que o caminho está correto

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

// Usando o Vuetify na aplicação
app.use(vuetify);
app.use(router); // Mover essa linha antes de app.mount
app.mount('#app');

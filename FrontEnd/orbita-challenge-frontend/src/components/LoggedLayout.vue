<template>
    <v-app>
      <v-container fluid class="background-color">
        <v-row>
          <!-- Menu lateral fixo -->
          <v-col cols="3">
            <v-navigation-drawer app permanent color="white">
              <v-list>
                <!-- Logo e título do módulo -->
                <v-list-item>
                  <img alt="Vue logo" src="@/assets/LogoGrupoA.png" />
                </v-list-item>
                <v-divider></v-divider>
  
                <!-- Título do módulo -->
                <v-list-item>
                  <v-list-item-title class="text-h6 font-weight-bold title-logged-layout">Módulo Acadêmico</v-list-item-title>
                </v-list-item>
  
                <!-- Menu Alunos -->
                <v-list-item class="menu-item">
                  <v-list-item-title class="menu-title">Alunos</v-list-item-title>
                </v-list-item>
              </v-list>
            </v-navigation-drawer>
          </v-col>
  
          <!-- Área de conteúdo variável -->
          <v-col cols="9">
            <!-- Aqui renderiza o conteúdo da página atual -->
            <slot></slot>
          </v-col>
        </v-row>
      </v-container>
    </v-app>
  </template>
  
  <script>
  export default {
    name: "LoggedLayout",
  };
  </script>
  
  <style scoped>
  .title-logged-layout {
    color: #01b0b6
  }
  .v-navigation-drawer {
    border-right: 1px solid #ccc;
  }
  
  .background-color{
    background-color: #dcdcdc;
    min-height: 100vh;
  }
  
  .menu-item {
    padding: 10px 20px;
    transition: background-color 0.3s;
  }
  
  .menu-title {
    font-weight: bold;
    color:#ff203b
  }
  
  .menu-item:hover {
    background-color: rgba(0, 0, 0, 0.1);
    border-radius: 4px;
  }
  </style>
  
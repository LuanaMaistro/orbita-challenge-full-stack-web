<template>
    <v-snackbar v-model="internalVisible" :timeout="3000" :color="isSuccess ? 'success' : 'error'">
      {{ message }}
      <template v-slot:action="{ attrs }">
        <v-btn text v-bind="attrs" @click="close">Fechar</v-btn>
      </template>
    </v-snackbar>
  </template>
  
  <script>
  export default {
    props: {
      visible: {
        type: Boolean,
        default: false
      },
      message: {
        type: String,
        default: ''
      },
      color: {
        type: String,
        default: 'success'
      },
      isSuccess: {
      type: Boolean,
      default: true,
    },
    },
    data() {
      return {
        internalVisible: this.visible
      };
    },
    watch: {
      visible(newValue) {
        this.internalVisible = newValue;
      },
      internalVisible(newValue) {
        this.$emit('update:visible', newValue);
      }
    },
    methods: {
      close() {
        this.internalVisible = false;
      }
    }
  };
  </script>
  
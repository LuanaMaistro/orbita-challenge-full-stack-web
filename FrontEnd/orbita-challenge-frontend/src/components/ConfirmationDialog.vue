<template>
  <v-dialog v-model="dialog" max-width="400">
    <v-card>
      <v-card-title class="headline">{{ title }}</v-card-title>
      <v-card-text>
        {{ content }}
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="grey" text @click="cancel">{{ cancelText }}</v-btn>
        <v-btn color="red" text @click="confirm">{{ confirmText }}</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
export default {
  props: {
    student: Object,
    visible: Boolean,
    title: {
      type: String,
      default: 'Confirmar Ação'
    },
    content: {
      type: String,
      default: 'Tem certeza que deseja realizar esta ação?'
    },
    cancelText: {
      type: String,
      default: 'Cancelar'
    },
    confirmText: {
      type: String,
      default: 'Excluir'
    }
  },
  emits: ['confirm', 'close'],
  computed: {
    dialog: {
      get() {
        return this.visible;
      },
      set(value) {
        this.$emit('close', value);
      }
    }
  },
  methods: {
    confirm() {
      this.$emit('confirm', this.student);
      this.dialog = false;
    },
    cancel() {
      this.dialog = false;
    }
  }
};
</script>

<style scoped></style>

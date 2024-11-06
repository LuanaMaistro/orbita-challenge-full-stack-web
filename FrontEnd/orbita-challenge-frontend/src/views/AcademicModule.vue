<template>
  <LoggedLayout>
    <v-app>
      <v-col>
        <v-card class="mb-4">
          <v-card-title>Consulta de alunos</v-card-title>
          <v-card-text>
            <v-row>
              <v-col cols="8">
                <v-text-field label="Digite sua busca" v-model="search" dense>
                </v-text-field>
              </v-col>
              <v-col cols="2">
                <v-btn color="#01b0b6" block @click="searchStudent">Pesquisar</v-btn>
              </v-col>
              <v-col cols="2">
                <v-btn color="#01b0b6" block @click="createStudent">Cadastrar</v-btn>
              </v-col>
            </v-row>
          </v-card-text>
        </v-card>

        <v-data-table :items="alunos" :headers="headers" class="elevation-1">

          <template v-slot:item="{ item }">
            <tr>
              <td>{{ item.ra }}</td>
              <td>{{ item.name }}</td>
              <td>{{ item.cpf }}</td>
              <td>

                <v-btn small text color="#01b0b6" @click="editStudent(item)">
                  <v-icon>fa fa-pencil-alt</v-icon> 
                </v-btn>

                <v-btn small text color="#ff203b" @click="openConfirmationDialog(item)">
                  <v-icon>fa fa-trash-alt</v-icon> 
                </v-btn>
              </td>
            </tr>
          </template>
        </v-data-table>

        <!-- Diálogo de Confirmação -->
        <v-dialog v-model="dialog" max-width="400">
          <v-card>
            <v-card-title class="headline">Confirmar Exclusão</v-card-title>
            <v-card-text>
              Tem certeza que deseja excluir o aluno com o RA: {{ clickedStudent.ra }}?
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="grey" text @click="dialog = false">Cancelar</v-btn>
              <v-btn color="red" text @click="deleteStudent(clickedStudent.ra)">Excluir</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>

        <SnackBar v-model:visible="snackbarVisible" :message="snackbarMessage" :isSuccess="snackbarIsSuccess" />

      </v-col>
    </v-app>
  </LoggedLayout>
</template>

<script>
import { getAllStudents, searchStudents, deleteStudentByRA } from '@/api';
import SnackBar from '@/components/SnackBar.vue';
import LoggedLayout from '@/components/LoggedLayout.vue';


export default {
  components: {
    SnackBar,
    LoggedLayout
  },
  data() {
    return {
      search: '',
      alunos: [],
      headers: [
        { title: 'Registro Acadêmico', align: 'center', key: 'ra' },
        { title: 'Nome',align: 'center', key: 'name' },
        { title: 'CPF',align: 'center', key: 'cpf' },
        { title: 'Ações',align: 'center', key: 'actions', sortable: false },
      ],
      clickedStudent: null,
      dialog: false,
      snackbarVisible: false,
      snackbarMessage: '',
      snackbarIsSuccess: true
    };
  },
  methods: {
    createStudent() {
      this.$router.push({ name: 'AddStudent' });

    },
    openConfirmationDialog(student) {
      this.clickedStudent = student;
      this.dialog = true;
    },
    editStudent(student) {
      this.$router.push({ name: 'EditStudent', params: { id: student.ra } });
    },
    async deleteStudent(student) {
      try {
        const response = await deleteStudentByRA(student);

        if (response.data.success) {
          this.snackbarMessage = response.data.message;
          this.snackbarVisible = true;
          this.dialog = false; 
          this.fetchStudents(); 
        }
        else {
          this.snackbarMessage = response.data.message;
          this.snackbarIsSuccess = false
          this.snackbarVisible = true;
        }
      } catch (error) {
        this.snackbarMessage = error;
        this.snackbarIsSuccess = false
        this.snackbarVisible = true;
      }
    },
    async searchStudent() {
      if (this.search) {
        try {
          const response = await searchStudents(this.search);

          if (response.data) {
            this.alunos = response.data.data;
          } else {
            this.alunos = [];
            this.snackbarMessage = response.data.message;
            this.snackbarIsSuccess = false
            this.snackbarVisible = true;
          }
        } catch (error) {
          this.alunos = [];
          this.snackbarMessage = error;
          this.snackbarIsSuccess = false
          this.snackbarVisible = true;
        }
      }
      else {
        this.fetchStudents();
      }
    },

    async fetchStudents() {
      try {
        const response = await getAllStudents();
        if (response.data.success)
          this.alunos = response.data.data;

      } catch (error) {
        this.snackbarMessage = error;
        this.snackbarIsSuccess = false
        this.snackbarVisible = true; 
      }
    },
  },

  mounted() {
    this.fetchStudents();
  },
};
</script>

<style scoped></style>

<template>
  <LoggedLayout>
  <v-app>
        <!-- Conteúdo Principal -->
        <v-col >
          <!-- Barra de Pesquisa -->
          <v-card class="mb-4">
            <v-card-title>Consulta de alunos {{ this.search }}</v-card-title>
            <v-card-text>
              <v-row>
                <v-col cols="8">
                  <v-text-field 
                    label="Digite sua busca" 
                    v-model="search" 
                    dense>
                  </v-text-field>
                </v-col>
                <v-col cols="2">
                  <v-btn color="primary" block @click="pesquisar">Pesquisar</v-btn>
                </v-col>
                <v-col cols="2">
                  <v-btn color="secondary" block @click="createStudent">Cadastrar</v-btn>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>

          <!-- Tabela de Alunos -->
          <v-data-table
            :items="alunos"
            :headers = "headers"
            class="elevation-1"
          >

            <!-- Slot para o corpo da tabela -->
            <template v-slot:item="{ item }">
              <tr>
                <td>{{ item.ra }}</td>
                <td>{{ item.name }}</td>
                <td>{{ item.cpf }}</td>
                <td>
                  <v-btn small text color="primary" @click="editarAluno(item)">[Editar]</v-btn>
                  <v-btn small text color="error" @click="openConfirmationDialog(item)">[Excluir]</v-btn>
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

             <!-- Snackbar para Mensagem de Sucesso -->
             <SnackBar
              v-model:visible="snackbarVisible"
              :message="snackbarMessage"
              :isSuccess="snackbarIsSuccess"
            />

        </v-col>
  </v-app>
</LoggedLayout>
</template>

<script>
import { getAllStudents,searchStudents,deleteStudentByRA } from '@/api'; // ajuste o caminho conforme necessário
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
       // Definindo os cabeçalhos desejados
       headers: [
        { title: 'Registro Acadêmico', key: 'ra' },
        { title: 'Nome', key: 'name' },
        { title: 'CPF', key: 'cpf' },
        { title: 'Ações', key: 'actions', sortable: false },
      ],
      clickedStudent: null,
      dialog: false,
      snackbarVisible: false,
      snackbarMessage: '',
      snackbarIsSuccess: true
    };
  },
  methods: {
    createStudent(){
      this.$router.push({ name: 'AddStudent'});

    },
    openConfirmationDialog(student) {
      this.clickedStudent = student;
      this.dialog = true;
    },
    editarAluno(student) {
    // Redireciona para a tela de edição do aluno, passando o ID
    this.$router.push({ name: 'EditStudent', params: { id: student.ra } });
  },
  async deleteStudent(student) {
      try {
          const response = await deleteStudentByRA(student); 

          if (response.data.success){
            this.snackbarMessage = response.data.message; 
            this.snackbarVisible = true; 
            this.dialog = false; // Fecha o diálogo de confirmação
            this.fetchStudents(); // Atualiza a lista de alunos após a exclusão
          }
          else{
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
    async pesquisar() {
      if(this.search){
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
      else{
        this.fetchStudents();
      }
  },

    async fetchStudents() {
      try {
        const response = await getAllStudents();
        if(response.data.success)
        this.alunos = response.data.data;
        
      } catch (error) {
        this.snackbarMessage = error;
        this.snackbarIsSuccess = false
        this.snackbarVisible = true; // Abre o snackbar
      }
    },
  },

  mounted() {
    this.fetchStudents();
  },
};
</script>

<style scoped>


</style>

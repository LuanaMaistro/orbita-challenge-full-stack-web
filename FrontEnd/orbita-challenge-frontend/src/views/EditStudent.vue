<template>
  <LoggedLayout>
    <v-card class="mb-4">
      <v-card-title class="text-center">Editar Aluno </v-card-title>
    </v-card>
    <v-card class="mb-4 d-flex justify-center">
      <v-card-text>
        <v-row class="d-flex justify-center">
          <v-col cols="12" md="6">
            <v-col>
              <label class="form-label">Nome</label>
              <v-text-field v-model="nome" placeholder="Informe o nome completo" dense></v-text-field>
            </v-col>
            <v-col>
              <label class="form-label">RA</label>
              <v-text-field v-model="ra" dense readonly></v-text-field>
            </v-col>
            <v-col>
              <label class="form-label">CPF</label>
              <v-text-field v-model="cpf" v-mask="'###.###.###-##'" placeholder="Informe o CPF" dense></v-text-field>
            </v-col>
            <v-col>
              <label class="form-label">Email</label>
              <v-text-field v-model="email" placeholder="Informe o email" dense></v-text-field>
            </v-col>
          </v-col>
        </v-row>
        <v-row class="d-flex justify-center mt-4">
          <v-btn color="#01b0b6" @click="editarAluno">Salvar</v-btn>
          <v-btn color="#01b0b6" @click="backHome" class="ml-2">Cancelar</v-btn>
        </v-row>
      </v-card-text>
    </v-card>
  </LoggedLayout>

  <SnackBar v-model:visible="snackbarVisible" :message="snackbarMessage" :isSuccess="snackbarIsSuccess" />
</template>

<script>
import LoggedLayout from '@/components/LoggedLayout.vue';
import { editStudent, getStudentByRA } from '@/api';
import SnackBar from '@/components/SnackBar.vue';
 
export default {
  components: { LoggedLayout, SnackBar },
  data() {
    return {
      nome: '',
      ra: '',
      cpf: '',
      email: '',
      snackbarMessage: '',
      snackbarIsSuccess: true,
      snackbarVisible: false
    };
  },
  mounted() {
    this.getStudentByRA();
  },
  methods: {
    backHome() {
      this.$router.push({ name: "AcademicModule" });
    },
    async editarAluno() {
            const studentData = {
                Name: this.nome,
                RA: this.ra,
                CPF: this.cpf.replace(/\D/g, ''),
                Email: this.email,
            };

            try {
                const response = await editStudent(studentData);
                if (response.data.success) {
                    this.snackbarMessage = response.data.message;
                    this.snackbarVisible = true;
                    this.snackbarIsSuccess = true

                    setTimeout(() => {
                        this.$router.push({ name: "AcademicModule" });
                    }, 3000);

                }
                else {
                    this.snackbarMessage = response.data.message;
                    this.snackbarIsSuccess = false
                    this.snackbarVisible = true;
                }
            } catch (error) {
                if (error.response && error.response.data.errors) {
                    const validationErrors = error.response.data.errors;
                    const firstErrorMessage = Object.values(validationErrors)[0][0];
                    this.snackbarMessage = firstErrorMessage;
                    this.snackbarIsSuccess = false;  
                    this.snackbarVisible = true;    
                } else {
                    this.snackbarMessage = 'Ocorreu um erro ao salvar os dados.';
                    this.snackbarIsSuccess = false;
                    this.snackbarVisible = true;
                }
            }
        },

    async getStudentByRA() {
      try {
        const response = await getStudentByRA(this.$route.params.id)
        const studentData = response.data.data;
        this.nome = studentData.name;
        this.ra = studentData.ra;
        this.cpf = studentData.cpf;
        this.email = studentData.email;
      } catch (error) {
        console.error("Erro ao buscar aluno:", error);
      }
    },
  },
};
</script>

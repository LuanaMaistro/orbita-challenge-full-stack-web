<template>
    <LoggedLayout>
        <v-card class="mb-4">
            <v-card-title class="text-center">Adicionar Aluno</v-card-title>
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
                            <v-text-field v-model="ra" placeholder="Informe o RA" dense></v-text-field>
                        </v-col>
                        <v-col>
                            <label class="form-label">CPF</label>
                            <v-text-field v-model="cpf" placeholder="Informe o CPF" dense></v-text-field>
                        </v-col>
                        <v-col>
                            <label class="form-label">Email</label>
                            <v-text-field v-model="email" placeholder="Informe o email" dense></v-text-field>
                        </v-col>
                    </v-col>
                </v-row>
                <!-- Botões abaixo do formulário -->
                <v-row class="d-flex justify-center mt-4">
                    <v-btn color="primary" @click="saveStudent">Salvar</v-btn>
                    <v-btn color="primary" @click="backHome" class="ml-2">Cancelar</v-btn>
                </v-row>
            </v-card-text>
        </v-card>
    </LoggedLayout>

    <SnackBar v-model:visible="snackbarVisible" :message="snackbarMessage" :isSuccess="snackbarIsSuccess" />
</template>

<script>
import LoggedLayout from '@/components/LoggedLayout.vue';
import { addStudent } from '@/api';
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
    methods: {
        backHome() {
            this.$router.push({ name: "AcademicModule" });
        },
        async saveStudent() {
            // Dados capturados dos campos de entrada
            const studentData = {
                Name: this.nome,
                RA: this.ra,
                CPF: this.cpf,
                Email: this.email,
            };

            try {
                // Enviando os dados ao backend usando a função `addStudent`
                const response = await addStudent(studentData);
                if (response.data.success) {
                    this.snackbarMessage = response.data.message;
                    this.snackbarVisible = true;

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
                    // Acesse os erros do backend
                    const validationErrors = error.response.data.errors;
                    // Você pode querer exibir os erros de forma personalizada, aqui estamos apenas pegando o primeiro erro
                    const firstErrorMessage = Object.values(validationErrors)[0][0];
                    console.log('Erro de validação:', firstErrorMessage);

                    // Exibe a primeira mensagem de erro no snackbar
                    this.snackbarMessage = firstErrorMessage;
                    this.snackbarIsSuccess = false;  // Indica que é um erro
                    this.snackbarVisible = true;     // Exibe o snackbar
                } else {
                    // Se não for um erro de validação, apenas exibe o erro genérico
                    console.log('Erro geral:', error);
                    this.snackbarMessage = 'Ocorreu um erro ao salvar os dados.';
                    this.snackbarIsSuccess = false;
                    this.snackbarVisible = true;
                }
            }
        },
    },
};
</script>

<style scoped>
.form-label {
    display: block;
    font-weight: 500;
    margin-bottom: 4px;
    text-align: left;
}
</style>
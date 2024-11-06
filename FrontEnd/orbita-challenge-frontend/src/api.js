import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:44353/api', 
  headers: {
    'Content-Type': 'application/json',
  },
});

//buscar todos os estudantes
export const getAllStudents = () => api.get('/Student');

//Buscar estudantes por pesquisa
export const searchStudents = (searchTerm) => {
  return api.get('/Student/search', {
    params: { searchTerm }, // Passa o parâmetro de busca
  });
};
export const deleteStudentByRA = (ra) => api.post(`/Student/delete/${ra}`);

// Função para adicionar um estudante
export const addStudent = (student) => api.post('/Student', student);


// export const addAluno = (aluno) => api.post('/Student', aluno);
// export const addAluno = (aluno) => api.post('/Student', aluno);
// export const updateAluno = (aluno) => api.put(`/alunos/${aluno.id}`, aluno);


import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:44353/api', 
  headers: {
    'Content-Type': 'application/json',
  },
});

export const getAllStudents = () => api.get('/Student');

export const searchStudents = (searchTerm) => {
  return api.get('/Student/search', {
    params: { searchTerm },
  });
};
export const deleteStudentByRA = (ra) => api.post(`/Student/delete/${ra}`);

// Função para adicionar um estudante
export const addStudent = (student) => api.post('/Student', student);


export const getStudentByRA = (ra) => api.get(`/Student/GetByRA/${ra}`);

export const editStudent = (student) => api.post('/Student/edit', student);


// export const getStudentByRA = (ra) => api.get(`/Student/GetByRA/${ra}`);





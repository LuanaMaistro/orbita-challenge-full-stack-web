# OrbitaChallenge

# **Arquitetura Utilizada**

**Backend**:

Para o desenvolvimento do Backend, foi utilizada uma arquitetura inspirada no padrão MVC  (Model-View-Controller), mas com alguns pontos adaptados para que a estrutura atendesse melhor às necessidades do projeto. Em vez de uma View tradicional foi utilizado o padrão de **Repository** e **Interface**, que permitiram uma organização mais voltada para uma API.

- **Controller**: É o responsável por receber as requisições e encaminhá-las de forma organizada. Ele interpreta as entradas e chama o Repository, que é onde realmente acontecem as operações com os dados.
- **Repository**: Aqui é onde as interações diretas com o banco de dados acontecem. Ele serve como uma camada intermediária entre o banco e o restante da aplicação, centralizando as operações e facilitando a manutenção.
- **Interface**: Funciona como um “guia”, definindo o que o Repository precisa oferecer, garantindo que as implementações sigam o mesmo padrão. Além disso, essa abordagem torna o código mais fácil de testar, pois é possível simular o Repository sem precisar acessar o banco de dados real.

**FrontEnd**:

O projeto segue uma arquitetura modular, facilitando a manutenção e escalabilidade. 

- Pasta assets: Contém os membros estáticos, imagens e ícones do projeto.
- Pasta router: Responsável pela configuração de rotas do projeto (arquivo index.js).
- Pasta components: Contém alguns componentes que são utilizados em várias páginas como por exemplo o SnackBar.vue.
- Pasta views: Contém as views principais do sistema como por exemplo a AcademicModule.vue e a AddStudent.vue.

# Bibliotecas de terceiros utilizadas

**Backend**

- **Microsoft.Extensions.Logging** (ILogger): Para gerenciamento e registro de logs.
- **Swashbuckle.AspNetCore** (Swagger): Para documentação e visualização da API.
- **Microsoft.EntityFrameworkCore**: Integração com o banco de dados.
- **Pomelo.EntityFrameworkCore.MySql**: Adaptador do MySQL para Entity Framework Core.
- **FluentValidation**: Para validação de dados.
- **xUnit**: Biblioteca de testes unitários.
- **Moq:** Para mocking em testes.

**FrontEnd**

- **Vue.js**
- **Vuetify**: Framework de UI para Vue.js.
- **Vue Router**: Rotas da aplicação.
- **Axios**: Responsável por fazer as requisições HTTP.
- **vue-the-mask** : Inclusão de máscara no campo CPF.
- **FontAwesome**: Ícones.

# O que melhoraria se tivesse mais tempo

- Implementar um sistema de login utilizando **JWT (JSON Web Token)**. Que ajudaria a proteger o acesso às rotas mais sensíveis, garantindo que apenas usuários autenticados possam interagir com a aplicação, além de personalizar também as permissões de acesso, para que funções específicas sejam liberadas de acordo com o perfil de cada usuário.
- Aplicaria responsividade no frontend, e aprimoraria o design e a interface num geral para proporcionar uma experiência mais clara e intuitiva, além de melhorar a usabilidade da aplicação.
- Também gostaria de componentizar melhor o FrontEnd, estilizando e trabalhando mais com os componentes do Vuetify.

# Padrões de commit

Para o projeto escolhi seguir os padrões de commit do [iuricode/padroes-de-commits](https://github.com/iuricode/padroes-de-commits) para deixar o histórico bem organizado e fácil de entender. 

Cada commit tem uma tag específica, como `feat` para novas funcionalidades ou `fix` para correções, além de uma descrição clara sobre o que foi feito, facilitando qualquer pessoa acompanhar as mudanças e entender a evolução do projeto
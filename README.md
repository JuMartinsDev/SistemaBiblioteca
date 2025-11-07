# Sistema de Empréstimo de Livros

Este projeto tem como objetivo implementar um sistema de controle de empréstimos de livros, aplicando regras de negócio e boas práticas de desenvolvimento em C# com Entity Framework e banco de dados em memória.

Dupla:
Julia Martins de Almeida Antunes RM98601
Ana Luisa Giaquinto Zólio RM99348
---

## Regras Implementadas

- Usuários não podem ter mais de 3 empréstimos ativos ao mesmo tempo.  
- Livros emprestados não podem ser reservados.  
- Multa é calculada automaticamente com base no atraso (R$ 1,00 por dia).  
- Professores têm prazo de empréstimo maior que alunos.  
- Bloqueio de novos empréstimos para usuários com multas pendentes.  
- Atualização automática do status do livro após empréstimo e devolução.  
- Tratamento de exceções personalizadas para erros de negócio.  

---

## Diagrama Simplificado das Entidades
+--------------------+
|       LIVRO        |
+--------------------+
| ISBN : string (PK) |
| Titulo : string    |
| Autor : string     |
| Categoria : enum   | -> {FICCAO, TECNICO, DIDATICO}
| Status : enum      | -> {DISPONIVEL, EMPRESTADO, RESERVADO}
| DataCadastro : DateTime |
+---------+----------+
          |
          | 1..*
          |
+---------v----------+
|    EMPRESTIMO      |
+--------------------+
| Id : int (PK)      |
| ISBNLivro : string | -> LIVRO.ISBN
| IdUsuario : int    | -> USUARIO.Id
| DataEmprestimo : DateTime |
| DataPrevista : DateTime  |
| DataDevolucao : DateTime? |
| Status : enum      | -> {ATIVO, FINALIZADO, ATRASADO}
+---------+----------+
          |
          | 0..1
          |
+---------v----------+
|       MULTA        |
+--------------------+
| IdEmprestimo : int (PK, FK) | -> EMPRESTIMO.Id
| Valor : decimal             |
| Status : enum               | -> {PENDENTE, PAGA}
+-----------------------------+

+--------------------+
|      USUARIO       |
+--------------------+
| Id : int (PK)      |
| Nome : string      |
| Email : string     |
| Tipo : enum        | -> {ALUNO, PROFESSOR, FUNCIONARIO}
| DataCadastro : DateTime |
+--------------------+

---

## Exemplos de Requisições da API (Evidências de Funcionamento)

### Cadastrar um Livro
**POST /api/livros**

{
  "isbn": "978-8595081512",
  "titulo": "Clean Code",
  "autor": "Robert C. Martin",
  "categoria": "TECNICO",
  "status": "DISPONIVEL"
}


**Resposta**


{
  "isbn": "978-8595081512",
  "titulo": "Clean Code",
  "autor": "Robert C. Martin",
  "status": "DISPONIVEL"
}

---

### Cadastrar um Usuário

**POST /api/usuarios**

{
  "nome": "Carlos Souza",
  "email": "carlos@email.com",
  "tipoUsuario": "ALUNO"
}


**Resposta**


{
  "id": 1,
  "nome": "Carlos Souza",
  "tipoUsuario": "ALUNO",
  "dataCadastro": "2025-11-07"
}


---

### Registrar um Empréstimo

**POST /api/emprestimos**


{
  "idUsuario": 1,
  "isbnLivro": "978-8595081512"
}


**Resposta**


{
  "id": 10,
  "usuario": "Carlos Souza",
  "livro": "Clean Code",
  "dataEmprestimo": "2025-11-07",
  "dataDevolucaoPrevista": "2025-11-14",
  "status": "ATIVO"
}


---

### Registrar Devolução com Atraso

**PUT /api/emprestimos/10/devolver**

{
  "dataDevolucao": "2025-11-18"
}


**Resposta**

{
  "id": 10,
  "diasAtraso": 4,
  "multa": 4.0,
  "status": "FINALIZADO"
}


---

###  Tentativa de Empréstimo de Livro Indisponível

**POST /api/emprestimos**

{
  "idUsuario": 2,
  "isbnLivro": "978-8595081512"
}


**Resposta**

{
  "erro": "LivroIndisponivelException",
  "mensagem": "O livro informado não está disponível para empréstimo."
}


---

###  Listar Empréstimos em Atraso

**GET /api/relatorios/atrasados**

[
  {
    "usuario": "Carlos Souza",
    "livro": "Clean Code",
    "diasAtraso": 4,
    "multa": 4.0
  }
]


---

## Como Executar o Projeto

1. **Clonar o repositório**

   git clone https://github.com/SEU_USUARIO/biblioteca-api.git
   

2. **Entrar na pasta do projeto**

   cd biblioteca-api
   

3. **Executar o projeto**

   dotnet run
   

4. **A aplicação ficará disponível em**

   http://localhost:5000
   

5. **Testar os endpoints**

---

## Banco de Dados

O projeto utiliza o **Entity Framework Core** com banco de dados **em memória (InMemoryDatabase)** para facilitar os testes e não exigir instalação de nenhum banco externo.

Configuração presente na Program.cs:

builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseInMemoryDatabase("BibliotecaDB"));



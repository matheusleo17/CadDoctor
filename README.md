# 🩺 CadDoctor API

API para gerenciamento de consultas médicas, desenvolvida em **.NET 9.0** com **Swagger** e banco de dados **SQL Server**.

---

## 📖 Descrição
O **CadDoctor** é uma API REST que permite o cadastro, atualização, listagem e remoção de consultas médicas.  
O projeto foi estruturado com boas práticas de camadas, utilizando Entity Framework para acesso ao banco de dados.

---

## 🛠 Tecnologias Utilizadas
- **.NET 9.0**
- **C#**
- **Entity Framework Core**
- **SQL Server**
- **Swagger** (documentação e testes de endpoints)
- **Hash de senhas
- **Token JWT

---

## 📂 Estrutura do Projeto
CadDoctor/
├── Api
│ └── Controllers/ # Endpoints da API (Consultas, Médicos, Pacientes, Autenticação)
├── Application
│ ├── DTO/ # Objetos de transferência de dados
│ ├── Interfaces/ # Interfaces dos serviços
│ └── Services/ # Implementação das regras de negócio
├── Domain
│ └── Models/ # Modelos da aplicação (Consultas, Médicos, Pacientes)
├── Infrastructure
│ ├── AppDbContext.cs # Contexto do banco de dados
│ └── Migrations/ # Controle de migrações do EF Core

---

## 📦 Instalação e Execução

### 1️⃣ Clonar o repositório
```bash
git clone https:https://github.com/matheusleo17/CadDoctor.git
cd caddoctor
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=CadDoctorDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

# OrderWebAPI

API REST em .NET 8 para gerenciamento de ordens de serviço, com autenticação JWT, controle de categorias e geração de PDF.

## 🚀 Tecnologias
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Serilog
- xUnit + Moq
- Swagger

## 🔑 Funcionalidades
- Registro e login de usuários
- Autenticação e autorização com JWT
- CRUD de categorias e pedidos
- Geração de PDF de pedidos
- Rate Limiting
- Logs estruturados com Serilog
- Testes unitários com banco em memória (EF InMemory)

## 📌 Endpoints principais
### Auth
- `POST /Auth/Register` – Cadastro de novos usuários
- `POST /Auth/Login` – Login e geração de token JWT

### Order
- `GET /Orders` – Lista todas as ordens
- `GET /Orders/{id}` – Busca ordem por ID
- `POST /Orders` – Cria nova ordem
- `PUT /Orders/{id}` – Atualiza ordem existente
- `DELETE /Orders/{id}` – Remove ordem
- `GET /Orders/Printer/{id}` – Gera PDF da ordem

### Category
- `GET /Categorys` – Lista todas as categorias
- `GET /Categorys/{id}` – Busca categoria por ID
- `POST /Categorys` – Cria nova categoria
- `DELETE /Categorys/{id}` – Remove categoria

## ▶️ Como executar
1. Configure o arquivo `appsettings.Development.json`
2. Execute as migrations: `dotnet ef database update`
3. Inicie o projeto: `dotnet run`
4. Acesse o Swagger em `/swagger`
5. Cadastre um usuário em `/api/Auth/Register`
6. Faça login em `/api/Auth/Login` para obter o token JWT


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
- `GET /Order/GetAllOrders` – Lista todas as ordens
- `GET /Order/GetOrderById/{id}` – Busca ordem por ID
- `POST /Order/CreateOrder` – Cria nova ordem
- `PUT /Order/UpdateOrder/{id}` – Atualiza ordem existente
- `DELETE /Order/DeleteOrder/{id}` – Remove ordem
- `GET /Order/PrinterOrder/{id}` – Gera PDF da ordem

### Category
- `GET /Category/GetAllCategories` – Lista todas as categorias
- `GET /Category/GetCategoryById/{id}` – Busca categoria por ID
- `POST /Category/CreateCategory` – Cria nova categoria
- `DELETE /Category/DeleteCategory/{id}` – Remove categoria

## ▶️ Como executar
1. Configure o arquivo `appsettings.Development.json`
2. Execute as migrations: `dotnet ef database update`
3. Inicie o projeto: `dotnet run`
4. Acesse o Swagger em `/swagger`
5. Cadastre um usuário em `/api/auth/register`
6. Faça login em `/api/auth/login` para obter o token JWT


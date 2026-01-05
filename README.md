# OrderWebAPI

API REST desenvolvida em .NET para gerenciamento ordem de serviços, com autenticação JWT,
controle de categorias e geração de PDF.

## Tecnologias Utilizadas
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Serilog
- xUnit e Moq
- Swagger

## Funcionalidades
- Registro e login de usuários
- Autenticação e autorização com JWT
- CRUD de categorias
- CRUD de pedidos
- Geração de PDF do pedido
- Rate Limiting
- Logs estruturados com Serilog

## Testes
- Testes unitários aplicados nos Services
- Banco em memória (EF InMemory)

# Enpoints
## Auth
* /Auth/Register  -  Cadastro de novos usuários.
* /Auth/Login - Efetua o login do usuario, e ja fornece o token.

## Order
* /Order/PrinterOrder/{id} - Gera arquivo em PDF da ordem de serviço com detalhes.
* /Order/GetAllOrders - Filtra todas as ordens.
* /Order/GetOrderById - Retorna ordem com o ID especifico.
* /Order/CreateOrder - Cria novas ordens.
* /Order/UpdateOrder/{id} - Atualiza todos os campos da ordem do ID especifico.
* /Order/DeleteOrder/{id} - Apaga ordem com ID especifico.
  
## Category
* /Category/GetAllCategorys - Filtra todas as categorias.
* /Category/GetCategoryById/{id} - Retorna categoria com o ID especifico.
* /Category/CreateCategory - Cria novas categorias.
* /Category/DeleteCategory/{id} - Apaga categoria com ID especifico.


## Como Executar
1. Configure o arquivo `appsettings.Development.json`
2. Execute as migrations
3. Inicie o projeto
4. Acesse o Swagger em `/swagger`
5. Realizar cadastro /api/auth/register
6. Efetuar o login  /api/auth/login, gerará o token para autorização de manipulação do endpoints.
 



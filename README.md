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

## Como Executar
1. Configure o arquivo `appsettings.Development.json`
2. Execute as migrations
3. Inicie o projeto
4. Acesse o Swagger em `/swagger`

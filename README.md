# OrderWebAPI

API REST desenvolvida em **ASP.NET Core (.NET)** para gerenciamento de **Pedidos (Orders)** e **Categorias (Categories)**, com foco em boas práticas de backend, segurança, testes e arquitetura limpa.

---

## 🚀 Tecnologias Utilizadas

* **.NET / ASP.NET Core Web API**
* **Entity Framework Core**
* **SQL Server**
* **Identity + JWT** (Autenticação e Autorização)
* **AutoMapper**
* **Repository Pattern**
* **xUnit** (Testes Unitários)
* **Moq** (Mock de dependências)
* **Middleware Global de Erros**
* **CORS configurado**

---

## 🏗️ Arquitetura do Projeto

O projeto segue uma separação clara de responsabilidades:

```
OrderWebAPI
│
├── Controllers        → Endpoints HTTP
├── Services           → Regras de negócio
├── Repositories       → Acesso a dados
├── Models             → Entidades do domínio
├── DTOs               → Objetos de transferência de dados
├── Data               → DbContext e Migrations
├── Middleware         → Tratamento global de erros
└── Tests              → Testes unitários (xUnit + Moq)
```

### 🔹 Services

Responsáveis por:

* Validações de regras de negócio
* Orquestração entre Controller e Repository
* Lançamento de exceções de domínio

### 🔹 Repositories

Responsáveis por:

* Comunicação com o banco de dados
* CRUD utilizando EF Core
* Nenhuma regra de negócio

---

## 🔐 Segurança

* Autenticação via **JWT**
* Identity configurado
* Endpoints protegidos (acesso somente autenticado)
* CORS configurado

---

## 🧪 Testes Unitários

Os testes unitários foram desenvolvidos utilizando:

* **xUnit** → Framework de testes
* **Moq** → Simulação de dependências (Repository e Mapper)

### 📁 Estrutura de Testes

```
OrderWebAPI.Tests
│
├── CategoryUnitTests
│   ├── CreateCategoryTests.cs
│   ├── GetCategoryTests.cs
│   └── DeleteCategoryTests.cs
│
└── OrderUnitTests
    ├── CreateOrderTests.cs
    ├── GetOrderTests.cs
    └── DeleteOrderTests.cs
```

### 🧠 Padrão de Nomenclatura dos Testes

```
Metodo_Cenario_ResultadoEsperado
```

Exemplo:

```csharp
CreateAsync_ShouldThrowException_WhenOrderIsNull
GetById_ShouldReturnOrder_WhenExists
DeleteAsync_ShouldThrowException_WhenNotFound
```

### ✅ O que é testado

* Dados válidos (sucesso)
* Dados inexistentes (NotFound)
* Dados inválidos (Exceptions)
* Garantia de que o Repository não é chamado quando há erro

### 📌 Exemplo de Teste

```csharp
[Fact]
public async Task CreateAsync_ShouldThrowException_WhenOrderIsNull()
{
    var repoMock = new Mock<IOrderRepository>();
    var mapperMock = new Mock<IMapper>();

    var service = new OrderService(repoMock.Object, mapperMock.Object);

    await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateAsync(null));

    repoMock.Verify(r => r.CreateAsync(It.IsAny<OrderModel>()), Times.Never);
}
```

---

## ⚠️ Tratamento Global de Erros

O projeto utiliza **Middleware Global** para capturar exceções e retornar respostas padronizadas:

* 400 → BadRequest
* 404 → NotFound
* 500 → InternalServerError

Isso mantém os Controllers limpos e padroniza as respostas da API.

---

## 🐳 Docker (Planejado)

O projeto está preparado para containerização com:

* SQL Server
* ASP.NET Core

(Dockerfile e docker-compose serão adicionados futuramente)

---

## ▶️ Como Executar o Projeto

1. Clone o repositório
2. Configure a connection string no `appsettings.json`
3. Execute as migrations
4. Rode o projeto via Visual Studio ou `dotnet run`

---

## 🎯 Objetivo do Projeto

Este projeto foi criado com foco em:

* Evolução profissional como **Backend .NET Developer**
* Aplicação de boas práticas do mercado
* Preparação para vagas Júnior / Pleno
* Base real para uso em ambiente empresarial

---

## 👨‍💻 Autor

**Ailson Brito**
Desenvolvedor Backend .NET

> "1% hoje e 100% amanhã"



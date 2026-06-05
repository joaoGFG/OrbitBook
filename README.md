# OrbitBook

![Status](https://img.shields.io/badge/Status-Conclu%C3%ADdo-success) 
![.NET Version](https://img.shields.io/badge/.NET-9.0-blue)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-brightgreen)
![Database](https://img.shields.io/badge/Database-Oracle%2019c%2B-red)

## ?? O Problema e a Solução
A economia espacial saiu da ficção científica. Porém, não existe hoje uma plataforma centralizada para encontrar, comparar e reservar experiências espaciais de forma simples, segura e automatizada.

O **OrbitBook** atua como uma plataforma web e mobile de centralização de cadastros e reservas para o turismo espacial (Voos Suborbitais, Órbita Baixa (LEO), Missões Lunares e Colonização em Marte). A API é responsável pelo controle integrado de viajantes, operadoras e emissão com inteligência nas reservas.

---

## ?? Arquitetura e Padrões Aplicados

A API foi desenvolvida em **C# / .NET 9** utilizando **Clean Architecture** (separação rígida de responsabilidades) e seguindo princípios do **SOLID**. As instâncias são gerenciadas via **Injeção de Dependências** natural do ASP.NET.

### Projetos da Solução:
- **OrbitBook.Domain:** Regras de negócio, Entidades (Models) (`User`, `Destination`, `Booking`, etc.). Nenhuma dependência externa.
- **OrbitBook.Application:** Casos de uso, interfaces de Serviços e Repositórios, e DTOs (Data Transfer Objects).
- **OrbitBook.Infrastructure:** Persistência (`DbContext` usando **EF Core** no Oracle), Mapeamento e Classes de Repositórios de Banco.
- **OrbitBook.API:** Controllers, Swagger, Middlewares e Injeção do JWT.
- **OrbitBook.Tests:** Testes unitários focados na camada Application utilizando **xUnit** e Mock (Moq).

### Diagramas Base

Abaixo listamos como acontece a hierarquia geral da aplicação e a ligação do Banco Relacional:

- **Entity Relationship Model (Resumo):**  
  `Users` (1:N) `Bookings`, onde cada registro na plataforma é mapeado a um Usuário.  
  `Bookings` (N:1) `Destinations` (Tabela de catálogo base).  
  `Bookings` (1:N) `Passengers` (Passageiros atrelados a cada reserva gerada).

---

## ?? Requisitos Atendidos 

- ? **API REST em ASP.NET Core** com boas práticas arquiteturais.
- ? **Persistência Relacional**: Banco Oracle usando Entity Framework Core.
- ? **Mapeamento Entidades**: Existência de relacionamentos `1:N` explícitos mapeados pelo `HasMany` e `WithOne` entre Destinos, Usuários e Reservas (Bookings).
- ? **Tratamento de Exceções Global**: Middleware inteligente `GlobalExceptionHandlerMiddleware` operando.
- ? **Migrations**: Histórico de Tracking habilitado gerado na pasta `Infrastructure/Migrations`. 
- ? **Health Checks**: Mapeamento do Oracle via pacote HealthChecks operando em `/health`.
- ? **Autenticação via JWT**: Rotas sensíveis (como de Reservas e Histórico de Usuário) só são expostas usando bearer token.
- ? **Documentação da API via Swagger**.
- ? **Testes Automatizados**: `AuthServiceTests.cs` desenvolvido sobre a arquitetura `AAA` testando cenários de Autenticação na Application layer.

---

## ?? Testando o Projeto (Instruções de Acesso)

### Instalação e Configuração:
1. Clone o repositório:
   ```bash
   git clone https://github.com/joaoGFG/OrbitBook.git
   cd OrbitBook
   ```
2. Adicione sua URL / Connection String do Oracle no `OrbitBook.API/appsettings.json`, buscando a tag:
   ```json
   "ConnectionStrings": {
     "OracleConnection": "DATA SOURCE=seu_servidor_oracle;USER ID=seu_user;PASSWORD=sua_senha;"
   }
   ```
3. O comando EF Migrations já foi efetuado. Para apenas popular a base execute os **SCRIPTS SQL/DML** que acompanham o repositório na raiz (arquivos com inserts simulados).
4. Restaure e crie a build do projeto na CLI:
   ```bash
   dotnet build
   ```
5. Execute o sistema:
   ```bash
   dotnet run --project OrbitBook.API\OrbitBook.API.csproj
   ```

A API estará rodando e pronta para receber chamadas com documentação mapeada em `https://localhost:XXXX/swagger`.

### Exemplos de Teste (Requisições API)

#### 1. Acesso Público — Lista de Catálogo de Viagens
- **Método**: `GET`
- **Rota**: `/api/destinations`
- **Funcionalidade**: Retorna as viagens habilitadas da SpaceX, Blue Origin e suas capacidades no Payload. Funciona sem JWT.

#### 2. Rota de Autenticação — Gerando Token
- **Método**: `POST`
- **Rota**: `/api/auth/login`
- **Body JWT Request:**
   ```json
   {
     "email": "carlos.souza@email.com",
     "password": "$2b$12$abc001hashed"
   }
   ```
- **Retorno Esperado:** Ele validará o seu JWT gerando o `Bearer Token` do sistema que será usado em frente.

#### 3. Rota de Protegida [Authorize] — Vendo Minhas Reservas
- **Método**: `GET`
- **Rota**: `/api/bookings`
- **Headers:** No Header da requisição utilize `Authorization: Bearer <seu_token_gerado>`.
- **Funcionalidade**: Verifica pela ClaimsIdentity quem você é extraindo o seu `NameIdentifier` e puxando na base os Voos comprados por aquele usuário especificamente.

#### 4. Executando os Testes Unitários de Autorização (AAA)
Para conferir o teste xUnit funcionando sem rodar a aplicação principal:
```bash
dotnet test
```
Resultados de sucesso do Mock da service de autenticação devem ser exibidos validando `Arrange, Act, Assert`.

---

**Autoria**: João Guilherme FG.
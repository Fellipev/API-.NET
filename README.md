# API de Clientes, Contatos e Endereços

API desenvolvida em **.NET 8** utilizando **SQLite In-Memory** para facilitar os testes.  
O objetivo é contemplar recursos entre as entidades relacionais **Cliente**, **Contato** e **Endereço**, permitindo:

-  Mapear relacionamentos 1:1 entre as entidades  
-  Inserir cada recurso de forma independente  
-  Vincular e desvincular recursos via rotas específicas  
-  Disponibilizar **JSONs de entrada e saída** claros em cada requisição

---

## Tecnologias utilizadas
- [.NET 8](https://dotnet.microsoft.com/)
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [SQLite In-Memory](https://www.sqlite.org/inmemorydb.html)
- [Swagger / Swashbuckle](https://swagger.io/)

---

## Estrutura das Entidades

- **Cliente**
  - `Id`, `Nome`, `Email`, `Cpf`, `Rg`
  - Relacionamento 1:1 com **Contato** (opcional)

- **Contato**
  - `Id`, `Tipo` (Residencial/Entrega/Cobrança), `Ddd`, `Telefone`
  - Relacionamento 1:1 com **Endereço** (opcional)

- **Endereço**
  - `Id`, `Tipo` (Preferencial/Entrega/Cobrança), `Cep`, `Logradouro`, `Numero`, `Cidade`, `Estado`, `Referencia`

---

## Endpoints principais
### Clientes
- POST /cliente/criar
- GET /cliente/listar (Podendo usar filtros como ?nome=...&email=...)
- PUT /cliente/atualizar/{id}
- DELETE /cliente/remover/{id}
- PUT /cliente/{id}/vincular-contato/{contatoId}
- DELETE /cliente/{id}/desvincular-contato

### Contatos
- POST /contato
- GET /contato
- PUT /contato/{id}/vincular-endereco/{enderecoId}
- DELETE /contato/{id}/desvincular-endereco
- DELETE /contato/{id}

### Endereços
- POST /endereco
- GET /endereco
- GET /endereco/{id}
- PUT /endereco/{id}
- DELETE /endereco/{id}

---

## Exemplos de JSONs
### Criar cliente
```
{
  "nome": "Ana Maria",
  "email": "ana@exemplo.com",
  "cpf": "123.456.789-09",
  "rg": "12.345.678-9"
}
```
### Criar contato
```
{
  "tipo": "Residencial",
  "ddd": 11,
  "telefone": 999999999
}
```
### Criar endereço
```
{
  "tipo": "Preferencial",
  "cep": "01310-100",
  "logradouro": "Av. Paulista",
  "numero": 1000,
  "bairro": "Bela Vista",
  "cidade": "São Paulo",
  "estado": "SP",
  "referencia": "Próximo ao MASP"
}
```

# SkillBridge.Infrastructure

## 🛠️ Objetivo
Esta camada contém as **implementações técnicas** do sistema: persistência, autenticação, serviços externos, etc. Ela **implementa as interfaces** declaradas na camada Domain.

---

## 📁 Estrutura

- **Persistence/**: DbContext, Migrations, configurações do EF Core
- **Repositories/**: Implementações dos repositórios (ex: `UserRepository`)
- **Auth/**: Implementações de autenticação e identidade

---

## ✅ Responsabilidades

- Acessar e persistir dados em bancos de dados
- Integrar com serviços externos (e-mail, storage, etc)
- Fornecer autenticação/autorização (ex: JWT)

---

## 🚫 Não fazer

- Definir regras de negócio
- Depender da camada API
- Conter lógica de apresentação ou casos de uso

---

## 🔁 Dependências

- Depende de: `SkillBridge.Domain`
- Referenciado por: `SkillBridge.API` (via DI)

---

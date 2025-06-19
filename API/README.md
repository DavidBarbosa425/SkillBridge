# SkillBridge.API

## 🌐 Objetivo
Projeto responsável por expor os endpoints HTTP da aplicação. Atua como **interface com o cliente (frontend, mobile, etc.)** e orquestra os casos de uso da camada `Application`.

---

## 📁 Estrutura

- **Controllers/**: Endpoints REST (ex: `UserController`, `ProjectController`)
- **Middlewares/**: Manipulação de erros, autenticação, logs, etc
- **Configuration/**: Configurações como CORS, Swagger, Autenticação, DI

---

## ✅ Responsabilidades

- Receber requisições HTTP e validar entrada
- Chamar casos de uso da `Application`
- Retornar respostas formatadas (ex: JSON)
- Configurar serviços, middlewares e dependências

---

## 🚫 Não fazer

- Regras de negócio
- Acesso direto ao banco de dados
- Implementações de repositórios

---

## 🔁 Dependências

- Depende de: `SkillBridge.Application`
- Configura e injeta: `SkillBridge.Infrastructure`

---

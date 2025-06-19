# SkillBridge.Domain

## 🧠 Objetivo
Este projeto representa o núcleo da aplicação: o **Domínio**. Ele contém toda a lógica de negócio, entidades, regras e contratos (interfaces). Não deve depender de nenhuma outra camada.

---

## 📁 Estrutura

- **Entities/**: Entidades do domínio (ex: `User`, `Project`)
- **ValueObjects/**: Objetos de valor (ex: `Email`, `ProjectName`)
- **Interfaces/**: Contratos para repositórios e serviços
- **Enums/**: Tipos enumerados usados nas entidades (ex: `ProjectStatus`)

---

## ✅ Responsabilidades

- Modelar as regras de negócio puras
- Definir a linguagem ubíqua do sistema
- Garantir a consistência do modelo de domínio
- Declarar interfaces para repositórios e serviços

---

## 🚫 Não fazer

- Acessar banco de dados
- Usar bibliotecas externas (exceto auxiliares puras)
- Depender de Application, Infrastructure ou API

---

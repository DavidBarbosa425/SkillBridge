# SkillBridge.Application

## 🎯 Objetivo
Esta camada representa os **casos de uso da aplicação**, orquestrando as regras de negócio contidas no projeto Domain. Pode depender do `Domain`, mas não da `Infrastructure` ou `API`.

---

## 📁 Estrutura

- **UseCases/**: Implementações dos casos de uso (ex: `CreateProjectUseCase`)
- **DTOs/**: Modelos de entrada e saída (ex: `CreateProjectRequest`, `UserResponse`)
- **Validators/**: Validações com FluentValidation
- *(Opcional)* **Mappings/**: Configurações do AutoMapper

---

## ✅ Responsabilidades

- Orquestrar a execução das regras de negócio
- Validar entrada e saída de dados via DTOs
- Trabalhar com repositórios e serviços através de interfaces

---

## 🚫 Não fazer

- Implementar lógica de domínio complexa
- Usar classes de banco de dados diretamente
- Gerar saída HTTP

---

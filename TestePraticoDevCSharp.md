## Processo Seletivo — Desenvolvedor C# Pleno

**Stack obrigatória e eliminatória:**

* Windows Forms
* .NET Framework 4.6
* PostgreSQL
* Npgsql
* ReportViewer

---

# 1. Objetivo

Avaliar domínio técnico real em ambiente desktop corporativo com:

* Arquitetura organizada
* Controle transacional
* Performance básica
* Concorrência
* Relatórios gerenciais consistentes
* Código sustentável

Este teste busca diferenciar um desenvolvedor pleno experiente de um perfil apenas operacional.

---

# 2. Cenário

Desenvolver um sistema desktop de **Gestão de Ordens de Serviço (OS)** com controle financeiro simplificado, auditoria e relatórios.

O sistema deve ser estruturado como se fosse evoluir para produção.

---

# 3. Requisitos Funcionais Avançados

## 3.1 Clientes

Campos:

* Id (PK)
* Nome (obrigatório)
* Documento (único)
* Tipo (Física / Jurídica)
* E-mail
* Telefone
* DataCadastro
* Ativo (bool)

Regras:

* Documento único no banco (constraint)
* Não permitir exclusão se existir OS vinculada
* Pesquisa com múltiplos filtros combináveis (nome + documento + ativo)

---

## 3.2 Serviços

Campos:

* Id
* Nome
* ValorBase
* PercentualImposto (decimal)
* Ativo

Regras:

* ValorBase > 0
* PercentualImposto entre 0 e 100
* Ao alterar ValorBase, não afetar OS já criadas

---

## 3.3 Ordem de Serviço

Campos:

* Id
* ClienteId
* DataAbertura
* DataConclusao (nullable)
* Status
* Observacao
* ValorTotal
* Versao (controle de concorrência)

### Itens

Campos:

* Id
* OrdemServicoId
* ServicoId
* Quantidade
* ValorUnitario
* PercentualImpostoAplicado
* ValorTotalItem

Regras:

* ValorTotalItem = (Quantidade × ValorUnitario) + imposto
* ValorTotal da OS deve ser recalculado a cada alteração
* Não permitir edição de itens se status for Concluída ou Cancelada
* Não permitir concluir OS com valor total = 0

---

# 4. Requisitos Técnicos Avançados (Obrigatórios)

## 4.1 Transação Completa

Inserção ou atualização de:

* OS
* Itens
* Histórico de status

Deve ocorrer em uma única transação.

Rollback obrigatório em qualquer falha.

---

## 4.2 Controle de Concorrência (Obrigatório)

Implementar controle de concorrência otimista utilizando:

* Campo "Versao" (integer ou xmin do PostgreSQL)

Se dois usuários abrirem a mesma OS:

* O segundo deve receber erro de concorrência ao tentar salvar.

---

## 4.3 Histórico / Auditoria

Criar tabela de auditoria contendo:

* Entidade
* IdRegistro
* Operacao (INSERT/UPDATE/DELETE)
* DataHora
* Usuario
* Snapshot JSON do estado

Registrar auditoria ao:

* Alterar status
* Alterar itens
* Alterar valor total

---

## 4.4 Performance

Listagens devem:

* Ter paginação (LIMIT/OFFSET)
* Não carregar itens automaticamente na grid principal
* Carregar itens apenas ao abrir a OS

---

## 4.5 Acesso a Dados

Obrigatório:

* NpgsqlConnection
* NpgsqlCommand
* Parâmetros nomeados
* Using statements adequados
* Não usar ORM

Separação mínima:

* UI
* Services
* Repositories
* Entities
* Infra (conexão/log)

---

# 5. Relatórios (ReportViewer — Avançado)

Criar relatório contendo:

Filtros:

* Período
* Cliente
* Status

Exibição:

* Agrupado por cliente
* Total por cliente
* Total geral
* Total de impostos
* Quantidade total de OS no período

Relatório deve:

* Utilizar DataSet tipado ou objeto de projeção
* Aplicar filtros dinamicamente
* Permitir exportação para PDF

---

# 6. Banco de Dados

Entregar script contendo:

* PK / FK
* Índices
* Constraints CHECK
* Unique constraint para documento
* Índice em:

  * documento
  * data_abertura
  * status
  * cliente_id

Opcional (diferencial):

* Uso de partial index
* Uso de trigger para auditoria

---

# 7. Arquitetura Esperada

Deve demonstrar:

* Separação de responsabilidades
* Services contendo regras
* Repositórios sem lógica de negócio
* Validações centralizadas
* Tratamento adequado de exceções
* Logs em arquivo

---

# 8. Tratamento de Erros

Esperado:

* Mensagens amigáveis na UI
* Log técnico detalhado em arquivo
* Captura de erro de concorrência
* Captura de erro de constraint (unique, FK)

---

# 9. Entrega

Obrigatório:

1. Código-fonte (Git ou ZIP)
2. Script SQL completo
3. README contendo:

   * Arquitetura adotada
   * Decisões técnicas
   * Estratégia de concorrência
   * Como rodar
   * String de conexão exemplo

---

# 10. Critérios de Avaliação 

* Arquitetura e organização — 20%
* Modelagem e SQL — 20%
* Transações e concorrência — 20%
* WinForms (UX, binding, organização) — 15%
* ReportViewer — 10%
* Código limpo e sustentável — 10%
* Git e documentação — 5%

---

# 11. Prazo

7 dias corridos

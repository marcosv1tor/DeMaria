# GestaoOS

Sistema desktop de Gestao de Ordens de Servico com controle financeiro simplificado, auditoria, concorrencia otimista e relatorio gerencial.

## Stack

- Windows Forms
- .NET Framework 4.6
- PostgreSQL
- Npgsql 4.0.17
- ReportViewer 150.1652.0
- MSTest

## Arquitetura

A solucao foi separada em camadas para manter regras fora da interface e SQL fora dos services.

- `GestaoOS.Domain`: entidades, enums, filtros e DTOs.
- `GestaoOS.Application`: services, validacoes, transacoes, auditoria e contratos de repositorio.
- `GestaoOS.Infrastructure`: conexao PostgreSQL, Unit of Work, repositories Npgsql e log em arquivo.
- `GestaoOS.WinForms`: telas, bindings, mensagens amigaveis e ReportViewer.
- `GestaoOS.Tests`: testes das regras principais.

Os repositories usam `NpgsqlConnection`, `NpgsqlCommand`, parametros nomeados e `using`. Nao ha ORM.

## Banco De Dados

Crie o banco e execute os scripts:

```powershell
createdb -U postgres gestao_os
psql -U postgres -d gestao_os -f database/001_schema.sql
psql -U postgres -d gestao_os -f database/002_seed.sql
```

O schema inclui PK/FK, `UNIQUE(documento)`, checks, indices obrigatorios e partial indexes para clientes/servicos ativos.

## String De Conexao

Exemplo em `src/GestaoOS.WinForms/App.config`:

```xml
<add name="GestaoOS" connectionString="Host=localhost;Port=5432;Database=gestao_os;Username=postgres;Password=postgres" providerName="Npgsql" />
```

## Concorrencia E Transacoes

A OS usa concorrencia otimista com o campo `versao`.

- Updates usam `WHERE id = @id AND versao = @versao`.
- Quando nenhuma linha e afetada, a aplicacao lanca erro de concorrencia.
- Cabecalho da OS, itens, historico de status e auditoria sao salvos em uma unica transacao.
- Qualquer falha chama rollback.

## Auditoria

A tabela `auditorias` registra entidade, registro, operacao, data/hora, usuario e snapshot JSON. A aplicacao registra auditoria ao salvar OS, cobrindo alteracoes de status, itens e valor total.

O usuario gravado e `Environment.UserName`.

## Relatorios

A tela de relatorio permite filtrar por periodo, cliente e status. O ReportViewer usa a projecao `RelatorioOrdemServico`, exibe totais e permite exportar PDF.

## Como Rodar

1. Instale Visual Studio com suporte a .NET Framework e o targeting pack do .NET Framework 4.6.
2. Restaure os pacotes NuGet.
3. Execute os scripts SQL.
4. Abra `GestaoOS.sln`.
5. Configure `GestaoOS.WinForms` como projeto inicial.
6. Execute em Debug.

Comandos de validacao usados no desenvolvimento:

```powershell
& 'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' GestaoOS.sln /t:Restore /v:minimal
& 'C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe' GestaoOS.sln /p:Configuration=Debug /p:FrameworkPathOverride='C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2' /v:minimal
& 'C:\Program Files\Microsoft Visual Studio\18\Community\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe' tests\GestaoOS.Tests\bin\Debug\GestaoOS.Tests.dll
```

Observacao: nesta maquina o diretorio de referencia `v4.6` existe, mas esta incompleto, sem as DLLs do targeting pack. Por isso a compilacao local foi validada com `FrameworkPathOverride` apontando para os assemblies disponiveis de `v4.7.2`, mantendo todos os projetos configurados como `.NET Framework 4.6`.

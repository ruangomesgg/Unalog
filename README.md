# UNALOG - Cadastro de Motoristas Terceiros

Aplicação web para cadastrar motoristas terceiros e consultar os registros salvos. O projeto foi desenvolvido em C# com ASP.NET Core MVC, .NET 10, Entity Framework Core e SQLite.

## Estrutura da solução

- `Unalog.Web`: aplicação ASP.NET Core MVC, com controllers, views, viewmodels e configuração.
- `Unalog.Core`: entidades de domínio e interfaces dos repositórios.
- `Unalog.Infrastructure`: contexto do Entity Framework Core, acesso ao SQLite e implementação dos repositórios e migrations.
- `Unalog.Tests`: testes automatizados das validações do formulário.

O controller usa `IMotoristaRepository`; o acesso ao banco fica na camada Infrastructure.
O Serilog grava os eventos da aplicação no console e em arquivos diários.

## Requisitos

- .NET SDK 10.0
- Ferramenta de linha de comando do Entity Framework Core (`dotnet-ef`) para criar ou aplicar migrations

Confira o SDK instalado:

```powershell
dotnet --list-sdks
```

Se ainda não tiver instalado o `dotnet-ef`, instale-o uma vez:

```powershell
dotnet tool install --global dotnet-ef
```

Feche e reabra o terminal depois da instalação, se o comando não for reconhecido. Confira a ferramenta:

```powershell
dotnet ef --version
```

## Configuração do banco de dados

A connection string fica em `Unalog.Web/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=unalog.db"
}
```

O banco local é SQLite. Para usar outro caminho ou banco, altere essa connection string e configure o provedor correspondente no `Program.cs`.

## Criar e aplicar migrations

Execute os comandos a partir da pasta raiz da solução, onde está `Unalog.slnx`.

Para criar uma nova migration (por exemplo, após alterar uma entidade):

```powershell
dotnet ef migrations add NomeDaMigration --project .\Unalog.Infrastructure\Unalog.Infrastructure.csproj --startup-project .\Unalog.Web\Unalog.Web.csproj --output-dir Data\Migrations
```

Para aplicar as migrations e criar ou atualizar o banco:

```powershell
dotnet ef database update --project .\Unalog.Infrastructure\Unalog.Infrastructure.csproj --startup-project .\Unalog.Web\Unalog.Web.csproj
```

A migration inicial já foi criada e aplicada no ambiente de desenvolvimento.

## Compilar e executar

Na raiz da solução, compile os projetos:

```powershell
dotnet build .\Unalog.slnx
```

Para executar os testes automatizados:

```powershell
dotnet test .\Unalog.Tests\Unalog.Tests.csproj
```

Inicie o site:

```powershell
dotnet run --project .\Unalog.Web\Unalog.Web.csproj
```

Abra no navegador o endereço `localhost` exibido no terminal. No ambiente de desenvolvimento usado durante a construção, o endereço foi `http://localhost:5258`.

- Página inicial: `/`
- Cadastro: `/Motorista/Cadastrar`
- Lista de motoristas: `/Motorista`

Para encerrar o servidor, pressione `Ctrl+C` no terminal.

## Logs

Os logs são gravados na pasta `Unalog.Web/Logs`, em arquivos com data no nome. A configuração mantém até sete arquivos diários. O registro inclui eventos HTTP, cadastros concluídos e detalhes de exceções ao salvar; não registra nome, e-mail ou telefone do motorista.

## Funcionalidades

- Cadastro de nome, e-mail, telefone, modelo do veículo e matrícula.
- Edição dos dados de motoristas já cadastrados.
- Indicadores de treinamento para cargas especiais e rastreador.
- Validação dos campos do formulário.
- Confirmação após o cadastro e consulta dos motoristas cadastrados.

## Observação sobre acesso

O projeto não inclui autenticação, conforme o escopo definido para esta versão. Use-o localmente para demonstração e não exponha o site nem dados reais de motoristas a outras pessoas sem implementar o controle de acesso adequado.

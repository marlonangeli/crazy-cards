# Serviços

| Aqui são descritos os serviços utilizados no projeto.

## Redis

Redis é um banco de dados de chave-valor em memória, que pode ser usado como cache. Ele é usado para armazenar dados que são carregados com frequência, como os dados de configuração do jogo e recursos mais usados.

## RabbitMQ

RabbitMQ é um servidor de mensagens que pode ser usado para comunicação assíncrona entre os serviços. Ele é usado para enviar mensagens de notificação para os jogadores.

## SQL Server

SQL Server é um banco de dados relacional que armazena os dados do jogo. Ele é usado para armazenar dados de jogadores, partidas e cartas.

---

# Como executar

## Pré-requisitos

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

## Executando

Para executar o projeto, basta executar o comando abaixo no diretório `services`:

```bash
docker-compose up -d
```
## Variáveis de ambiente

Os serviços podem ser configurados através de variáveis de ambiente. As variáveis de ambiente são definidas no arquivo `.env` no diretório `services`.

| Variável | Descrição | Valor padrão |
| -------- | --------- | ------------ |
| `REDIS_HOST` | Endereço do servidor Redis | `localhost` |
| `REDIS_PORT` | Porta do servidor Redis | `6379` |
| `REDIS_PASSWORD` | Senha do servidor Redis | `changeme` |
| `RABBITMQ_HOST` | Endereço do servidor RabbitMQ | `localhost` |
| `RABBITMQ_PORT` | Porta do servidor RabbitMQ | `5672` |
| `RABBITMQ_DEFAULT_USER` | Usuário do servidor RabbitMQ | `guest` |
| `RABBITMQ_DEFAULT_PASSWORD` | Senha do servidor RabbitMQ | `guest` |
| `RABBITMQ_DEFAULT_VHOST` | Virtual host do servidor RabbitMQ | `/` |
| `SQLSERVER_HOST` | Endereço do servidor SQL Server | `localhost` |
| `SQLSERVER_PORT` | Porta do servidor SQL Server | `1433` |
| `SQLSERVER_USER` | Usuário do servidor SQL Server | `crazycards` |
| `SQLSERVER_PASSWORD` | Senha do servidor SQL Server | `changeme` |
| `SQLSERVER_SA_PASSWORD` | Senha do usuário `sa` do servidor SQL Server | `changeme` |
| `SQLSERVER_DATABASE` | Nome do banco de dados do servidor SQL Server | `CrazyCards` |

| OBS: As variáveis de ambiente também são acessadas pela aplicação. Para mais informações, consulte o [README](../README.md) do projeto.

---
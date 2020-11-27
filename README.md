# Leaderboard 

Desafio tecnico para vaga de back-end na Lumen games
Consiste numa API de Leaderboard 

### Tech

Esse projeto usa as sequintes tecnologias:

* [ASP.NET CORE](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-5.0) - Framework de c# para desenvolvimento de API'S
* [Redis](https://redis.io/) - Banco de dados NoSql


### Instalando 

Instale no site oficial o ASP.NET CORE

Instalando as dependencias necessarias

```sh
$ cd leaderboard
$ dotnet msbuild -pp:leaderboard.xml
```

Redis

```sh
$ redis-server
```
### Rodando o codigo

```sh 
$ donet run 
```

### Documentação 

Segue as rotas disponiveis
- Criar player 
    - Rota: localhost:5001/api/Player
    - Metodo: POST
    - Conteudo do body: 
    ``` sh 
    {
	"Nickname":"apelido",
	"Score":0	
    }
    ```
- Atualizar player
   - Rota: localhost:5001/api/Player/{apelido}
    - Metodo: PUT
    - Conteudo do body: 
    ``` sh 
    {
	"Nickname":"apelido",
	"Score":0	
    }
    ```
- Pegar apenas um player
    - Rota: localhost:5001/api/Player/{apelido}
    - Metodo: GET
    
- Pegar o leaderboard
    - Rota: localhost:5001/api/Player/leaderboard
    - Metodo: GET
 - Deleta um player 
    - Rota: localhost:5001/api/Player/{apelido}
    - Metodo: DELETE
 


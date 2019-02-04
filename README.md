# CashBack Web Api
Exemplo de Web Api

# Tecnologia empregada
* .Net Core 2.0
* EntityFrameworkCore.InMemory (2.2.1)
* EntityFrameworkCore.Proxies (2.2.1)

# Disponibilizando a api através do servidor Kestrel que foi previamente configurado no projeto.
1) Fazer o download da pasta bin/Release
2) Através do prompt de comando, acessar a pasta salva: Exemplo cd c:\projetos\bin\Release\netcoreapp2.0
3) Digitar o comando dotnet CashBack.dll

Feito isso, a api estará no ar, utilizando muito provavelmente a URL htt://localhost:5000 que será nossa URL base.

# Testando os serviços com o Postman
Assim que os serviços estiverem no ar, é possível começar a testar os seguintes serviços, com o Postman

# Verbo GET
1) [Route("api/CatalogoDiscos/genero/{genero}")] => Busca os catálogos de discos por gênero (POP, MPB, CLASSIC, ROCK).
   Obs.: Nessa rota é possível utilizar os parâmetros offset e limit para determinar a paginação.
   Exemplo: http://localhost:5000/api/CatalogoDiscos/genero/ROCK?offset=0&limit=10
   
2) [Route("api/CatalogoDiscos/{id}")] => Busca um disco específico pelo identificador

3) [Route("api/CashBackPercentual/{genero}")] => Busca o valor de um cashBack de acordo com o gênero. Para a busca utiliza a data atual.

4) [Route("api/vendas/{id}")] => Busca uma venda pelo seu identificador

5) [Route("api/vendas/{dataInicial}/{dataFinal}")] => Busca as vendas de acordo com o range determinado nas datas inicial e final.
   Obs.: Nessa rota é possível utilizar os parâmetros offset e limit para determinar a paginação.
   Exemplo: http://localhost:5000/api/vendas/3-2-2019/3-2-2019?offset=0&limit=50
   
# Verbo POST
1) [Route("api/vendas")] => Através dessa ação é possível gravar uma venda passando um objeto Json no body
   Exemplo de objeto JSON
   
{
    "itens": [
        {
            "disco": {
		        "id": 10,
		        "genero": "ROCK",
		        "nome": "Grunge Forever",
		        "precoVenda": 48.22
		    }
        },
        {
            "disco": {
		    	"id": 16,
		        "genero": "ROCK",
		        "nome": "Pop Rock Shot",
		        "precoVenda": 25.75
		    }
        }
    ]
}

# Informações Adicionais importantes

Através da classe Startup, inicio 3 métodos que são:

1) AdicionarPercentuaisCashBack
   Nesse método eu adiciono uma tabela previamente solicitada para os cálculos de caskback no momento das vendas.
   
2) AdicionarAlbunsSpotify
   Criei uma conta teste no Spotify e estou utilizando a API deles para buscar os títulos e preencher o meu banco de dados.
   A intenção inicial era a de buscar os álbuns, mas na documentação do Spotify, até o momento em que desenvolvi esse projeto, eles
disponibilizavam apenas playlists separadas por gêneros.
   Como a intenção era a de popular o meu banco para ter informações para testar a venda, acabei optando por utilizar as playlists.
   
   Outro ponto importante nesse método, é que não encontrei o gênero MPB e então, para não perder muito tempo, eu acabei buscando
o gênero JAZZ como se fosse MPB.

3) AdicionarVendasTestes
   Nesse método eu realizo 40 vendas, já buscando valores totais de vendas e também de cashBack para popular o banco e termos como
testar o relatório de vendas por período.

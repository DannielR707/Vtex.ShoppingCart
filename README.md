##Developer Shopping Cart

Criar o carrinho de compras de uma loja que vende desenvolvedores.


## Tarefas e priorização

Lista de tarefas escolhidas:

* DETERMINAR O PREÇO DO DESENVOLVEDOR A PARTIR DE INFORMAÇÕES DO SEU PERFIL DO GITHUB, COMO POR EXEMPLO: FOLLOWERS, REPOS, STARS, COMMITS, ETC.
* SUBSTITUIR OS INPUTS DE TEXTO POR UMA LISTA DE DESENVOLVEDORES COM NOME, FOTO, PREÇO E UM BOTÃO DE "ADICIONAR AO CARRINHO".
* POPULAR A LISTA DE DESENVOLVEDORES A PARTIR DE UMA ORGANIZAÇÃO DO GITHUB.

Considero as duas primeras as tarefas mais importantes porque são o núcleo da aplicação já que determinan e mostran ao usuario a informação principal do "produto":
o preço, o nome, e a foto.  
E além disso toda essa informação tem que ser obtida de alguma parte, porem precisa se que no site o usuario possa ter a opção de inserir a organização da qual
quer obter a lista de desenvolvedores.


## Server side

- Foi implementado usando Microsoft Visual Studio 2015 e ASP .NET Web API.
- O back end faz o chamado da API de GITHUB para obter a informação dos desenvolvedores a partir de uma organização.
- Uma arquitetura de multicapas foi implementada pra a lógica do negocio.
- A Injeção de Dependencia foi implementada com Ninject.
- Algums testes unitarios foram implementados com Moq.
- A aplicação faz "log" das exceções usando Elmah.
- O preço do desenvolvedor é calculado a partir de: Followers, Stars e Public Repositories. No calculo do preço cada um desses parametros tem um valor
diferente que modifica o preço do desenvolvedor de acordo a quantidade.

Obs: Os commits nao foram usados porque requeria trazer os commits de cada repositorio de cada usuario.

## Client side

- Foi implementado em React.

Obs: Pode se melhorar implementando validaçoes quando o "cliente" recebe o nome da organização, por exemplo se não existe. 
Tambem uma forma de indicar ao usuario no momento que a aplicação esta trazendo os desenvolvedores.

## Teste da aplicação

O Back End e Front End da aplicação foram carregados na plataforma de Google Cloud.

URL: http://130.211.212.145:8000/

Para testar a aplicação o usuario tem que ingressar o nome de uma organização de Github, segue algums exemplos: entryway, merb, sauspiel, lincolnloop.



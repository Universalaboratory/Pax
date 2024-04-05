
Para Funções que possam ser entendidas facilmente, crie um simples comentário de uma linha exemplificando o que a mesma faz, um exemplo é mostrado abaixo, onde uma função lida com a rotação do player de acordo com o input e a câmera da cena.



Se atentar sempre em modularizar ao máximo funções grandes e confusas para o menor tamanho possível, um exemplo seria a locomoção de um personagem, onde possui rotação, movimento e detecções com o chão e saltos, abaixo é mostrado a divisão de tais funções.




3 Formato dos arquivos de entrega

	Todos os arquivos entregues devem seguir a mesma estrutura listada acima, nomes de pastas começam em maiúsculo, já os arquivos possuem em seu nome a estrutura de (tipo de arquivo)_(categoria/conjunto)(especificador).(extensão).

QA: Os relatórios de QA devem ser entregues ou em arquivos do Google Doc para cada problema com referências visuais. A entrega dos relatórios de QA deve se criar uma nova pasta com o nome no mesmo padrão que o exemplo: QA _Build 0.0.0.2 Assunto - Data.



Build: O versionamento das Build do projeto serão feitos utilizando o método “Versionamento Semântico 2.0.0” sendo 1.0.0.0 a versão de lançamento final no mercado:

Dado um número de versão MAJOR.MINOR.PATCH, incremente a:
Versão Maior(MAJOR): quando fizer mudanças incompatíveis na API,
Versão Menor(MINOR): quando adicionar funcionalidades mantendo compatibilidade, e
Versão de Correção(PATCH): quando corrigir falhas mantendo compatibilidade.
Rótulos adicionais para pré-lançamento(pre-release) e metadados de construção(build) estão disponíveis como extensão ao formato MAJOR.MINOR.PATCH.
Link: https://semver.org/lang/pt-BR/

Exemplo Nomenclatura da Build: Tipo de Build_0.0.0.1

A entrega das Build deve ser entregue na Pasta Build, criando uma pasta com a seguinte nomenclatura Tipo de Build 0.0.0.1 Assunto - Data.  

Versionamento de código
Para o versionamento de código, vamos utilizar git e git flow, a imagem abaixo representa nossa principal estrutura de branch e suas principais finalidades.



Utilizando git
Criando uma nova branch para trabalho:

É importante lembrar que toda branch será criada a partir da main, e JAMAIS fazer git pull origin dev para sua branch, uma vez que a branch é compartilhada por todos e pode conter códigos com problemas.
Para a criação de uma branch a partir a main, os passos são:

git checkout main
git pull origin main
git checkout -b nome-da-nova-branch

Enviando Alterações para minha branch:

git add . (adiciona todos os arquivos modificados na área de stage)
git commit -m “nome do commit” (cria um commit com suas alterações)
git push (envia alterações para sua branch), caso a mesma ainda não tenha sido criada no repositório, utilize: git push --set-upstream origin nome-da-branch

Enviando minhas alterações para dev:

O envio de alterações para a branch dev pode ser importante, uma vez que pessoas que irão testar poderão possuir um overview acerca do game criado pela equipe.

O envio consiste em puxar suas alterações para a branch dev, mas nunca atualizar sua branch com a mesma, veja abaixo os comandos:
git checkout dev (entra na branch dev)
git pull origin dev (sincroniza com o repositório)
git pull origin nome-da-sua-branch (adiciona sua branch)
git push (envia as alterações)

Meu código foi testado e pode ser enviado para a branch main, como fazer?

Neste caso, a atualização da main é feita mediante uma solicitação de pull request, para fazer a solicitação, siga os passos mostrados abaixo:
prepare a branch:

git checkout nome-da-sua-branch (seleciona a branch)
git pull origin main (sincroniza seu código com o da main)
git push (envia sua branch para o repositório remoto)

solicite o pull request:

Após a criação da branch, navegue até o seu repositório e selecione Compare & pull request.



Adicione uma descrição do que foi feito na branch a ser adicionada, além disso, sempre adicione um revisor para confirmar suas alterações, o ideal é selecionar um revisor que não tenha participado da implementação da feature.



Quero adicionar uma nova branch, mas não possuo ela na minha máquina.
git pull origin nome-da-branch

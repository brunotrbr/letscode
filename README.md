# letscode
## Desafio técnico de backend - Lets Code


UPDATE:

Olá,

Fiz a entrega da URL para a Raquel na segunda feira até o horário combinado, 19h, mas não tive tempo de implementar a orquestração dos containers e nem de fazer os testes unitários e de sistema.


Hoje finalizei a orquestração para facilitar a avaliação do código, pois tive um pouco mais de tempo para trabalhar. Optei por não fazer os testes, pois isso levaria mais alguns dias, e o prazo já havia estourado.

É importante salientar que as alterações foram somente a inclusão dos dockerfiles e do docker-compose.yml, sem alterar nenhum outro arquivo/código/requisito do projeto.

Agradeço pela compreensão.



## Para subir com o docker, execute os procedimentos abaixo:
1. Efetue o clone do repositório
```
git clone git@github.com:bruno-braganca/letscode.git
```

2. Entre no diretório letscode
```
cd letscode
```

2. No windows, no terminal do powershell, execute o script **build.ps1**, No linux, no terminal, execute o script **build.sh**
```
.\build.ps1
```
ou
```
sh build.sh
```

3. Assim que os contêineres subirem, abra o navegador e acesse o endereço abaixo
```
http://localhost:3000/
```

4. Caso queira verificar os endpoints, pode acessar o endereço abaixo:
```
http://localhost:5000/swagger/index.html
```

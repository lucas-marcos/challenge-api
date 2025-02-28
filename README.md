# Sobre a Caiapó
A construtora Caiapó é uma das maiores construtoras de infraestrutura do Brasil com obras espalhadas em todo território nacional.
Visando processos cada vez mais maduros, eficientes, e com sistemas robustos que ofereçam cada vez mais suporte ao modelo de negócio, desde 2019 a Caiapó vem investindo no desenvolvimento de suas próprias soluções de software.

# O Desafio
Crie um microsserviço capaz de aceitar solicitações RESTful que recebam como parâmetro o nome da cidade ou as coordenadas (*latitude e longitude*) e retorne as 5 tops músicas (*apenas os nomes das músicas*) de um determinado artista de acordo com a temperatura atual.

## Requisitos
1) Se a temperatura(*celsius*) estiver acima de 30 graus, entregue as 5 top músicas do David Guetta;
2) Caso a temperatura esteja entre 15 e 30 graus, entregue as 5 top músicas da Madonna;
3) Se estiver um pouco frio (entre 10 e 14 graus), entregue as 5 top de Os Paralamas do Sucesso;
4) Caso contrário, se estiver frio lá fora, entregue as 5 top músicas de Ludwig van Beethoven.

## Requisitos não funcionais
- O microsserviço deve estar preparado para ser tolerante a falhas, responsivo e resiliente;
- Utilize a linguagem C# .NET. Use qualquer ferramenta e estrutura com as quais se sinta confortável e elabore brevemente sua solução, detalhes de arquitetura, escolha de padrões e estruturas;
- Crie uma imagem docker do seu microsserviço e publique em algum hub a sua escolha (exemplo: dockerhub) e informe o comando para que seu serviço possa ser implantado localmente.

## Dicas
Você pode usar a API do *[OpenWeatherMaps](https://openweathermap.org)* para buscar dados de temperatura e o *[Spotify](https://developer.spotify.com/)* para sugerir as músicas da playlist.

A interpretação dos requisitos e o fornecimento da melhor solução são fundamentais para atender bem aos clientes desse microsserviço. Dê o seu melhor! :)

## Recomendações
- Faça um fork desse repositório para subir a sua solução e podermos avaliar seu código;
- Utilize padrões de documentação de commit e Pull Request;
- Utilize C#;
- Utilize .NET 8;
- Utilize docker;
- Utilize boas práticas de codificação, isso será avaliado;
- Mostre que conhece c#;
- Código limpo, organizado e documentado (quando necesário);
- Use e abuse de:
    - SOLID;
    - Criatividade;
    - Performance;
    - Manutenabilidade;
    - Testes de Unidade (quando necessário);
    - ... pois avaliaremos tudo !

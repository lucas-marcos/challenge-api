
## 🚀 **Manual de Instalação**

Para instalar e executar esta aplicação, consulte o **[MANUAL-INSTALACAO.md](MANUAL-INSTALACAO.md)** que contém instruções detalhadas de instalação via scripts automatizados ou comandos manuais.

## 🌐 **Projeto Publicado**

Esta aplicação está disponível e funcionando em: **[http://147.93.36.49:5000/swagger/index.html](http://147.93.36.49:5000/swagger/index.html)**

**Swagger UI com tema dark** disponível para testar todas as funcionalidades da API.


# Sobre a Caiapó
A construtora Caiapó é uma das maiores construtoras de infraestrutura do Brasil com obras espalhadas em todo território nacional.
Visando processos cada vez mais maduros, eficientes, e com sistemas robustos que ofereçam cada vez mais suporte ao modelo de negócio, desde 2019 a Caiapó vem investindo no desenvolvimento de suas próprias soluções de software.

# O Desafio
Considere o microserviço ServiceA que seja responsável por registrar tudo que é feito na obra para a construção de uma rodovia. Ex.: Serviço de Pavimentação, Serviço de Tapa Buraco, Serviço de Confecção de Meio Fio, etc.
Considere agora um outro microserviço, chamado ServiceControl, que seja responsável por receber esses registros, processar adicionando a informação das condições climáticas no dia em que a obra está sendo executada, persistir as informações, e repassar essa informação para um outro microserviço ServiceB.

Desenho da arquitetura

![image](https://github.com/user-attachments/assets/5ab45012-8605-4421-ae34-924c6d379f9d)

Para o teste implemente somente o ServiceControl e como ele se comunica com os outros microserviços. Não é necessário implementar o ServiceA e ServiceB.

## Requisitos
1) Obtenha a temperatura(*celsius*) na cidade em que a obra está sendo executada, e na hora em que o ServiceControl recebeu esse registro;
2) O ServiceControl irá registrar, além dos dados recebidos pelo ServiceA, a temperatura da cidade e a condição climática;
3) Caso a temperatura esteja entre 15 e 30 graus, sinalize no registro que o clima estava em "ótimas condições";
4) Se estiver um pouco frio (entre 10 e 14 graus), sinalize no registro que o clima estava "agradável";
5) Caso contrário, se estiver frio lá fora, sinalize no registro que o clima estava "impraticável", ou seja, sem condições de se executar a obra no dia;
6) Considere um volume de 50 a 100 mil registros diários.

Considere que o ServiceA entregará os dados na seguinte estrutura:
```
{
    "Id": "", //identificação do registro
    "ServicoExecutado": "", //descrição do serviço que foi executado na obra
    "Data": "", //data em que o serviço foi executado
    "Responsavel": "", //nome da pessoa responsável pela execução do serviço
}
```

## Requisitos não funcionais
- O microsserviço deve estar preparado para ser tolerante a falhas, responsivo e resiliente;
- Utilize a linguagem C# .NET. Use qualquer ferramenta e estrutura com as quais se sinta confortável e elabore brevemente sua solução, detalhes de arquitetura, escolha de padrões e estruturas;
- Crie uma imagem docker do seu microsserviço e publique em algum hub a sua escolha (exemplo: dockerhub) e informe o comando para que seu serviço possa ser implantado localmente. Considere que os recursos de infra utilizados também precisam subir (banco de dados, etc).

## Dicas
Você pode usar a API do *[OpenWeatherMaps](https://openweathermap.org)* para buscar dados de temperatura.

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
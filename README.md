# Quake log parser

## Task 1 - Construa um parser para o arquivo de log games.log e exponha uma API de consulta.

O arquivo games.log é gerado pelo servidor de quake 3 arena. Ele registra todas as informações dos jogos, quando um jogo começa, quando termina, quem matou quem, quem morreu pq caiu no vazio, quem morreu machucado, entre outros.

O parser deve ser capaz de ler o arquivo, agrupar os dados de cada jogo, e em cada jogo deve coletar as informações de morte.

### Exemplo

      21:42 Kill: 1022 2 22: <world> killed Isgalamido by MOD_TRIGGER_HURT
  
  O player "Isgalamido" morreu pois estava ferido e caiu de uma altura que o matou.

      2:22 Kill: 3 2 10: Isgalamido killed Dono da Bola by MOD_RAILGUN
  
  O player "Isgalamido" matou o player Dono da Bola usando a arma Railgun.
  
Para cada jogo o parser deve gerar algo como:

    game_1: {
        total_kills: 45;
        players: ["Dono da bola", "Isgalamido", "Zeh"]
        kills: {
          "Dono da bola": 5,
          "Isgalamido": 18,
          "Zeh": 20
        }
      }



### Observações

1. Quando o `<world>` mata o player ele perde -1 kill.
2. `<world>` não é um player e não deve aparecer na lista de players e nem no dicionário de kills.
3. `total_kills` são os kills dos games, isso inclui mortes do `<world>`.

## Task 2 - Após construir o parser construa uma API que faça a exposição de um método de consulta que retorne um relatório de cada jogo.


# Requisitos

1. Use uma das seguintes linguagens: Node.js, Java, Python e C#(.NET)
2. As APIs deverão seguir o modelo RESTFul com formato JSON  
3. Faça testes unitários, suite de testes bem organizados. (Dica. De uma atenção especial a esse item!)
4. Use git e tente fazer commits pequenos e bem descritos.
5. Faça pelo menos um README explicando como fazer o setup, uma explicação da solução proposta, o mínimo de documentação para outro desenvolvedor entender seu código
6. Siga o que considera boas práticas de programação, coisas que um bom desenvolvedor olhe no seu código e não ache "feio" ou "ruim".
7. Após concluir o teste suba em um repositório privado e nos mande o link

HAVE FUN :)

# Documentação Desenvolvedor

🚀 Projeto backend desenvolvido em **C# (.NET)** com foco em:

- **Domain-Driven Design (DDD)** para modelagem rica do domínio
- **Test-Driven Development (TDD)** para garantir confiabilidade e evolução segura
- **Clean Code** para clareza, organização e legibilidade
- Princípios **SOLID** para design orientado à manutenção e escalabilidade
- **IoC (Inversão de Controle)** via Dependency Injection para desacoplamento
- Exposição via **API RESTful** acessível e bem estruturada

## 🔍 Componentes-Chave

### 🌐 API

- Exposta via ASP.NET Core
- Controladores RESTful organizados
- Utiliza injeção de dependência para acesso aos serviços

### 📄 LogParserService

- Serviço que interpreta entradas de log e gera objetos ricos do domínio
- Aplica regras de validação e parsing de forma extensível
- Totalmente coberto por testes automatizados

### 🏗 EntityFactory

- Fábrica que encapsula a construção segura das entidades
- Garante integridade e aderência às regras do domínio
- Facilitadora de testes e refatorações

## 🧼 Boas Práticas

### ✅ Clean Code

- Métodos curtos e autoexplicativos
- Nomeação clara de variáveis e classes
- Redução da complexidade ciclomática
- Comentários mínimos e precisos

### 🧩 Princípios SOLID

- **S**: Single Responsibility — cada classe com propósito único
- **O**: Open/Closed — código aberto para extensão, fechado para modificação
- **L**: Liskov Substitution — heranças seguras e consistentes
- **I**: Interface Segregation — contratos específicos por necessidade
- **D**: Dependency Inversion — dependência de abstrações, não implementações

## 🔌 IoC e Dependency Injection

- Configurado em `Infrastructure/IoC/DependencyInjection.cs`
- Centraliza o registro de serviços e fábricas
- Facilita testes e substituição de implementações

Exemplo:

```csharp
services.AddScoped<ILogParserService, LogParserService>();
services.AddScoped<IEntityFactory, EntityFactory>();



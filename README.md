# Projeto de Validação de Melhor Rota (.NET)

Este projeto tem como objetivo validar a melhor rota entre dois locais utilizando uma abordagem heurística através de uma função recursiva.

## Instruções para Execução

1. Abra a solução no **Visual Studio**.
2. Execute o projeto.
3. Certifique-se de que a solução de inicialização seja **"Routes.Api"**.

## Pacotes Utilizados

- **FluentAssertions**: Utilizado para validação de testes unitários.
- **FluentValidation**: Para validação de entradas de dados.
- **Mapster**: Responsável pelo mapeamento de objetos para entidades e vice-versa.
- **EF Core e EF Core InMemory**: Utilizados para manipulação de dados.
- **NSubstitute**: Para criação de mocks nos testes.
- **xUnit**: Framework de testes utilizado no projeto.

## Arquitetura

A estrutura do projeto foi baseada no **Domain-Driven Design (DDD)**, devido ao seu propósito de servir como um projeto de teste.

## Descrição do Código

O código consiste em realizar uma heurística utilizando uma função recursiva para validar qual é a melhor rota entre dois locais.

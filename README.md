# API Calculadora - ASP.NET Core

Este projeto é uma API REST desenvolvida em C# utilizando ASP.NET Core. A API permite realizar operações matemáticas básicas, como soma, subtração, multiplicação, divisão, média e raiz quadrada.

## Tecnologias Utilizadas

- C#
- .NET 8
- ASP.NET Core

## Como Executar o Projeto

1. **Clonar o Repositório**
   ```bash
   git clone https://github.com/OtavioRamos56/RestWithASP-NET5Udemy.git
   ```
2. **Acessar o Diretório do Projeto**
   ```bash
   cd RestWithASP-NET5Udemy\RestWithASPNETUdemy_Calculator\RestWithASPNETErudio
   ```
   Caso possua o Visual Studio só precisa abrir o projeto "RestWithASPNETErudio.sln" e executa-lo

3. **Restaurar Dependências**
   ```bash
   dotnet restore
   ```
4. **Executar o Projeto**
   ```bash
   dotnet run
   ```

A API estará disponível em `http://localhost:5057`.

| Operação     | Descrição |
|--------------|-----------|
| Soma         | sum       |
| Subtração    | sub       |
| Multiplicação| mul       |
| Divisão      | div       |
| Média        | med       |
| Raiz Quadrada| square    | 

## Endpoints Disponíveis

### Soma
- **Requisição:** `/calculator/sum/{firstNumber}/{secondNumber}`
- **Exemplo:** `/calculator/sum/10/5`
- **Resposta:** `15`

### Subtração
- **Requisição:** `/calculator/sub/{firstNumber}/{secondNumber}`
- **Exemplo:** `/calculator/sub/10/5`
- **Resposta:** `5`

### Multiplicação
- **Requisição:** `/calculator/mul/{firstNumber}/{secondNumber}`
- **Exemplo:** `/calculator/mul/10/5`
- **Resposta:** `50`

### Divisão
- **Requisição:** `/calculator/div/{firstNumber}/{secondNumber}`
- **Exemplo:** `/calculator/div/10/5`
- **Resposta:** `2`

### Média
- **Requisição:** `/calculator/med/{firstNumber}/{secondNumber}`
- **Exemplo:** `/calculator/med/10/5`
- **Resposta:** `7.5`

### Raiz Quadrada
- **Requisição:** `/calculator/square/{firstNumber}`
- **Exemplo:** `/calculator/square/25`
- **Resposta:** `5`

## Licença

Este projeto está licenciado sob os termos da Licença MIT.

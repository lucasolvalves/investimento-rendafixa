# Investimento Renda Fixa - Guia de início rápido

## O QUE É
Serviço responsável por obter as custódias de rendas fixas dos clientes.

## A QUEM SE DESTINA / OBJETIVO
Este projeto se trata de um case técnico abordado pela empresta XPTO. O objetivo deste documento é facilitar a compreenção do escopo da solução e de seu funcionamento.

## INICIANDO...
- `git clone https://github.com/lucasolvalves/investimento-rendafixa.git`

## PRÉ-REQUISITOS
- `dotnet --version`
Você deverá ver a indicação da versão do dotnet instalado.
Observe que para executar o projeto é necessario possuir a 5.0.

## EXEMPLO DE REQUEST

`GET /api/v1/rendas_fixas/{accountId}`

**Parâmetros**

|          Nome | Obrigatório |  Tipo   | Descrição                                                                                                                                                           |
| -------------:|:--------:|:-------:| --------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|     `accountId` | sim | long  | Identificador do cliente.

    curl -X GET "https://investimentorendafixa.azurewebsites.net/api/v1/rendas_fixas/123456" -H  "accept: application/json"

## DESENHO DA ARQUITETURA
![](https://raw.githubusercontent.com/lucasolvalves/investimento-cliente-custodia/main/design_investimento_cliente_custodia.png)

## SOBRE O AUTOR/ORGANIZADOR
Lucas de Oliveira Alves<br>
lucas.olvalves@gmail.com

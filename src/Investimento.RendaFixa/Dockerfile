#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Investimento.RendaFixa/Investimento.RendaFixa.csproj", "Investimento.RendaFixa/"]
COPY ["Investimento.RendaFixa.Domain/Investimento.RendaFixa.Domain.csproj", "Investimento.RendaFixa.Domain/"]
COPY ["Investimento.RendaFixa.Infrastructure/Investimento.RendaFixa.Infrastructure.csproj", "Investimento.RendaFixa.Infrastructure/"]
RUN dotnet restore "Investimento.RendaFixa/Investimento.RendaFixa.csproj"
COPY . .
WORKDIR "/src/Investimento.RendaFixa"
RUN dotnet build "Investimento.RendaFixa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Investimento.RendaFixa.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Investimento.RendaFixa.dll"]
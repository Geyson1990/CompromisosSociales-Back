#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Contable.Web.Host/Contable.Web.Host.csproj", "src/Contable.Web.Host/"]
COPY ["src/Contable.Web.Core/Contable.Web.Core.csproj", "src/Contable.Web.Core/"]
COPY ["src/Contable.Application/Contable.Application.csproj", "src/Contable.Application/"]
COPY ["src/Contable.Application.Shared/Contable.Application.Shared.csproj", "src/Contable.Application.Shared/"]
COPY ["src/Contable.Core.Shared/Contable.Core.Shared.csproj", "src/Contable.Core.Shared/"]
COPY ["src/Contable.Core/Contable.Core.csproj", "src/Contable.Core/"]
COPY ["src/Contable.EntityFrameworkCore/Contable.EntityFrameworkCore.csproj", "src/Contable.EntityFrameworkCore/"]
COPY ["src/Contable.GraphQL/Contable.GraphQL.csproj", "src/Contable.GraphQL/"]
RUN dotnet restore "src/Contable.Web.Host/Contable.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/Contable.Web.Host"
RUN dotnet build "Contable.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contable.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contable.Web.Host.dll"]

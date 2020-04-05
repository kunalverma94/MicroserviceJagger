#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["OrderDetailService/OrderDetailService.csproj", "OrderDetailService/"]
RUN dotnet restore "OrderDetailService/OrderDetailService.csproj"
COPY . .
WORKDIR "/src/OrderDetailService"
RUN dotnet build "OrderDetailService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderDetailService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderDetailService.dll"]
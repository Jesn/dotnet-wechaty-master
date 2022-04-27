#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["wechaty-grpc-webapi/wechaty-grpc-webapi.csproj", "wechaty-grpc-webapi/"]
COPY ["modules/Wechaty.Grpc.PuppetService/Wechaty.Grpc.PuppetService.csproj", "modules/Wechaty.Grpc.PuppetService/"]
COPY ["modules/Wechaty.Module.FileBox/Wechaty.Module.FileBox.csproj", "modules/Wechaty.Module.FileBox/"]
COPY ["modules/Wechaty.Module.Puppet/Wechaty.Module.Puppet.csproj", "modules/Wechaty.Module.Puppet/"]
COPY ["modules/Wechaty.Module.ReactiveQueue/Wechaty.Module.ReactiveQueue.csproj", "modules/Wechaty.Module.ReactiveQueue/"]
COPY ["modules/Wechaty.Module.Watchdog/Wechaty.Module.Watchdog.csproj", "modules/Wechaty.Module.Watchdog/"]
COPY ["modules/Wechaty.Module.EventEmitter/Wechaty.Module.EventEmitter.csproj", "modules/Wechaty.Module.EventEmitter/"]
COPY ["modules/Wechaty.Module.Common/Wechaty.Module.Common.csproj", "modules/Wechaty.Module.Common/"]
COPY ["modules/Wechaty.Module.MemoryCard/Wechaty.Module.MemoryCard.csproj", "modules/Wechaty.Module.MemoryCard/"]
RUN dotnet restore "wechaty-grpc-webapi/wechaty-grpc-webapi.csproj"
COPY . .
WORKDIR "/src/wechaty-grpc-webapi"
RUN dotnet build "wechaty-grpc-webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "wechaty-grpc-webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wechaty-grpc-webapi.dll"]

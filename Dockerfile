FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:16.17.0 AS client
ARG skip_client_build=false
WORKDIR /app
COPY /ClientApp .
RUN [[ ${skip_client_build} = true ]] && echo "Skipping npm install" || npm install
RUN [[ ${skip_client_build} = true ]] && mkdir dist || npm run-script build

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Application/Application.csproj", "Application/"]
RUN dotnet restore "Application/Application.csproj"
RUN dotnet tool install --global dotnet-ef
COPY . .
WORKDIR "/src/Application"
RUN dotnet build "Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=client /app/dist /app/dist
ENTRYPOINT ["dotnet", "Application.dll"]

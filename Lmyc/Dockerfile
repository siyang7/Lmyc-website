FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY Lmyc/Lmyc.csproj Lmyc/
COPY LmycDataLib/LmycDataLib.csproj LmycDataLib/
COPY Lmyc.Tests/Lmyc.Tests.csproj Lmyc.Tests/
COPY docker-compose.dcproj /src/
RUN dotnet restore Lmyc/Lmyc.csproj
RUN dotnet restore LmycDataLib/LmycDataLib.csproj
RUN dotnet restore Lmyc.Tests/Lmyc.Tests.csproj
COPY . .
WORKDIR /src/Lmyc
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Lmyc.dll"]

FROM microsoft/aspnetcore:2-jessie
COPY Lmyc/dist /app
WORKDIR /app
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Lmyc.dll"]
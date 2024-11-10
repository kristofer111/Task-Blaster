# OrderManagement/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and project files
COPY . .
ENV HANGFIRE_DATABASE="Host=hangfire-db;Port=5434;Database=Hangfire;Username=admin;Password=admin"

# Restore dependencies

ENV ConnectionStrings__HangfireDatabaseConnectionString=Host=hangfire-db;Port=5434;Database=Hangfire;Username=admin;Password=admin
ENV ServiceUri__TaskManagement=http://task-management-api:5212
RUN dotnet restore 
RUN dotnet build 
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# RUN dotnet ef database update --project OrderManagement.WebApi
RUN dotnet publish -c Release -o out

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80 
EXPOSE 443
ENTRYPOINT ["dotnet", "TaskBlaster.TaskManagement.Notifications.dll"]

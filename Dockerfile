# Use the ASP.NET Core base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the .NET SDK for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all project files
COPY "Shop/Shop.csproj" "Shop/"
COPY "IOC/IOC.csproj" "IOC/"
COPY "Application/Application.csproj" "Application/"
COPY "Core/Core.csproj" "Core/"
COPY "Infrastructure/Infrastructure.csproj" "Infrastructure/"

RUN dotnet restore "Shop/Shop.csproj"
COPY . .
WORKDIR "/src/Shop"
RUN dotnet build "Shop.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Shop.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Finalize the build stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.dll"]

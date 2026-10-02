# Stage 1: Build .NET Application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY PresentadorDotNet.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "PresentadorDotNet.dll"]

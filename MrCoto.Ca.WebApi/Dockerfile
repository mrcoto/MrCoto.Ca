FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy previous folder and restore them
COPY ./MrCoto.Ca.Domain ./MrCoto.Ca.Domain
COPY ./MrCoto.Ca.Application ./MrCoto.Ca.Application
COPY ./MrCoto.Ca.Infrastructure ./MrCoto.Ca.Infrastructure
COPY ./MrCoto.Ca.WebApi ./MrCoto.Ca.WebApi
RUN dotnet restore ./MrCoto.Ca.WebApi/MrCoto.Ca.WebApi.csproj

# Publish
RUN dotnet publish ./MrCoto.Ca.WebApi/MrCoto.Ca.WebApi.csproj -c Release -o ./MrCoto.Ca.WebApi/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/MrCoto.Ca.WebApi/out .
ENTRYPOINT ["dotnet", "MrCoto.Ca.WebApi.dll"]
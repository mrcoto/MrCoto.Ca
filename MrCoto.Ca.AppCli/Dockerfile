FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy previous folder and restore them
COPY ./MrCoto.Ca.Domain ./MrCoto.Ca.Domain
COPY ./MrCoto.Ca.Application ./MrCoto.Ca.Application
COPY ./MrCoto.Ca.Infrastructure ./MrCoto.Ca.Infrastructure
COPY ./MrCoto.Ca.AppCli ./MrCoto.Ca.AppCli
RUN dotnet restore ./MrCoto.Ca.AppCli/MrCoto.Ca.AppCli.csproj

# Publish
RUN dotnet publish ./MrCoto.Ca.AppCli/MrCoto.Ca.AppCli.csproj -c Release -o ./MrCoto.Ca.AppCli/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/MrCoto.Ca.AppCli/out .
CMD ["bash"]
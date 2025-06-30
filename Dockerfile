ARG builder_image=mcr.microsoft.com/dotnet/sdk:9.0
FROM ${builder_image} AS build

WORKDIR /src
# The below allows layer caching for the restore.

# Copy the Razor Pages project and the SharedLib DLL
COPY RazorPageWeddingWebsite/ ./RazorPageWeddingWebsite/
COPY bin/Content.Modelling.dll ./RazorPageWeddingWebsite/libs/

# Set working directory to the web project
WORKDIR /src/RazorPageWeddingWebsite

# Restore and publish the project
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish


#############################
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
ENV ASPNETCORE_URLS=http://*:3001
WORKDIR /app
COPY --from=build /app/publish .
COPY ./manifest.json /manifest.json
EXPOSE 3001

ENTRYPOINT ["dotnet", "RazorPageWeddingWebsite.dll"]

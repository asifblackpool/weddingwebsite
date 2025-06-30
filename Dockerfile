ARG builder_image=mcr.microsoft.com/dotnet/sdk:9.0
FROM ${builder_image} AS build

WORKDIR /src
# The below allows layer caching for the restore.
#COPY RazorPageWeddingWebsite/RazorPageWeddingWebsite.csproj .
#RUN dotnet restore
#COPY RazorPageWeddingWebsite ./
#RUN dotnet publish $csproj -c Release -o /app/publish


# 1. Copy and build normally (produces DLL in bin/Debug)
COPY RazorPageWeddingWebsite/RazorPageWeddingWebsite.csproj .
RUN dotnet restore
COPY RazorPageWeddingWebsite .
RUN dotnet build -c Debug -o /app/build

# 2. Manually copy the Windows-built DLL
# Note: Must match your exact runtime identifier (net8.0-windows8.0)
COPY RazorPageWeddingWebsite/bin/Debug/net8.0-windows8.0/Content.Modelling.dll /app/publish/

#############################
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
ENV ASPNETCORE_URLS=http://*:3001
WORKDIR /app
COPY --from=build /app/publish .
COPY ./manifest.json /manifest.json
EXPOSE 3001

ENTRYPOINT ["dotnet", "RazorPageWeddingWebsite.dll"]

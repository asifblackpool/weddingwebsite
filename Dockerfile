ARG builder_image=mcr.microsoft.com/dotnet/sdk:9.0
FROM ${builder_image} AS build

WORKDIR /src

# 1. Copy ALL shared DLLs first
COPY ../SharedDLLs/. ./SharedDLLs/

# 2. Verify DLLs exist
RUN ls -la ./SharedDLLs/ && \
    [ "$(ls -1 ./SharedDLLs/ | wc -l)" -gt 0 ] || { echo "ERROR: Asif: No DLLs found!"; exit 1; }

# 3. Copy web project files
COPY RazorPageWeddingWebsite/RazorPageWeddingWebsite.csproj .
RUN dotnet restore

# 4. Copy remaining files
COPY RazorPageWeddingWebsite/ .

# 5. Build and publish
RUN dotnet publish -c Release -o /app/publish

# 6. Copy ALL shared DLLs to output bin
RUN mkdir -p /app/publish/bin && \
    cp ./SharedDLLs/*.dll /app/publish/bin/

#############################
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
ENV ASPNETCORE_URLS=http://*:3001
WORKDIR /app
COPY --from=build /app/publish .
COPY ./manifest.json /manifest.json
EXPOSE 3001

ENTRYPOINT ["dotnet", "RazorPageWeddingWebsite.dll"]

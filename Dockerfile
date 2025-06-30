ARG builder_image=mcr.microsoft.com/dotnet/sdk:9.0
FROM ${builder_image} AS build

WORKDIR /src

# 1. Copy ONLY the DLL first (separate layer for caching)
COPY ["sharedDLLs/Content.Modelling.dll", "sharedDLLs/"]

# 2. Debug: Verify DLL exists
RUN ls -la sharedDLLs/ && \
    test -f sharedDLLs/Content.Modelling.dll || (echo "ERROR: Missing DLL!" && exit 1)

# 3. Copy project files
COPY ["RazorPageWeddingWebsite/RazorPageWeddingWebsite.csproj", "./"]
RUN dotnet restore

# 4. Copy everything else
COPY . .

# 5. Build and publish
RUN dotnet publish -c Release -o /app/publish

# 6. Copy DLL to final output
RUN mkdir -p /app/publish/bin && \
    cp sharedDLLs/Content.Modelling.dll /app/publish/bin/
#############################
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
ENV ASPNETCORE_URLS=http://*:3001
WORKDIR /app
COPY --from=build /app/publish .
COPY ./manifest.json /manifest.json
EXPOSE 3001

ENTRYPOINT ["dotnet", "RazorPageWeddingWebsite.dll"]

ARG builder_image=mcr.microsoft.com/dotnet/sdk:9.0
FROM ${builder_image} AS build

WORKDIR /src

# Copy DLL from parent directory
COPY ../sharedDLLs/Content.Modelling.dll ./sharedDLLs/

# Verify DLL exists
RUN ls -la ./sharedDLLs/ && \
    test -f ./sharedDLLs/Content.Modelling.dll || (echo "ERROR: Missing DLL!" && exit 1)

# Copy web project files
COPY RazorPageWeddingWebsite/RazorPageWeddingWebsite.csproj .
RUN dotnet restore

# Copy remaining files
COPY RazorPageWeddingWebsite/ .

# Build and publish
RUN dotnet publish -c Release -o /app/publish

# Ensure DLL is in output
RUN cp ./sharedDLLs/Content.Modelling.dll /app/publish/bin/

#############################
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
ENV ASPNETCORE_URLS=http://*:3001
WORKDIR /app
COPY --from=build /app/publish .
COPY ./manifest.json /manifest.json
EXPOSE 3001

ENTRYPOINT ["dotnet", "RazorPageWeddingWebsite.dll"]

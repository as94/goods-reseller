FROM mcr.microsoft.com/dotnet/sdk:3.1 AS backend
COPY src/. ./src
COPY GoodsReseller.sln GoodsReseller.sln
RUN dotnet restore
RUN dotnet publish --no-restore --configuration Release --output ./artifacts/backend src/GoodsReseller.Api/GoodsReseller.Api.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY --from=backend ./artifacts/backend/ .

ENTRYPOINT [ "dotnet", "GoodsReseller.Api.dll", "--updateDatabase" ]
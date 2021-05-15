FROM node:15.12.0-alpine3.10 AS frontend
COPY app/. ./app
COPY src/. ./src
COPY GoodsReseller.sln GoodsReseller.sln
WORKDIR /app
RUN yarn && yarn build-prod

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS backend
COPY --from=frontend app/. ./app
COPY --from=frontend src/. ./src
COPY --from=frontend GoodsReseller.sln GoodsReseller.sln
RUN dotnet restore
RUN dotnet publish --no-restore --configuration Release --output ./artifacts/backend src/GoodsReseller.Api/GoodsReseller.Api.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY --from=backend ./artifacts/backend/ .
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000
ENTRYPOINT [ "dotnet", "GoodsReseller.Api.dll" ]
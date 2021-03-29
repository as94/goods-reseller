FROM mcr.microsoft.com/dotnet/core/sdk:3.1
RUN cd /app && yarn && yarn build-prod
COPY . ./artifacts/backend/
RUN dotnet restore
RUN dotnet publish --no-restore --no-build --configuration Release --output ./artifacts/backend src/GoodsReseller.Api/GoodsReseller.Api.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY ./artifacts/backend/ .
EXPOSE 80
ENTRYPOINT [ "dotnet", "GoodsReseller.Api.dll" ]
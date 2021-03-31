cd ./app && yarn && yarn build-prod
cd ../
dotnet restore
dotnet publish --no-restore --no-build --configuration Release --output ./artifacts/backend src/GoodsReseller.Api/GoodsReseller.Api.csproj
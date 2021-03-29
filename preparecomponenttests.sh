echo 'Removing containers ...'
docker stop database &>/dev/null

echo 'Run mock postgres...'
docker run --rm -p 5432:5432 --name database -e POSTGRES_PASSWORD=qwe123 -d postgres:13.2

echo 'dotnet restore'
dotnet restore GoodsReseller.sln

echo 'dotnet build'
dotnet build --no-restore --configuration Release GoodsReseller.sln

mkdir -p ./artifacts/backend
dotnet publish --no-restore --no-build --configuration Release --output ./artifacts/backend src/GoodsReseller.Api/GoodsReseller.Api.csproj

echo 'Build migrator...'
MIGRATOR_ID=$(docker build -q -f ./migrator.dockerfile .)
[[ -z "$MIGRATOR_ID" ]] &&   exit 1

echo 'Wait postgres...'
docker run --rm --link database -e SLEEP_LENGTH=0.5  dadarek/wait-for-dependencies 'database:5432'

echo 'Run migrator...'
docker run --rm --link database -e DatabaseOptions__ConnectionString="Host=database;Port=5432;Database=goodsreseller;Username=postgres;Password=qwe123" $MIGRATOR_ID

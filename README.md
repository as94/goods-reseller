# Reseller API

This project implements the simple idea of reseller goods to customers.

Customer -> Landing Page (payment included) -> Order -> Supply (transfer +) -> Delivery -> Consumption (transfer -)

## Bounded contexts

1. auth
1. data catalog (products)
1. order
1. products transfer

# Tech Documentation

## Postgres & Entity Framework

### Docker
docker run --name some-postgres -p 5432:5432 -e POSTGRES_PASSWORD=qwe123 -e POSTGRES_DB=goodsreseller -d postgres

### Migrations
dotnet ef migrations add InitialCreate -s GoodsReseller.Api/GoodsReseller.Api.csproj -p  GoodsReseller.Infrastructure/GoodsReseller.Infrastructure.csproj -c GoodsResellerDbContext

dotnet ef database update -s GoodsReseller.Api/GoodsReseller.Api.csproj -p  GoodsReseller.Infrastructure/GoodsReseller.Infrastructure.csproj -c GoodsResellerDbContext

### Backup / Restore
docker exec -t your-db-container pg_dumpall -c -U postgres > dump_`date +%d-%m-%Y"_"%H_%M_%S`.sql
cat your_dump.sql | docker exec -i your-db-container psql -U postgres

### JMeter
open /usr/local/bin/jmeter

### Сертификаты
https://certbot.eff.org/lets-encrypt/ubuntuxenial-other
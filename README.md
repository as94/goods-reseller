# Reseller API

This project implements the simple idea of reseller goods to customers.

Customer -> Landing Page (payment included) -> Order -> Supply (transfer +) -> Delivery -> Consumption (transfer -)

## Bounded contexts

1. auth
1. data catalog (products)
1. order
1. products transfer

# Tech Documentation

## MongoDB

Commands
- mongo [go to shell]
- show dbs
- use goods-reseller
- db.orders.find()
- db.orders.remove({})

Migrations
https://thegreatco.com/posts/20180821/

Docker
- docker run -d -p 27017-27019:27017-27019 --name mongodb mongo:latest
- docker exec -it mongodb bash

## Postgres & Entity Framework

### Docker
docker run --name some-postgres -p 5432:5432 -e POSTGRES_PASSWORD=qwe123 -e POSTGRES_DB=goodsreseller -d postgres

### Migrations
dotnet ef migrations add InitialCreate -s GoodsReseller.Api/GoodsReseller.Api.csproj -p  GoodsReseller.Infrastructure/GoodsReseller.Infrastructure.csproj -c GoodsResellerDbContext

dotnet ef database update -s GoodsReseller.Api/GoodsReseller.Api.csproj -p  GoodsReseller.Infrastructure/GoodsReseller.Infrastructure.csproj -c GoodsResellerDbContext

## Nginx

Docker
- docker run -it --rm -d -p 8080:80 --name web -v ~/Desktop/pet-projects/GoodsReseller/landings/yoga:/usr/share/nginx/html nginx 
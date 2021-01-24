# Reseller API

This project implements the simple idea of reseller goods to customers.

Customer -> Landing Page (payment included) -> Order -> Supply (transfer +) -> Delivery -> Consumption (transfer -)

## Bounded contexts

1. auth
1. data catalog (products)
1. order
1. products transfer

# Tech Documentation

## MongoDB Migrations
https://thegreatco.com/posts/20180821/

## Nginx

docker run -it --rm -d -p 8080:80 --name web -v ~/Desktop/pet-projects/GoodsReseller/landings/yoga:/usr/share/nginx/html nginx 
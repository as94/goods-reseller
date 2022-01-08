# Техническая документация

## Генерация самоподписанного сертификата
dotnet dev-certs https

dotnet dev-certs https — trust

## Как поднять всё локально для тестирования?
1. Запустить из консоли скрипт updb.sh, который поднимет базу в контейнере и прогонит миграции
2. Запустить проект `GoodsReseller.Api Migrator`, который создаст тестового администратора (логин: test@test, пароль: qwe123) и тестовые подарочные наборы
3. Запустить проект `GoodsReseller.Api`, который поднимет бэкенд на localhost:5001. Тут можно открыть /swagger для тестирования бэкенда
4. Открыть проект /app и выполнить команду yarn watch для тестирования фронтенда. Тут можно тестировать заказ подарочного набора.
5. Чтобы зайти в админский UI, надо открыть /admin и войти из под тестового администратора. Тут можно смотреть статистику, управлять (crud) продуктами и подарочными наборами, поставками, заказами.

## Как поднять всё локально с prod данными?
1. Поднять контейнер с базой данных
2. Выполнить последнюю миграцию из бэкапа
3. Положить в `GoodsReseller.Api/wwwroot/assets` архив [отсюда](https://drive.google.com/file/d/19yhkXaRA69bObtr8IW6F341AA4KKbKqo/view?usp=drivesdk) и распаковать его
4. Запустить бэкенд и фронтенд локально

## База данных

### Миграции
cd src

dotnet ef migrations add InitialCreate -s GoodsReseller.Api/GoodsReseller.Api.csproj -p  GoodsReseller.Infrastructure/GoodsReseller.Infrastructure.csproj -c GoodsResellerDbContext

dotnet ef database update -s GoodsReseller.Api/GoodsReseller.Api.csproj -p  GoodsReseller.Infrastructure/GoodsReseller.Infrastructure.csproj -c GoodsResellerDbContext

### Сохранение / Восстановление
docker exec -t your-db-container pg_dumpall -c -U postgres > dump_`date +%d-%m-%Y"_"%H_%M_%S`.sql

поменять пароль prod пользователя на qwe123

cat your_dump.sql | docker exec -i your-db-container psql -U postgres

## Другое

### Переместить файл с prod хоста на локальный
scp root@happyboxy.ru:dump_06-01-2022_06_18_48.sql ~/Desktop

### JMeter
open /usr/local/bin/jmeter

### Сертификаты
Stop all containers and run command "sudo certbot certonly --standalone"
https://certbot.eff.org/lets-encrypt/ubuntuxenial-other

### Работа с изображениями

https://unsplash.com/

https://pixabay.com/

https://convertio.co/


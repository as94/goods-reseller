# About the Project

## For Users

This online store offers users several gift sets to choose from.

Each set card contains a photo, the set's contents, and the price. When selecting any product from the set, a photo of that product is displayed.

To order a set, click the "I Want This" button. Next, the user needs to provide contact details and delivery information. After that, the user reviews the entered data and clicks the "Place Order" button.

## For Administrators

After a user places an order, a notification is sent via Telegram about the new order. The administrator can access the admin panel to view detailed information about the order and contact the user to clarify details.

When the gift set is prepared, the administrator updates the order status to "Packed." Once the set is shipped to the client and delivered, the status is changed to "Delivered."

A separate "Supplies" section is available for tracking supply details, where suppliers, products, and costs can be recorded.

The "Products" section allows the administrator to manage gift sets and individual products, including filling in product names, descriptions, and uploading photos.

In the "Statistics" section, information on revenue, expenses, and other key metrics is available.

# Technical Documentation

## Generating a Self-Signed Certificate

dotnet dev-certs https

dotnet dev-certs https — trust

## How to Run Everything Locally for Testing

1. Run the `updb.sh` script from the console to start the database container and apply migrations.
2. Launch the `GoodsReseller.Api Migrator` project, which will create a test admin (login: `test@test`, password: `qwe123`) and sample gift sets.
3. Start the `GoodsReseller.Api` project to launch the backend on `localhost:5001`. You can access `/swagger` to test the backend.
4. Open the `/app` project and run the `yarn watch` command to test the frontend, including gift set orders.
5. To access the admin UI, go to `/admin` and log in using the test admin credentials. From there, you can view statistics and manage (CRUD) products, gift sets, supplies, and orders.

## How to Run Everything Locally with Production Data

1. Start the database container.
2. Apply the latest migration from the backup.
3. Place the archive from [here](https://drive.google.com/file/d/19yhkXaRA69bObtr8IW6F341AA4KKbKqo/view?usp=drivesdk) into the `GoodsReseller.Api/wwwroot/assets` directory and extract it.
4. Launch both the backend and frontend locally.

## Database

### Migrations

cd src

dotnet ef migrations add InitialCreate -s GoodsReseller.Api/GoodsReseller.Api.csproj -p  GoodsReseller.Infrastructure/GoodsReseller.Infrastructure.csproj -c GoodsResellerDbContext

dotnet ef database update -s GoodsReseller.Api/GoodsReseller.Api.csproj -p  GoodsReseller.Infrastructure/GoodsReseller.Infrastructure.csproj -c GoodsResellerDbContext

### Backup / Restore

docker exec -t your-db-container pg_dumpall -c -U postgres > dump_`date +%d-%m-%Y"_"%H_%M_%S`.sql

Change the production user’s password to qwe123

cat your_dump.sql | docker exec -i your-db-container psql -U postgres

## Other

### Transfer a File from the Production Host to Local

scp root@happyboxy.ru:dump_06-01-2022_06_18_48.sql ~/Desktop

### Certificates

Stop all containers and run the following command

`sudo certbot certonly --standalone`

https://certbot.eff.org/lets-encrypt/ubuntuxenial-other


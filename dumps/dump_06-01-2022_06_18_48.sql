--
-- PostgreSQL database cluster dump
--

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Drop databases (except postgres and template1)
--

DROP DATABASE goodsreseller;




--
-- Drop roles
--

DROP ROLE postgres;


--
-- Roles
--

CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS PASSWORD 'qwe123';






--
-- Databases
--

--
-- Database "template1" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 13.2 (Debian 13.2-1.pgdg100+1)
-- Dumped by pg_dump version 13.2 (Debian 13.2-1.pgdg100+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

UPDATE pg_catalog.pg_database SET datistemplate = false WHERE datname = 'template1';
DROP DATABASE template1;
--
-- Name: template1; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE template1 WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE template1 OWNER TO postgres;

\connect template1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: DATABASE template1; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE template1 IS 'default template for new databases';


--
-- Name: template1; Type: DATABASE PROPERTIES; Schema: -; Owner: postgres
--

ALTER DATABASE template1 IS_TEMPLATE = true;


\connect template1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: DATABASE template1; Type: ACL; Schema: -; Owner: postgres
--

REVOKE CONNECT,TEMPORARY ON DATABASE template1 FROM PUBLIC;
GRANT CONNECT ON DATABASE template1 TO PUBLIC;


--
-- PostgreSQL database dump complete
--

--
-- Database "goodsreseller" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 13.2 (Debian 13.2-1.pgdg100+1)
-- Dumped by pg_dump version 13.2 (Debian 13.2-1.pgdg100+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: goodsreseller; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE goodsreseller WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE goodsreseller OWNER TO postgres;

\connect goodsreseller

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: DataProtectionKeys; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."DataProtectionKeys" (
    "Id" integer NOT NULL,
    "FriendlyName" text,
    "Xml" text
);


ALTER TABLE public."DataProtectionKeys" OWNER TO postgres;

--
-- Name: DataProtectionKeys_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."DataProtectionKeys" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."DataProtectionKeys_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- Name: order_items; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.order_items (
    "Id" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "UnitPriceValue" numeric DEFAULT 0.0 NOT NULL,
    "QuantityValue" integer DEFAULT 0 NOT NULL,
    "DiscountPerUnitValue" numeric DEFAULT 0.0 NOT NULL,
    "OrderId" uuid,
    "CreationDate" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "CreationDateUtc" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "IsRemoved" boolean DEFAULT false NOT NULL,
    "LastUpdateDate" timestamp without time zone,
    "LastUpdateDateUtc" timestamp without time zone
);


ALTER TABLE public.order_items OWNER TO postgres;

--
-- Name: orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.orders (
    "Id" uuid NOT NULL,
    "Address" json DEFAULT '{}'::json NOT NULL,
    "CustomerInfo" json DEFAULT '{}'::json NOT NULL,
    "TotalCostValue" numeric DEFAULT 0.0 NOT NULL,
    "CreationDate" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "CreationDateUtc" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "LastUpdateDate" timestamp without time zone,
    "LastUpdateDateUtc" timestamp without time zone,
    "IsRemoved" boolean NOT NULL,
    "Version" integer NOT NULL,
    "Status_Id" integer DEFAULT 0 NOT NULL,
    "Status_Name" character varying(255) DEFAULT ''::character varying NOT NULL,
    "DeliveryCostValue" numeric DEFAULT 0.0 NOT NULL,
    "AddedCostValue" numeric DEFAULT 0.0 NOT NULL
);


ALTER TABLE public.orders OWNER TO postgres;

--
-- Name: products; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.products (
    "Id" uuid NOT NULL,
    "Label" character varying(255) NOT NULL,
    "Name" character varying(255) NOT NULL,
    "Description" character varying(1024) NOT NULL,
    "UnitPriceValue" numeric DEFAULT 0.0 NOT NULL,
    "DiscountPerUnitValue" numeric DEFAULT 0.0 NOT NULL,
    "ProductIds" json,
    "CreationDate" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "CreationDateUtc" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "LastUpdateDate" timestamp without time zone,
    "LastUpdateDateUtc" timestamp without time zone,
    "IsRemoved" boolean NOT NULL,
    "Version" integer NOT NULL,
    "AddedCostValue" numeric DEFAULT 0.0 NOT NULL,
    "PhotoPath" character varying(2048)
);


ALTER TABLE public.products OWNER TO postgres;

--
-- Name: supplies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.supplies (
    "Id" uuid NOT NULL,
    "SupplierInfo" json DEFAULT '{}'::json NOT NULL,
    "TotalCostValue" numeric DEFAULT 0.0 NOT NULL,
    "CreationDate" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "CreationDateUtc" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "LastUpdateDate" timestamp without time zone,
    "LastUpdateDateUtc" timestamp without time zone,
    "IsRemoved" boolean NOT NULL,
    "Version" integer DEFAULT 1 NOT NULL
);


ALTER TABLE public.supplies OWNER TO postgres;

--
-- Name: supply_items; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.supply_items (
    "Id" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "UnitPriceValue" numeric DEFAULT 0.0 NOT NULL,
    "QuantityValue" integer DEFAULT 0 NOT NULL,
    "DiscountPerUnitValue" numeric DEFAULT 0.0 NOT NULL,
    "SupplyId" uuid,
    "CreationDate" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "CreationDateUtc" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "LastUpdateDate" timestamp without time zone,
    "LastUpdateDateUtc" timestamp without time zone,
    "IsRemoved" boolean NOT NULL
);


ALTER TABLE public.supply_items OWNER TO postgres;

--
-- Name: telegram_chats; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.telegram_chats (
    "ChatId" bigint NOT NULL,
    "UserName" character varying(255) NOT NULL
);


ALTER TABLE public.telegram_chats OWNER TO postgres;

--
-- Name: telegram_chats_ChatId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.telegram_chats ALTER COLUMN "ChatId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."telegram_chats_ChatId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    "Id" uuid NOT NULL,
    "Email" character varying(255) NOT NULL,
    "PasswordHash_Value" character varying(1024) DEFAULT ''::character varying NOT NULL,
    "Role_Name" character varying(255) DEFAULT ''::character varying NOT NULL,
    "Role_Id" integer DEFAULT 0 NOT NULL,
    "CreationDate" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "CreationDateUtc" timestamp without time zone DEFAULT '0001-01-01 00:00:00'::timestamp without time zone NOT NULL,
    "LastUpdateDate" timestamp without time zone,
    "LastUpdateDateUtc" timestamp without time zone,
    "IsRemoved" boolean NOT NULL,
    "Version" integer NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Data for Name: DataProtectionKeys; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."DataProtectionKeys" ("Id", "FriendlyName", "Xml") FROM stdin;
1	key-a4e4f6a4-d8c2-40b8-8944-4b932b50eaec	<key id="a4e4f6a4-d8c2-40b8-8944-4b932b50eaec" version="1"><creationDate>2021-05-17T06:34:10.4488943Z</creationDate><activationDate>2021-05-17T06:34:05.0655543Z</activationDate><expirationDate>2021-08-15T06:34:05.0655543Z</expirationDate><descriptor deserializerType="Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel.AuthenticatedEncryptorDescriptorDeserializer, Microsoft.AspNetCore.DataProtection, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60"><descriptor><encryption algorithm="AES_256_CBC" /><validation algorithm="HMACSHA256" /><masterKey p4:requiresEncryption="true" xmlns:p4="http://schemas.asp.net/2015/03/dataProtection"><!-- Warning: the key below is in an unencrypted form. --><value>xKrWGdNhY6+z0ZEWVhx2dmxRhP80H+uv3Dz234ITdEZlV+mRPYOW8erXy4LMmpbEAk8/bZTFsnE2qL4hW/9BzQ==</value></masterKey></descriptor></descriptor></key>
2	key-f9223563-4db0-4749-8cec-365e6a34fdc7	<key id="f9223563-4db0-4749-8cec-365e6a34fdc7" version="1"><creationDate>2021-08-18T06:13:30.7516363Z</creationDate><activationDate>2021-08-18T06:13:24.8174941Z</activationDate><expirationDate>2021-11-16T06:13:24.8174941Z</expirationDate><descriptor deserializerType="Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel.AuthenticatedEncryptorDescriptorDeserializer, Microsoft.AspNetCore.DataProtection, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60"><descriptor><encryption algorithm="AES_256_CBC" /><validation algorithm="HMACSHA256" /><masterKey p4:requiresEncryption="true" xmlns:p4="http://schemas.asp.net/2015/03/dataProtection"><!-- Warning: the key below is in an unencrypted form. --><value>LH50BrTCEbMCVQDDFR/ObWe0ccM4pwGMzW57VSGae6Jx0tPpI3BYZxt9XJYB1de1waMv6MJjiBOpOpm1yAujgQ==</value></masterKey></descriptor></descriptor></key>
3	key-70dc46b8-2c42-4d46-a30c-8ae71ae91e40	<key id="70dc46b8-2c42-4d46-a30c-8ae71ae91e40" version="1"><creationDate>2021-11-30T06:09:30.4610825Z</creationDate><activationDate>2021-11-30T06:09:24.8555941Z</activationDate><expirationDate>2022-02-28T06:09:24.8555941Z</expirationDate><descriptor deserializerType="Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel.AuthenticatedEncryptorDescriptorDeserializer, Microsoft.AspNetCore.DataProtection, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60"><descriptor><encryption algorithm="AES_256_CBC" /><validation algorithm="HMACSHA256" /><masterKey p4:requiresEncryption="true" xmlns:p4="http://schemas.asp.net/2015/03/dataProtection"><!-- Warning: the key below is in an unencrypted form. --><value>XNnqp9Xd0Su0PGOaPnFarHWR4r7go4i11/le07TiejrTy+xKc09/LIyxRogf2AkHO+bo+WRDaO/tHALetV3ZUw==</value></masterKey></descriptor></descriptor></key>
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20210207134011_InitialCreate	5.0.2
20210208191112_AddProductsTable	5.0.2
20210209125711_OrdersTable	5.0.2
20210213205113_MetaToOrderItems	5.0.2
20210225101532_AddOrderStatus	5.0.2
20210226185509_AddSuppliesTable	5.0.2
20210320125830_AddDeliveryCostToOrder	5.0.2
20210502082153_AddAddedCost	5.0.2
20210502083414_AddAddedCostToProduct	5.0.2
20210515153957_DataProtection	5.0.2
20210614102354_ProductPhotoPath	5.0.2
20210710154726_AddTelegramChatsTable	5.0.2
20220101081523_AddVersions	5.0.2
20220103073054_AddRequiredFields	5.0.2
\.


--
-- Data for Name: order_items; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.order_items ("Id", "ProductId", "UnitPriceValue", "QuantityValue", "DiscountPerUnitValue", "OrderId", "CreationDate", "CreationDateUtc", "IsRemoved", "LastUpdateDate", "LastUpdateDateUtc") FROM stdin;
\.


--
-- Data for Name: orders; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.orders ("Id", "Address", "CustomerInfo", "TotalCostValue", "CreationDate", "CreationDateUtc", "LastUpdateDate", "LastUpdateDateUtc", "IsRemoved", "Version", "Status_Id", "Status_Name", "DeliveryCostValue", "AddedCostValue") FROM stdin;
\.


--
-- Data for Name: products; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.products ("Id", "Label", "Name", "Description", "UnitPriceValue", "DiscountPerUnitValue", "ProductIds", "CreationDate", "CreationDateUtc", "LastUpdateDate", "LastUpdateDateUtc", "IsRemoved", "Version", "AddedCostValue", "PhotoPath") FROM stdin;
30c5678c-d149-4a26-902d-40ca55ede00e	patterned-socks	Носки с рисунком	Несколько видов популярных носков с необычными рисунками	150	0	[]	2021-04-10 11:54:17.342002	2021-04-10 11:54:17.342002	2021-07-05 09:48:28.147741	2021-07-05 09:48:28.147741	f	7	0.0	30c5678c-d149-4a26-902d-40ca55ede00e/photo_2021-07-05_12-44-51.jpg
f0e83dc0-459b-4620-b8ec-0c5cd397e7df	deodorant	Дезодорант	Be I	360	0	[]	2021-04-10 11:56:58.919598	2021-04-10 11:56:58.919598	2021-07-05 09:48:37.719625	2021-07-05 09:48:37.719625	f	7	0.0	f0e83dc0-459b-4620-b8ec-0c5cd397e7df/photo_2021-07-05_12-44-50.jpg
e77a1098-52a3-418c-8c07-26e74a2c3f17	shower-gel	Гель для душа	Be I	200	0	[]	2021-04-10 12:01:50.108054	2021-04-10 12:01:50.108054	2021-07-05 09:48:46.529431	2021-07-05 09:48:46.529431	f	7	0.0	e77a1098-52a3-418c-8c07-26e74a2c3f17/photo_2021-07-05_12-44-47.jpg
3d75c72c-798a-43cb-9201-ccb3ce5b488c	casio-watch-f-91w-1q	Часы Casio F-91W-1Q		1500	0	[]	2021-04-10 13:18:07.265916	2021-04-10 13:18:07.265916	2021-07-05 09:50:44.697505	2021-07-05 09:50:44.697505	f	7	0.0	3d75c72c-798a-43cb-9201-ccb3ce5b488c/photo_2021-07-05_12-44-20.jpg
9ee9815e-3412-491e-b8e8-d625a21f3d53	casio-watch-a-168wa-1	Часы Casio A-168WA-1		2150	0	[]	2021-04-10 13:16:45.224622	2021-04-10 13:16:45.224622	2021-07-05 09:50:37.352119	2021-07-05 09:50:37.352119	f	6	0.0	9ee9815e-3412-491e-b8e8-d625a21f3d53/photo_2021-07-05_12-44-23.jpg
92e8b6ad-3551-4582-8744-475a94885700	soap	Мыло ручной работы	В виде гранаты	120	0	[]	2021-04-10 12:02:22.414362	2021-04-10 12:02:22.414362	2021-07-05 09:53:53.44762	2021-07-05 09:53:53.44762	f	8	0.0	92e8b6ad-3551-4582-8744-475a94885700/photo_2021-07-05_12-44-49.jpg
0ae495da-cc60-4359-ae4f-bc5a7ac1f177	mens-set-basic	Набор "Базовый"	Для настоящих чистюль! 🧼	0	0	["617adb32-0cd2-4b50-8184-38ac47b165d5","549e230a-91ea-47ce-b655-aba7d8bafc1f","966be15e-dee6-459f-a7be-33d074a9b321","92e8b6ad-3551-4582-8744-475a94885700","48cef253-2147-4740-8fbe-5739f5870bc8","e77a1098-52a3-418c-8c07-26e74a2c3f17","f0e83dc0-459b-4620-b8ec-0c5cd397e7df","30c5678c-d149-4a26-902d-40ca55ede00e"]	2021-04-10 12:06:25.051112	2021-04-10 12:06:25.051112	2022-01-04 13:40:26.59234	2022-01-04 13:40:26.59234	f	26	1090	0ae495da-cc60-4359-ae4f-bc5a7ac1f177/photo_2021-07-05_12-44-52.jpeg
ce773ba3-bc44-41b9-961a-13bb9a08e98a	mens-set-standard	Набор "Стандартный"	Для тех парней, которые любят вспомнить молодость! 🎮	0	0	["617adb32-0cd2-4b50-8184-38ac47b165d5","549e230a-91ea-47ce-b655-aba7d8bafc1f","2183f519-9560-4eb7-b764-50f481764cf4","fa402ed6-f9e8-4e2f-a757-98023421e508","534fc842-a96b-4f54-bdd1-d89a0134c2fd","55f77bb3-9c26-486d-8f6c-46953a233c1b","11708d95-54db-4ec7-925d-541d33f5d654"]	2021-04-10 13:29:19.785196	2021-04-10 13:29:19.785196	2022-01-04 13:40:35.760959	2022-01-04 13:40:35.760959	f	23	614	ce773ba3-bc44-41b9-961a-13bb9a08e98a/photo_2021-07-05_12-44-43.jpg
f4b6e42e-0733-44bf-b2fc-cf76daf056c7	mens-set-premium	Набор "Премиальный"	Для стильных ребят! 🕶	0	0	["617adb32-0cd2-4b50-8184-38ac47b165d5","549e230a-91ea-47ce-b655-aba7d8bafc1f","ab908022-676d-4347-8b3b-2d1de0bb9f81","b5f62934-b23b-4a6a-8675-b1dc31b981b2","d1a97d04-37f1-425a-ba31-7a22bebc34b2","8f467f7b-f669-4def-a483-b773cd55df04","3d75c72c-798a-43cb-9201-ccb3ce5b488c"]	2021-04-10 13:31:19.480311	2021-04-10 13:31:19.480311	2022-01-04 13:40:40.137067	2022-01-04 13:40:40.137067	f	20	1330	f4b6e42e-0733-44bf-b2fc-cf76daf056c7/photo_2021-07-05_12-44-32.jpg
549e230a-91ea-47ce-b655-aba7d8bafc1f	gift-box	Подарочная коробка		300	0	[]	2021-04-10 13:27:00.613077	2021-04-10 13:27:00.613077	\N	\N	f	1	0.0	\N
617adb32-0cd2-4b50-8184-38ac47b165d5	box-filler	Наполнитель для коробки		100	0	[]	2021-04-10 13:27:48.264721	2021-04-10 13:27:48.264721	\N	\N	f	1	0.0	\N
fa402ed6-f9e8-4e2f-a757-98023421e508	antistress-toy	Игрушка-антисресс	В виде геймпада	220	0	[]	2021-04-10 13:14:37.604228	2021-04-10 13:14:37.604228	2021-07-05 09:52:01.790956	2021-07-05 09:52:01.790956	f	7	0.0	fa402ed6-f9e8-4e2f-a757-98023421e508/photo_2021-07-05_12-44-40.jpg
966be15e-dee6-459f-a7be-33d074a9b321	hair-wax	Воск для волос	Bock Style, 50 г	380	0	[]	2021-04-10 12:03:43.556143	2021-04-10 12:03:43.556143	2021-07-05 09:49:09.901312	2021-07-05 09:49:09.901312	f	7	0.0	966be15e-dee6-459f-a7be-33d074a9b321/photo_2021-07-05_12-44-48.jpg
d1a97d04-37f1-425a-ba31-7a22bebc34b2	whiskey-glass	Стакан для виски	250 мл	800	0	[]	2021-04-10 13:25:45.632665	2021-04-10 13:25:45.632665	2021-07-05 09:51:01.811913	2021-07-05 09:51:01.811913	f	6	0.0	d1a97d04-37f1-425a-ba31-7a22bebc34b2/photo_2021-07-05_12-44-25.jpg
0286c72e-27ec-4805-bec9-54271c471721	car-aston-martin-db5	Коллекционная машинка Aston Martin DB5		465	0	[]	2021-04-10 12:56:10.980733	2021-04-10 12:56:10.980733	2021-07-05 09:48:19.41324	2021-07-05 09:48:19.41324	f	6	0.0	0286c72e-27ec-4805-bec9-54271c471721/photo_2021-07-05_12-44-39.jpg
534fc842-a96b-4f54-bdd1-d89a0134c2fd	comics-transmetropolitan	Комикс "Трансметрополитен"		980	0	[]	2021-04-10 13:13:39.779479	2021-04-10 13:13:39.779479	2021-07-05 09:50:10.347027	2021-07-05 09:50:10.347027	f	6	0.0	534fc842-a96b-4f54-bdd1-d89a0134c2fd/photo_2021-07-05_12-44-42.jpg
11708d95-54db-4ec7-925d-541d33f5d654	ps4-sub	Подписка PlayStation 4	На 3 мес.	1700	0	[]	2021-04-10 12:52:54.560999	2021-04-10 12:52:54.560999	2021-07-05 09:52:18.058949	2021-07-05 09:52:18.058949	f	8	0.0	11708d95-54db-4ec7-925d-541d33f5d654/photo_2021-07-05_12-44-35.jpg
55f77bb3-9c26-486d-8f6c-46953a233c1b	car-kinsmart-shelby-gt500	Коллекционная машинка Kinsmart "Shelby GT500"		486	0	[]	2021-04-10 13:10:12.102332	2021-04-10 13:10:12.102332	2021-07-05 09:49:57.091686	2021-07-05 09:49:57.091686	f	6	0.0	55f77bb3-9c26-486d-8f6c-46953a233c1b/photo_2021-07-05_12-44-38.jpg
3a351394-36cc-447b-bd12-d300d156ec61	x-box-sub	Подписка Xbox	На 3 мес.	1700	0	[]	2021-04-10 12:52:09.997612	2021-04-10 12:52:09.997612	2021-07-05 09:52:38.509447	2021-07-05 09:52:38.509447	f	8	0.0	3a351394-36cc-447b-bd12-d300d156ec61/photo_2021-07-05_12-44-34.jpg
2183f519-9560-4eb7-b764-50f481764cf4	mug	Кружка с рисунком	330 мл.	300	0	[]	2021-04-10 13:15:21.423778	2021-04-10 13:15:21.423778	2021-07-05 09:50:26.709984	2021-07-05 09:50:26.709984	f	6	0.0	2183f519-9560-4eb7-b764-50f481764cf4/photo_2021-07-05_12-44-40 (2).jpg
ab908022-676d-4347-8b3b-2d1de0bb9f81	beverage-cooler	Камни для виски		450	0	[]	2021-04-10 13:26:37.401753	2021-04-10 13:26:37.401753	2021-07-05 09:51:42.315825	2021-07-05 09:51:42.315825	f	44	0.0	ab908022-676d-4347-8b3b-2d1de0bb9f81/photo_2021-07-05_12-44-29.jpg
8f467f7b-f669-4def-a483-b773cd55df04	purse	Портмоне		350	0	[]	2021-04-10 13:23:48.601022	2021-04-10 13:23:48.601022	2021-07-05 09:50:54.128431	2021-07-05 09:50:54.128431	f	6	0.0	8f467f7b-f669-4def-a483-b773cd55df04/photo_2021-07-05_12-44-26.jpg
48cef253-2147-4740-8fbe-5739f5870bc8	shampoo	Шампунь	Be I	200	0	[]	2021-04-10 11:57:41.904913	2021-04-10 11:57:41.904913	2021-07-05 09:48:53.697778	2021-07-05 09:48:53.697778	f	7	0.0	48cef253-2147-4740-8fbe-5739f5870bc8/photo_2021-07-05_12-44-45.jpg
b5f62934-b23b-4a6a-8675-b1dc31b981b2	sunglasses	Очки солнцезащитные		670	0	[]	2021-04-10 13:24:31.019065	2021-04-10 13:24:31.019065	2021-07-05 09:51:11.450814	2021-07-05 09:51:11.450814	f	22	0.0	b5f62934-b23b-4a6a-8675-b1dc31b981b2/photo_2021-07-05_12-44-28.jpg
\.


--
-- Data for Name: supplies; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.supplies ("Id", "SupplierInfo", "TotalCostValue", "CreationDate", "CreationDateUtc", "LastUpdateDate", "LastUpdateDateUtc", "IsRemoved", "Version") FROM stdin;
fa311951-39ee-4be1-942a-2c9a01154efc	{"Name":"Ozon"}	6101	2021-04-10 13:37:46.970687	2021-04-10 13:37:46.970687	\N	\N	f	1
43c364de-1a89-410f-ab09-9b6504f8a544	{"Name":"Wildberries"}	3943	2021-04-10 13:42:16.490874	2021-04-10 13:42:16.490874	\N	\N	f	1
ee7f5b17-6bc2-45b0-ba8a-b0edb85421bc	{"Name":"Ali Express"}	4074	2021-04-10 13:44:30.29077	2021-04-10 13:44:30.29077	\N	\N	f	1
\.


--
-- Data for Name: supply_items; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.supply_items ("Id", "ProductId", "UnitPriceValue", "QuantityValue", "DiscountPerUnitValue", "SupplyId", "CreationDate", "CreationDateUtc", "LastUpdateDate", "LastUpdateDateUtc", "IsRemoved") FROM stdin;
8befd7fd-9f09-495c-9394-1a2701ee0dc6	3d75c72c-798a-43cb-9201-ccb3ce5b488c	1500	2	0	fa311951-39ee-4be1-942a-2c9a01154efc	2021-04-10 13:37:46.971353	2021-04-10 13:37:46.971353	\N	\N	f
7554c591-91e2-461d-b432-04ac8b70e1dc	2183f519-9560-4eb7-b764-50f481764cf4	300	3	0	fa311951-39ee-4be1-942a-2c9a01154efc	2021-04-10 13:37:46.971512	2021-04-10 13:37:46.971512	\N	\N	f
0d2def61-99bb-4a87-ada1-e5799de0cb41	d1a97d04-37f1-425a-ba31-7a22bebc34b2	800	1	0	fa311951-39ee-4be1-942a-2c9a01154efc	2021-04-10 13:37:46.971513	2021-04-10 13:37:46.971513	\N	\N	f
10720b27-909c-4ba1-bed0-3b5b098f8035	ab908022-676d-4347-8b3b-2d1de0bb9f81	450	1	0	fa311951-39ee-4be1-942a-2c9a01154efc	2021-04-10 13:37:46.971514	2021-04-10 13:37:46.971514	\N	\N	f
d3673a2e-a0bd-4435-aa9d-fe5d8c5d1018	55f77bb3-9c26-486d-8f6c-46953a233c1b	486	1	0	fa311951-39ee-4be1-942a-2c9a01154efc	2021-04-10 13:37:46.971514	2021-04-10 13:37:46.971514	\N	\N	f
71470d66-96b1-4d11-b22b-89b4131e130f	0286c72e-27ec-4805-bec9-54271c471721	465	1	0	fa311951-39ee-4be1-942a-2c9a01154efc	2021-04-10 13:37:46.971516	2021-04-10 13:37:46.971516	\N	\N	f
0544089f-cf14-4fb2-9b50-8a8ae5663754	92e8b6ad-3551-4582-8744-475a94885700	124	2	0	43c364de-1a89-410f-ab09-9b6504f8a544	2021-04-10 13:42:16.492512	2021-04-10 13:42:16.492512	\N	\N	f
6c3431ab-0376-433b-a292-d902a1cd7f9d	48cef253-2147-4740-8fbe-5739f5870bc8	200	2	0	43c364de-1a89-410f-ab09-9b6504f8a544	2021-04-10 13:42:16.49253	2021-04-10 13:42:16.49253	\N	\N	f
7bd3e580-fde7-4c02-b3eb-9fe0b84f1c03	e77a1098-52a3-418c-8c07-26e74a2c3f17	200	2	0	43c364de-1a89-410f-ab09-9b6504f8a544	2021-04-10 13:42:16.492537	2021-04-10 13:42:16.492537	\N	\N	f
a5bb7db6-e000-4b6f-bf89-1a02060ab0ed	549e230a-91ea-47ce-b655-aba7d8bafc1f	300	4	0	43c364de-1a89-410f-ab09-9b6504f8a544	2021-04-10 13:42:16.492544	2021-04-10 13:42:16.492544	\N	\N	f
cb54055a-4deb-486a-bf9c-c25c945df823	534fc842-a96b-4f54-bdd1-d89a0134c2fd	950	1	0	43c364de-1a89-410f-ab09-9b6504f8a544	2021-04-10 13:42:16.492558	2021-04-10 13:42:16.492558	\N	\N	f
101dc81c-143c-4a3f-930b-7e2d643f99fe	f0e83dc0-459b-4620-b8ec-0c5cd397e7df	380	1	0	43c364de-1a89-410f-ab09-9b6504f8a544	2021-04-10 13:42:16.492566	2021-04-10 13:42:16.492566	\N	\N	f
be7d8f2d-1aaa-4d27-8ba4-a2e3ac66ad8a	966be15e-dee6-459f-a7be-33d074a9b321	365	1	0	43c364de-1a89-410f-ab09-9b6504f8a544	2021-04-10 13:42:16.492573	2021-04-10 13:42:16.492573	\N	\N	f
32e85b80-9913-4e03-85bf-1d2d7a18ed1c	b5f62934-b23b-4a6a-8675-b1dc31b981b2	672	2	0	ee7f5b17-6bc2-45b0-ba8a-b0edb85421bc	2021-04-10 13:44:30.290782	2021-04-10 13:44:30.290782	\N	\N	f
e9a64713-3179-4c5a-bb96-bc162793ba9c	30c5678c-d149-4a26-902d-40ca55ede00e	150	9	0	ee7f5b17-6bc2-45b0-ba8a-b0edb85421bc	2021-04-10 13:44:30.290788	2021-04-10 13:44:30.290788	\N	\N	f
fc17fb78-41ff-419b-9b1d-96eb6dcdaa08	fa402ed6-f9e8-4e2f-a757-98023421e508	220	2	0	ee7f5b17-6bc2-45b0-ba8a-b0edb85421bc	2021-04-10 13:44:30.290792	2021-04-10 13:44:30.290792	\N	\N	f
f2373636-f2c8-4358-bd87-c8ef5e2dda08	617adb32-0cd2-4b50-8184-38ac47b165d5	240	1	0	ee7f5b17-6bc2-45b0-ba8a-b0edb85421bc	2021-04-10 13:44:30.290796	2021-04-10 13:44:30.290796	\N	\N	f
1a87804e-3fe3-4bff-ade0-24de4301045c	8f467f7b-f669-4def-a483-b773cd55df04	350	2	0	ee7f5b17-6bc2-45b0-ba8a-b0edb85421bc	2021-04-10 13:44:30.2908	2021-04-10 13:44:30.2908	\N	\N	f
\.


--
-- Data for Name: telegram_chats; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.telegram_chats ("ChatId", "UserName") FROM stdin;
167585499	anatoly_sorokin
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users ("Id", "Email", "PasswordHash_Value", "Role_Name", "Role_Id", "CreationDate", "CreationDateUtc", "LastUpdateDate", "LastUpdateDateUtc", "IsRemoved", "Version") FROM stdin;
3ccccb5a-3b1a-4f33-82cb-990f839e4b12	anatoly.sorokin.personal@gmail.com	AQAQJwAAjiZfuUdv5QwMCyuB2C08OpRc+TviSQ35GDzVf2PQ5k5G70d8fDOHEgAW6vplTDxd	Admin	1	2021-03-31 17:53:11.011378	2021-03-31 17:53:11.011378	\N	\N	f	1
c38ea469-57a0-4a37-b4d5-438410bf7f9b	lukyanovst4s@mail.ru	AQAQJwAAapOxJ8s4+sGCwqTlsNIJakh3Ae+fFFkB9oBMP23Tc2YDPcUBqjqALNOer9vkf14S	Admin	1	2021-04-02 09:27:08.058389	2021-04-02 09:27:08.058389	\N	\N	f	1
\.


--
-- Name: DataProtectionKeys_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."DataProtectionKeys_Id_seq"', 3, true);


--
-- Name: telegram_chats_ChatId_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."telegram_chats_ChatId_seq"', 1, false);


--
-- Name: DataProtectionKeys PK_DataProtectionKeys; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DataProtectionKeys"
    ADD CONSTRAINT "PK_DataProtectionKeys" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: order_items PK_order_items; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT "PK_order_items" PRIMARY KEY ("Id");


--
-- Name: orders PK_orders; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT "PK_orders" PRIMARY KEY ("Id");


--
-- Name: products PK_products; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT "PK_products" PRIMARY KEY ("Id");


--
-- Name: supplies PK_supplies; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.supplies
    ADD CONSTRAINT "PK_supplies" PRIMARY KEY ("Id");


--
-- Name: supply_items PK_supply_items; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.supply_items
    ADD CONSTRAINT "PK_supply_items" PRIMARY KEY ("Id");


--
-- Name: telegram_chats PK_telegram_chats; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.telegram_chats
    ADD CONSTRAINT "PK_telegram_chats" PRIMARY KEY ("ChatId");


--
-- Name: users PK_users; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT "PK_users" PRIMARY KEY ("Id");


--
-- Name: IX_order_items_OrderId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_order_items_OrderId" ON public.order_items USING btree ("OrderId");


--
-- Name: IX_supply_items_SupplyId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_supply_items_SupplyId" ON public.supply_items USING btree ("SupplyId");


--
-- Name: IX_users_Email; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_users_Email" ON public.users USING btree ("Email");


--
-- Name: order_items FK_order_items_orders_OrderId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT "FK_order_items_orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES public.orders("Id") ON DELETE RESTRICT;


--
-- Name: supply_items FK_supply_items_supplies_SupplyId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.supply_items
    ADD CONSTRAINT "FK_supply_items_supplies_SupplyId" FOREIGN KEY ("SupplyId") REFERENCES public.supplies("Id") ON DELETE RESTRICT;


--
-- PostgreSQL database dump complete
--

--
-- Database "postgres" dump
--

--
-- PostgreSQL database dump
--

-- Dumped from database version 13.2 (Debian 13.2-1.pgdg100+1)
-- Dumped by pg_dump version 13.2 (Debian 13.2-1.pgdg100+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE postgres;
--
-- Name: postgres; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE postgres WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE postgres OWNER TO postgres;

\connect postgres

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: DATABASE postgres; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE postgres IS 'default administrative connection database';


--
-- PostgreSQL database dump complete
--

--
-- PostgreSQL database cluster dump complete
--


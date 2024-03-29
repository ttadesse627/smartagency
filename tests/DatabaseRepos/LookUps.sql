-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: smartagency
-- ------------------------------------------------------
-- Server version	8.0.32
/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */
;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */
;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */
;
/*!50503 SET NAMES utf8mb4 */
;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */
;
/*!40103 SET TIME_ZONE='+00:00' */
;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */
;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */
;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */
;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */
;
--
-- Table structure for table `lookups`
--
DROP TABLE IF EXISTS `lookups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */
;
/*!50503 SET character_set_client = utf8mb4 */
;
CREATE TABLE `lookups` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `CategoryId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `ModifiedAt` datetime(6) NOT NULL,
  `CreatedBy` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ModifiedBy` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_LookUps_CategoryId` (`CategoryId`),
  CONSTRAINT `FK_LookUps_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `categories` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */
;
--
-- Dumping data for table `lookups`
--
LOCK TABLES `lookups` WRITE;
/*!40000 ALTER TABLE `lookups` DISABLE KEYS */
;
INSERT INTO `lookups`
VALUES (
    '011601d8-18f4-4887-92ac-10d667f42868',
    '79883eea-bd7b-4620-8a63-3415dcadb975',
    'Airlines 2',
    '2023-05-15 08:24:00.750038',
    '2023-05-15 08:24:00.750040',
    NULL,
    NULL
  ),
(
    '02626010-dc9c-4edc-a749-cd8a8652e84b',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'City',
    '2023-05-15 08:17:25.817847',
    '2023-05-15 08:17:25.817847',
    NULL,
    NULL
  ),
(
    '02e12b3c-2377-43aa-81a0-699f7535cb1d',
    '7859bf3f-7013-4587-a840-94b18fa1e5f6',
    'Experience 1',
    '2023-05-14 19:23:26.225062',
    '2023-05-14 19:23:26.225062',
    NULL,
    NULL
  ),
(
    '0329173d-f4a2-4fbe-bc6c-392bf3cb58dd',
    '3048b353-039d-41b6-8690-a9aaa2e679cf',
    'Visa Type 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '0468367a-4d6e-4eec-b34b-b67576dfa5c4',
    '5b912c00-9df3-47a1-a525-410abf239616',
    'Health 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '0f375bb3-2164-4fc2-b43b-94db0ee0bff7',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Branch',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '13cad7f2-e470-4a90-a923-50eb59312d29',
    '813b098f-a77b-43f1-9782-e951257bccb8',
    'User Status 2',
    '2023-05-15 08:28:11.711560',
    '2023-05-15 08:28:11.711560',
    NULL,
    NULL
  ),
(
    '19ed86c2-c06d-4ef8-bce7-49b778f878c9',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Position',
    '2023-05-15 08:29:26.821057',
    '2023-05-15 08:29:26.821057',
    NULL,
    NULL
  ),
(
    '1a1f1de1-7253-4398-93ea-b16ef314656d',
    'a479edb5-c4ea-4aeb-9c6c-a7d0b828eff5',
    'Qualification Type 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '1bafe73b-bf00-4e60-be98-c2ba963a0150',
    '5d4deb36-fe17-4260-bbe3-5a21bed9477f',
    'Position 1',
    '2023-05-15 08:30:42.839352',
    '2023-05-15 08:30:42.839353',
    NULL,
    NULL
  ),
(
    '2141ba2b-93fc-4939-b501-45adebba0754',
    '00fa1a8e-ac70-400e-8f37-20010f81a27a',
    'Award 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '2923cce7-3906-42aa-90ed-6f22c850f270',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Visa Type',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '30cda564-b661-4dff-8603-e646dd18cd45',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Port of Arrival',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '323683d6-5bb7-46b7-b09e-b93bce7154dd',
    '689c1c0f-8d4d-4f98-a6b4-bda48aa474db',
    'City 1',
    '2023-05-15 08:18:09.257094',
    '2023-05-15 08:18:09.257095',
    NULL,
    NULL
  ),
(
    '348ee93d-c1e2-44c7-93b8-2b5ad5116ef7',
    '7859bf3f-7013-4587-a840-94b18fa1e5f6',
    'Experience 2',
    '2023-05-14 19:23:54.043834',
    '2023-05-14 19:23:54.043835',
    NULL,
    NULL
  ),
(
    '38811adc-0018-413d-aa0d-41cb9f2f315f',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Job Title',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '3c0afa94-4d60-47fb-804a-92624785d035',
    'bde559f3-8634-48a8-b0a5-396b3cf0fb93',
    'Broker 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '41e4b379-89c1-4f9f-b2a1-04f74160e71b',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Region',
    '2023-05-14 19:29:35.080941',
    '2023-05-14 19:29:35.080941',
    NULL,
    NULL
  ),
(
    '45a92e7d-0c88-402c-970b-1ed68bee8655',
    'bfb54662-9cf1-4d00-8776-0000d0e4eadf',
    'Ticket Office 2',
    '2023-05-15 08:20:57.454209',
    '2023-05-15 08:20:57.454209',
    NULL,
    NULL
  ),
(
    '46444fe3-6b0e-410a-a316-67732abc430a',
    '79883eea-bd7b-4620-8a63-3415dcadb975',
    'Airlines 1',
    '2023-05-15 08:23:10.875004',
    '2023-05-15 08:23:10.875005',
    NULL,
    NULL
  ),
(
    '4b5cc72e-676c-4b1c-b13b-036034d8349f',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Issued Place',
    '2023-05-15 05:39:45.951362',
    '2023-05-15 05:39:45.951363',
    NULL,
    NULL
  ),
(
    '5255916b-7f80-47fb-9671-ef58e97dc4ba',
    '60209c9d-47b4-497b-8abd-94a753814a86',
    'Religion 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '5398d264-a862-476e-a76c-f2ab1592919d',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Technical Skill',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '578c7f2b-27b0-4b2a-9826-216b11652973',
    'bde559f3-8634-48a8-b0a5-396b3cf0fb93',
    'Broker 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '57f779bc-bbd4-457e-9737-1dbb2623bd3c',
    '06e6add8-7147-4cb3-913c-6968183a7b44',
    'Language 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '5a63f695-496c-461c-991f-d679d1f8485d',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Broker Name',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '5da51288-d243-4882-acd5-66d9e21faeb9',
    'd9be1a87-57be-4424-a982-1970098083d7',
    'Marital Status 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '68ccacf6-c656-4447-b8cb-9a2f102f7e93',
    '60209c9d-47b4-497b-8abd-94a753814a86',
    'Religion 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '6d030bc0-3dd3-40a3-a49e-abfcb222a47f',
    '36b20c6a-9959-4466-851a-47ca00ef03df',
    'Region 2',
    '2023-05-14 19:31:05.930678',
    '2023-05-14 19:31:05.930678',
    NULL,
    NULL
  ),
(
    '6f29035f-a5d1-4943-ad6c-2b6fb496bfe3',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Airlines',
    '2023-05-15 08:21:51.949654',
    '2023-05-15 08:21:51.949655',
    NULL,
    NULL
  ),
(
    '73997878-adda-45db-84de-241ac1e2c50c',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Country',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '7c652443-e3cd-4547-8ca0-f1a4af73bc07',
    '69d4f430-2e92-409d-8a59-0cb07289ff82',
    'Relationship 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '7d33be1d-1ee9-40b0-83b6-97f1f79c846d',
    'cc58bbf2-f161-42e5-add4-37893466a988',
    'Country 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '8001b16e-07d0-4fdb-a5fe-3da855193156',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Health',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '80911c3a-078b-40d4-9999-a60ef0b77fd8',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Salary',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '809fbd76-6e42-48c9-9f4b-e6b8b58d3f9b',
    'a19a1b7e-67fc-4c0f-820d-7fc8ced1ba3b',
    'Salary 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '826d7ff9-dac6-4286-bbb6-f4e0277240a8',
    '75052304-9f92-4da6-af71-81716cabc5e2',
    'Technical Skill 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '86bba764-a52e-4e1a-a767-1c64f4cc2353',
    '5b912c00-9df3-47a1-a525-410abf239616',
    'Health 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '8cc8dd75-5ca9-483c-b374-51c758ad70d4',
    'ab6faccd-c97a-4c64-a378-7b23688e6f4e',
    'Nationality 2',
    '2023-05-15 08:14:15.488100',
    '2023-05-15 08:14:15.488101',
    NULL,
    NULL
  ),
(
    '8cee4417-5506-48c2-942b-e033962994f1',
    'ee0b4777-25b4-49d6-ad2a-833981a2295e',
    'Priority 1',
    '2023-05-15 08:42:44.069888',
    '2023-05-15 08:42:44.069889',
    NULL,
    NULL
  ),
(
    '90774ffc-e2f5-48f1-a20a-5fac0bfaacee',
    '955c3bba-3efc-4012-bbb9-a4c31f65612a',
    'Job Title 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '90c01598-c8e3-4795-9b12-9ac8849d5e44',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Relationship',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '99cf5eeb-39ed-4f74-8b8c-8eb27919191b',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Priority',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '9a315e76-7445-4b70-aa6a-f0e0c6a00383',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Marital Status',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    '9da90c6e-9bee-4b66-bf4b-f16161c87f8a',
    'f296e285-80c1-4e5b-aa91-87274b8bd67c',
    'Branch 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'a8bf96de-8c09-4e4e-a667-326bc84b8372',
    'a479edb5-c4ea-4aeb-9c6c-a7d0b828eff5',
    'Qualification Type 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'a92c2d65-29b8-4307-a629-acf8b87f2523',
    '813b098f-a77b-43f1-9782-e951257bccb8',
    'User Status 1',
    '2023-05-15 08:27:50.885133',
    '2023-05-15 08:27:50.885134',
    NULL,
    NULL
  ),
(
    'aabe0b7f-6fcf-442d-b2d7-9b9213bd3bb4',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'User Status',
    '2023-05-15 08:26:16.994829',
    '2023-05-15 08:26:16.994830',
    NULL,
    NULL
  ),
(
    'ab8ff66b-0883-4907-a03a-352fd47bef9e',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Ticket Office',
    '2023-05-15 08:19:49.096330',
    '2023-05-15 08:19:49.096332',
    NULL,
    NULL
  ),
(
    'abe48136-f37f-4c3b-b071-8c3174d2c863',
    '2d9ef769-6d03-4406-9849-430ff9723778',
    'Followup Status 2',
    '2023-05-15 08:09:39.513786',
    '2023-05-15 08:09:39.513787',
    NULL,
    NULL
  ),
(
    'ac39848c-6d2a-46f7-8970-18b01eef9ddc',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Award',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'acb116fd-c305-419c-83d1-4403cbf6bc52',
    '2d9ef769-6d03-4406-9849-430ff9723778',
    'Followup Status 1',
    '2023-05-15 08:09:16.933386',
    '2023-05-15 08:09:16.933387',
    NULL,
    NULL
  ),
(
    'af24b876-a25d-4012-8be7-3c9ca74b3566',
    '5d4deb36-fe17-4260-bbe3-5a21bed9477f',
    'Position 2',
    '2023-05-15 08:31:03.951642',
    '2023-05-15 08:31:03.951642',
    NULL,
    NULL
  ),
(
    'b03f7bf3-179e-49e8-9ea4-f0e467bcf911',
    'ee0b4777-25b4-49d6-ad2a-833981a2295e',
    'Priority 2',
    '2023-05-15 08:43:03.923499',
    '2023-05-15 08:43:03.923500',
    NULL,
    NULL
  ),
(
    'b40ad1d3-0b71-47ec-97a0-aad2f945ed79',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Qualification Type',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'b9e1f738-78a6-4c12-8f71-ba3700e62fab',
    'bfb54662-9cf1-4d00-8776-0000d0e4eadf',
    'Ticket Office 1',
    '2023-05-15 08:20:39.563292',
    '2023-05-15 08:20:39.563293',
    NULL,
    NULL
  ),
(
    'bdc2c6f6-3dd3-4b05-ba7b-c33723071a7c',
    'ab6faccd-c97a-4c64-a378-7b23688e6f4e',
    'Nationality 1',
    '2023-05-15 08:13:38.775930',
    '2023-05-15 08:13:38.775930',
    NULL,
    NULL
  ),
(
    'bf56140e-5787-41e6-90ac-5b74905caac9',
    '689c1c0f-8d4d-4f98-a6b4-bda48aa474db',
    'City 2',
    '2023-05-15 08:18:22.759151',
    '2023-05-15 08:18:22.759151',
    NULL,
    NULL
  ),
(
    'c01fc41e-2057-4e0b-b284-d720b698ea67',
    '36b20c6a-9959-4466-851a-47ca00ef03df',
    'Region 1',
    '2023-05-14 19:30:47.607121',
    '2023-05-14 19:30:47.607122',
    NULL,
    NULL
  ),
(
    'c0fe6c0e-1512-4e3f-956d-46645844e167',
    '69d4f430-2e92-409d-8a59-0cb07289ff82',
    'Relationship 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'c5302c62-c535-4a9a-a4bc-ddf7aa3bf7be',
    'e0724ed3-8a71-49bc-a231-e5008d481c36',
    'Port of Arrival 1',
    '2023-05-15 08:45:37.400541',
    '2023-05-15 08:45:37.400542',
    NULL,
    NULL
  ),
(
    'c929d520-b52e-40ca-9fcd-0b3fa53b129e',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Language',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'cfb5c4e0-fc85-422d-9dac-3fa93d6fb8b7',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Level of Qualification',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'd8571412-e0a0-436b-a18d-80b6ee4f6de6',
    'f296e285-80c1-4e5b-aa91-87274b8bd67c',
    'Branch 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'd914296b-fb04-45e8-a127-26ca4ba17778',
    '955c3bba-3efc-4012-bbb9-a4c31f65612a',
    'Job Title 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'dbb51fc9-c0f3-40c7-9468-4fafc977782f',
    'f03489f8-12b1-4102-912c-5b5e9d25b15b',
    'Level of Qualification 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'e21f9284-c916-4b50-865f-fdeff0731ebc',
    'cc58bbf2-f161-42e5-add4-37893466a988',
    'Country 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'e77884eb-89db-49eb-8ffd-1579ffa607bb',
    '1dc479ab-fe84-4ca8-828f-9a21de7434e7',
    'Issued Place 2',
    '2023-05-15 05:43:08.760617',
    '2023-05-15 05:43:08.760618',
    NULL,
    NULL
  ),
(
    'ea7b7d14-1771-4430-bfec-3b1c703d5d12',
    'd9be1a87-57be-4424-a982-1970098083d7',
    'Marital Status 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'ee0f3a62-3d1d-4ad6-81e9-6ba35888ec29',
    '1dc479ab-fe84-4ca8-828f-9a21de7434e7',
    'Issued Place 1',
    '2023-05-15 05:42:28.711150',
    '2023-05-15 05:42:28.711151',
    NULL,
    NULL
  ),
(
    'ee1dfcca-6066-4eed-bd41-f4de7dd2045c',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Followup Status',
    '2023-05-15 08:06:55.331944',
    '2023-05-15 08:06:55.331945',
    NULL,
    NULL
  ),
(
    'ee7d3a52-24e5-4dd5-81e2-964444f0fadb',
    'f03489f8-12b1-4102-912c-5b5e9d25b15b',
    'Level of Qualification 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'f03bbbb7-a928-4e47-b8b2-f7cfb89b782f',
    'e0724ed3-8a71-49bc-a231-e5008d481c36',
    'Port of Arrival 2',
    '2023-05-15 08:45:57.297646',
    '2023-05-15 08:45:57.297647',
    NULL,
    NULL
  ),
(
    'f38e9df7-edf3-44d8-9646-53c289c6daaa',
    '75052304-9f92-4da6-af71-81716cabc5e2',
    'Technical Skill 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'f3c1ba11-148a-470a-8b1d-8c1d78d7845f',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Nationality',
    '2023-05-15 08:12:03.276200',
    '2023-05-15 08:12:03.276201',
    NULL,
    NULL
  ),
(
    'f617f3f7-3771-4cef-b19e-d7e52bd6b2ce',
    '3048b353-039d-41b6-8690-a9aaa2e679cf',
    'Visa Type 1',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'f7c65b01-4ae6-47ab-9e85-c804b3255b3f',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Experience',
    '2023-05-14 19:16:58.126551',
    '2023-05-14 19:16:58.126552',
    NULL,
    NULL
  ),
(
    'fc6b063c-90f2-4f50-b2d1-f8fb1aa44ef6',
    'a19a1b7e-67fc-4c0f-820d-7fc8ced1ba3b',
    'Salary 1',
    '2023-05-14 19:36:12.286223',
    '2023-05-14 19:36:12.286223',
    NULL,
    NULL
  ),
(
    'fd472986-1cff-4d10-b421-8ded83039c37',
    '06e6add8-7147-4cb3-913c-6968183a7b44',
    'Language 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'fe6a63ea-6a21-4b05-b6b4-6b1863b0ad9c',
    '8aec3c2a-96ba-46ce-8a4b-14cf557fd621',
    'Religion',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  ),
(
    'fef46287-75df-4bd4-9704-54ec0e60ef5e',
    '00fa1a8e-ac70-400e-8f37-20010f81a27a',
    'Award 2',
    '2023-05-05 04:00:25.118661',
    '2023-05-05 04:00:25.118661',
    NULL,
    NULL
  );
/*!40000 ALTER TABLE `lookups` ENABLE KEYS */
;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */
;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */
;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */
;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */
;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */
;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */
;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */
;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */
;
-- Dump completed on 2023-05-15 11:58:14
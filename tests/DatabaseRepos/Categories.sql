-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: smartagency
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES 
('00fa1a8e-ac70-400e-8f37-20010f81a27a','Award'),
('06e6add8-7147-4cb3-913c-6968183a7b44','Language'),
('1dc479ab-fe84-4ca8-828f-9a21de7434e7','Issued Place'),
('2d9ef769-6d03-4406-9849-430ff9723778','Followup Status'),
('3048b353-039d-41b6-8690-a9aaa2e679cf','Visa Type'),
('36b20c6a-9959-4466-851a-47ca00ef03df','Region'),
('5b912c00-9df3-47a1-a525-410abf239616','Health'),
('5d4deb36-fe17-4260-bbe3-5a21bed9477f','Position'),
('60209c9d-47b4-497b-8abd-94a753814a86','Religion'),
('689c1c0f-8d4d-4f98-a6b4-bda48aa474db','City'),
('69d4f430-2e92-409d-8a59-0cb07289ff82','Relationship'),
('75052304-9f92-4da6-af71-81716cabc5e2','Technical Skill'),
('7859bf3f-7013-4587-a840-94b18fa1e5f6','Experience'),
('79883eea-bd7b-4620-8a63-3415dcadb975','Airlines'),
('813b098f-a77b-43f1-9782-e951257bccb8','User Status'),
('8aec3c2a-96ba-46ce-8a4b-14cf557fd621','Category'),
('955c3bba-3efc-4012-bbb9-a4c31f65612a','Job Title'),
('a19a1b7e-67fc-4c0f-820d-7fc8ced1ba3b','Salary'),
('a479edb5-c4ea-4aeb-9c6c-a7d0b828eff5','Qualification Type'),
('ab6faccd-c97a-4c64-a378-7b23688e6f4e','Nationality'),
('bde559f3-8634-48a8-b0a5-396b3cf0fb93','Broker Name'),
('bfb54662-9cf1-4d00-8776-0000d0e4eadf','Ticket Office'),
('cc58bbf2-f161-42e5-add4-37893466a988','Country'),
('d9be1a87-57be-4424-a982-1970098083d7','Marital Status'),
('e0724ed3-8a71-49bc-a231-e5008d481c36','Port of Arrival'),
('ee0b4777-25b4-49d6-ad2a-833981a2295e','Priority'),
('f03489f8-12b1-4102-912c-5b5e9d25b15b','Level of Qualification'),
('f296e285-80c1-4e5b-aa91-87274b8bd67c','Branch');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-15 11:54:43

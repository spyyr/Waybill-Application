USE [master]
GO
/****** Object:  Database [Formatka]    Script Date: 05.06.2019 13:03:38 ******/
CREATE DATABASE [Formatka]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Formatka', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Formatka.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Formatka_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Formatka_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Formatka] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Formatka].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Formatka] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Formatka] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Formatka] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Formatka] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Formatka] SET ARITHABORT OFF 
GO
ALTER DATABASE [Formatka] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Formatka] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Formatka] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Formatka] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Formatka] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Formatka] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Formatka] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Formatka] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Formatka] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Formatka] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Formatka] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Formatka] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Formatka] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Formatka] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Formatka] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Formatka] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Formatka] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Formatka] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Formatka] SET  MULTI_USER 
GO
ALTER DATABASE [Formatka] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Formatka] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Formatka] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Formatka] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Formatka] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Formatka] SET QUERY_STORE = OFF
GO
USE [Formatka]
GO
/****** Object:  Table [dbo].[Computers]    Script Date: 05.06.2019 13:03:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Computers](
	[ComputerID] [int] IDENTITY(1,1) NOT NULL,
	[ModelName] [nchar](100) NULL,
	[Price] [int] NULL,
	[Weight] [int] NULL,
	[HasAdapter] [bit] NULL,
 CONSTRAINT [PK_Computer] PRIMARY KEY CLUSTERED 
(
	[ComputerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Localisations]    Script Date: 05.06.2019 13:03:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Localisations](
	[LocalisationID] [int] IDENTITY(1,1) NOT NULL,
	[Street] [nchar](100) NULL,
	[City] [nchar](100) NULL,
	[ZipCode] [nchar](100) NULL,
 CONSTRAINT [PK_Localisation] PRIMARY KEY CLUSTERED 
(
	[LocalisationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OurLocalisations]    Script Date: 05.06.2019 13:03:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OurLocalisations](
	[Street] [nchar](100) NULL,
	[City] [nchar](100) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Computers] ON 

INSERT [dbo].[Computers] ([ComputerID], [ModelName], [Price], [Weight], [HasAdapter]) VALUES (1006, N'ComputerModel1                                                                                      ', 1000, 6, 1)
INSERT [dbo].[Computers] ([ComputerID], [ModelName], [Price], [Weight], [HasAdapter]) VALUES (1007, N'ComputerModel2                                                                                      ', 2000, 9, 0)
INSERT [dbo].[Computers] ([ComputerID], [ModelName], [Price], [Weight], [HasAdapter]) VALUES (1008, N'ComputerModel3                                                                                      ', 3000, 10, 1)
SET IDENTITY_INSERT [dbo].[Computers] OFF
SET IDENTITY_INSERT [dbo].[Localisations] ON 

INSERT [dbo].[Localisations] ([LocalisationID], [Street], [City], [ZipCode]) VALUES (1063, N'Street1                                                                                             ', N'City1                                                                                               ', N'10-752                                                                                              ')
INSERT [dbo].[Localisations] ([LocalisationID], [Street], [City], [ZipCode]) VALUES (1064, N'Street2                                                                                             ', N'City2                                                                                               ', N'98-032                                                                                              ')
INSERT [dbo].[Localisations] ([LocalisationID], [Street], [City], [ZipCode]) VALUES (1065, N'Street3                                                                                             ', N'City3                                                                                               ', N'86-352                                                                                              ')
SET IDENTITY_INSERT [dbo].[Localisations] OFF
USE [master]
GO
ALTER DATABASE [Formatka] SET  READ_WRITE 
GO

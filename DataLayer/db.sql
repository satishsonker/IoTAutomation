USE [master]
GO
/****** Object:  Database [IoT-AppDatabase]    Script Date: 8/30/2021 12:40:35 AM ******/
CREATE DATABASE [IoT-AppDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IoT-AppDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\IoT-AppDatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IoT-AppDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\IoT-AppDatabase_log.ldf' , SIZE = 270336KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [IoT-AppDatabase] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IoT-AppDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IoT-AppDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IoT-AppDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IoT-AppDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [IoT-AppDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IoT-AppDatabase] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [IoT-AppDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [IoT-AppDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [IoT-AppDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IoT-AppDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IoT-AppDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IoT-AppDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IoT-AppDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'IoT-AppDatabase', N'ON'
GO
ALTER DATABASE [IoT-AppDatabase] SET QUERY_STORE = OFF
GO
USE [IoT-AppDatabase]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/30/2021 12:40:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Devices]    Script Date: 8/30/2021 12:40:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Devices](
	[DeviceId] [int] IDENTITY(1,1) NOT NULL,
	[DeviceName] [nvarchar](max) NULL,
	[DeviceKey] [nvarchar](max) NULL,
	[UserKey] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NULL,
	[ConnectionCount] [int] NOT NULL,
	[DeviceDesc] [nvarchar](max) NULL,
	[DeviceTypeId] [int] NOT NULL,
	[LastConnected] [datetime2](7) NULL,
	[RoomId] [int] NOT NULL,
 CONSTRAINT [PK_Devices] PRIMARY KEY CLUSTERED 
(
	[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceType]    Script Date: 8/30/2021 12:40:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceType](
	[DeviceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[DeviceTypeName] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DeviceType] PRIMARY KEY CLUSTERED 
(
	[DeviceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 8/30/2021 12:40:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomKey] [nvarchar](max) NULL,
	[RoomName] [nvarchar](50) NULL,
	[RoomDesc] [nvarchar](max) NULL,
	[UserKey] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SceneActions]    Script Date: 8/30/2021 12:40:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SceneActions](
	[SceneActionId] [int] IDENTITY(1,1) NOT NULL,
	[SceneActionKey] [nvarchar](50) NOT NULL,
	[SceneId] [int] NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[DeviceId] [int] NOT NULL,
	[Value] [nvarchar](50) NULL,
	[UserKey] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_SceneActions] PRIMARY KEY CLUSTERED 
(
	[SceneActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Scenes]    Script Date: 8/30/2021 12:40:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scenes](
	[SceneId] [int] IDENTITY(1,1) NOT NULL,
	[SceneKey] [nvarchar](50) NOT NULL,
	[SceneName] [nvarchar](50) NOT NULL,
	[SceneDesc] [nvarchar](100) NULL,
	[Userkey] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Scenes] PRIMARY KEY CLUSTERED 
(
	[SceneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/30/2021 12:40:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[AuthProvidor] [nvarchar](max) NULL,
	[UserKey] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[Lastname] [nvarchar](max) NULL,
	[APIKey] [nvarchar](max) NULL,
	[Language] [nvarchar](max) NULL,
	[Timezone] [nvarchar](max) NULL,
	[Temperature] [nvarchar](max) NULL,
	[LastLogin] [datetime2](7) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210814062338_InitialCreateNew', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210816104118_new2', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210816115934_AddTableDeviceRoomDeviceType', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210818114807_UpdateDeviceTable', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210819043523_UpdateDeviceTableField', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210819053803_UpdateDeviceTableFieldDeviceType', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210819060835_AddDeviceForeignKey', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210819061118_AddRoomKeyForeignKey', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210819061401_UpdateDeviceRookKeyToRoomId', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210819063220_AddForeignKey', N'5.0.9')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210829173035_AddSceneTable', N'5.0.9')
SET IDENTITY_INSERT [dbo].[Devices] ON 

INSERT [dbo].[Devices] ([DeviceId], [DeviceName], [DeviceKey], [UserKey], [CreatedDate], [ModifiedDate], [ConnectionCount], [DeviceDesc], [DeviceTypeId], [LastConnected], [RoomId]) VALUES (10, N'Hall Light', N'D7C20759CC3347F6B8AA037E8F575567', N'100965730392215373474', CAST(N'2021-08-19T13:06:36.4373224' AS DateTime2), NULL, 0, N'Hall Light', 2, NULL, 25)
INSERT [dbo].[Devices] ([DeviceId], [DeviceName], [DeviceKey], [UserKey], [CreatedDate], [ModifiedDate], [ConnectionCount], [DeviceDesc], [DeviceTypeId], [LastConnected], [RoomId]) VALUES (11, N'Hall Light 1', N'4BBE91702F5F4BDB9C3DE49EDF13BEDC', N'100965730392215373474', CAST(N'2021-08-19T13:06:47.9120233' AS DateTime2), NULL, 0, N'Hall Light 1', 2, NULL, 25)
INSERT [dbo].[Devices] ([DeviceId], [DeviceName], [DeviceKey], [UserKey], [CreatedDate], [ModifiedDate], [ConnectionCount], [DeviceDesc], [DeviceTypeId], [LastConnected], [RoomId]) VALUES (12, N'Fan 1', N'B53C9EEED04E4B01BDF2AF323BCF087C', N'100965730392215373474', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-29T15:11:11.1472559' AS DateTime2), 0, N'Hall Fan 1', 3, NULL, 26)
INSERT [dbo].[Devices] ([DeviceId], [DeviceName], [DeviceKey], [UserKey], [CreatedDate], [ModifiedDate], [ConnectionCount], [DeviceDesc], [DeviceTypeId], [LastConnected], [RoomId]) VALUES (13, N'Light Dimmer', N'8646654403DD47138399C13E7A5BE82B', N'100965730392215373474', CAST(N'2021-08-19T23:51:41.8084171' AS DateTime2), NULL, 0, N'Light Dimmer', 9, NULL, 27)
INSERT [dbo].[Devices] ([DeviceId], [DeviceName], [DeviceKey], [UserKey], [CreatedDate], [ModifiedDate], [ConnectionCount], [DeviceDesc], [DeviceTypeId], [LastConnected], [RoomId]) VALUES (14, N'Light 1', N'F120E65F967C4FEA9AC2D52EF69D87EE', N'100965730392215373474', CAST(N'2021-08-24T15:57:07.4649260' AS DateTime2), NULL, 0, N'Light 1', 5, NULL, 28)
SET IDENTITY_INSERT [dbo].[Devices] OFF
SET IDENTITY_INSERT [dbo].[DeviceType] ON 

INSERT [dbo].[DeviceType] ([DeviceTypeId], [DeviceTypeName], [CreatedDate], [ModifiedDate]) VALUES (2, N'Light', CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DeviceType] ([DeviceTypeId], [DeviceTypeName], [CreatedDate], [ModifiedDate]) VALUES (3, N'Switch', CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DeviceType] ([DeviceTypeId], [DeviceTypeName], [CreatedDate], [ModifiedDate]) VALUES (4, N'Motion Sensor', CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DeviceType] ([DeviceTypeId], [DeviceTypeName], [CreatedDate], [ModifiedDate]) VALUES (5, N'Doorbell', CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DeviceType] ([DeviceTypeId], [DeviceTypeName], [CreatedDate], [ModifiedDate]) VALUES (9, N'Switch with Dimmer', CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[DeviceType] OFF
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([RoomId], [RoomKey], [RoomName], [RoomDesc], [UserKey], [CreatedDate], [ModifiedDate]) VALUES (25, N'4B8106CFE5664C35BF66505B72D41B1A', N'Living Room', N'', N'100965730392215373474', CAST(N'2021-08-18T16:39:01.6771002' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Rooms] ([RoomId], [RoomKey], [RoomName], [RoomDesc], [UserKey], [CreatedDate], [ModifiedDate]) VALUES (26, N'9DBAF0683D1F41639831E5A67762BA4D', N'Guest Room', N'', N'100965730392215373474', CAST(N'2021-08-18T16:39:08.6625466' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Rooms] ([RoomId], [RoomKey], [RoomName], [RoomDesc], [UserKey], [CreatedDate], [ModifiedDate]) VALUES (27, N'3FF6C1AC07D54FC29BADA3477CAA863B', N'Kid Room', N'', N'100965730392215373474', CAST(N'2021-08-18T16:39:13.3485152' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Rooms] ([RoomId], [RoomKey], [RoomName], [RoomDesc], [UserKey], [CreatedDate], [ModifiedDate]) VALUES (28, N'BF04081783E94BACB3F3010C57C34832', N'Bed Room', N'Bed Room1Bed Room1Bed Room1Bed Room1', N'100965730392215373474', CAST(N'2021-08-18T16:39:22.0070197' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Rooms] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [AuthProvidor], [UserKey], [Email], [FirstName], [Lastname], [APIKey], [Language], [Timezone], [Temperature], [LastLogin], [CreatedDate], [ModifiedDate]) VALUES (3, N'google-oauth2', N'100965730392215373474', N'btech.csit@gmail.com', N'Satish', N'Kumar Sonker', N'7C123F6551FB49E0A2DF99402906DFF81EF2632D12424DA88229EA6D431C8CFC', N'en-GB', N'GMT +5.30', N'Celcius', CAST(N'2021-08-30T00:22:25.3207861' AS DateTime2), CAST(N'2021-08-22T14:11:29.3348136' AS DateTime2), CAST(N'2021-08-28T23:42:51.1089489' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Index [IX_Devices_DeviceTypeId]    Script Date: 8/30/2021 12:40:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_Devices_DeviceTypeId] ON [dbo].[Devices]
(
	[DeviceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Devices_RoomId]    Script Date: 8/30/2021 12:40:36 AM ******/
CREATE NONCLUSTERED INDEX [IX_Devices_RoomId] ON [dbo].[Devices]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Devices] ADD  DEFAULT ((0)) FOR [DeviceTypeId]
GO
ALTER TABLE [dbo].[Devices] ADD  DEFAULT ((0)) FOR [RoomId]
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD  CONSTRAINT [FK_Devices_DeviceType_DeviceTypeId] FOREIGN KEY([DeviceTypeId])
REFERENCES [dbo].[DeviceType] ([DeviceTypeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Devices] CHECK CONSTRAINT [FK_Devices_DeviceType_DeviceTypeId]
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD  CONSTRAINT [FK_Devices_Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([RoomId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Devices] CHECK CONSTRAINT [FK_Devices_Rooms_RoomId]
GO
ALTER TABLE [dbo].[SceneActions]  WITH CHECK ADD  CONSTRAINT [FK_SceneActions_Devices] FOREIGN KEY([DeviceId])
REFERENCES [dbo].[Devices] ([DeviceId])
GO
ALTER TABLE [dbo].[SceneActions] CHECK CONSTRAINT [FK_SceneActions_Devices]
GO
ALTER TABLE [dbo].[SceneActions]  WITH CHECK ADD  CONSTRAINT [FK_SceneActions_Scenes] FOREIGN KEY([SceneId])
REFERENCES [dbo].[Scenes] ([SceneId])
GO
ALTER TABLE [dbo].[SceneActions] CHECK CONSTRAINT [FK_SceneActions_Scenes]
GO
USE [master]
GO
ALTER DATABASE [IoT-AppDatabase] SET  READ_WRITE 
GO

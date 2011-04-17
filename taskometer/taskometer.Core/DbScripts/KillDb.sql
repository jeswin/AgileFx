USE [master]
IF EXISTS(SELECT * FROM sys.databases WHERE NAME = 'TaskometerDb')
BEGIN
	ALTER DATABASE [TaskometerDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [TaskometerDb]
END
CREATE DATABASE [TaskometerDb]
GO

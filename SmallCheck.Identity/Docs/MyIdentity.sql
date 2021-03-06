USE [master]
GO
/****** Object:  Database [MyIdentity]    Script Date: 2017/3/8 18:42:16 ******/
CREATE DATABASE [MyIdentity]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyIdentity', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\MyIdentity.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MyIdentity_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\MyIdentity_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MyIdentity] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyIdentity].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyIdentity] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyIdentity] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyIdentity] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyIdentity] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyIdentity] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyIdentity] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MyIdentity] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyIdentity] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyIdentity] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyIdentity] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyIdentity] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyIdentity] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyIdentity] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyIdentity] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyIdentity] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MyIdentity] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyIdentity] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyIdentity] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyIdentity] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyIdentity] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyIdentity] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyIdentity] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyIdentity] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyIdentity] SET  MULTI_USER 
GO
ALTER DATABASE [MyIdentity] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyIdentity] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyIdentity] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyIdentity] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MyIdentity] SET DELAYED_DURABILITY = DISABLED 
GO
USE [MyIdentity]
GO
/****** Object:  Table [dbo].[roles]    Script Date: 2017/3/8 18:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [varchar](36) NOT NULL,
	[name] [varchar](256) NOT NULL,
 CONSTRAINT [PK_ROLES_ID] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[user_claims]    Script Date: 2017/3/8 18:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_claims](
	[id] [varchar](36) NOT NULL,
	[userid] [varchar](36) NOT NULL,
	[claim_type] [nvarchar](max) NULL,
	[claim_value] [nvarchar](max) NULL,
 CONSTRAINT [PK_USER_CLAIMS_ID] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_USER_CLAIMS_USERID_ID] UNIQUE NONCLUSTERED 
(
	[id] ASC,
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[user_logins]    Script Date: 2017/3/8 18:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_logins](
	[login_provider] [varchar](128) NOT NULL,
	[provider_key] [varchar](128) NOT NULL,
	[userid] [varchar](36) NOT NULL,
 CONSTRAINT [PK_USER_LOGINS_lOGIN_PROVIDER] PRIMARY KEY CLUSTERED 
(
	[userid] ASC,
	[login_provider] ASC,
	[provider_key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[user_roles]    Script Date: 2017/3/8 18:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_roles](
	[userid] [varchar](36) NOT NULL,
	[roleid] [varchar](36) NOT NULL,
 CONSTRAINT [PK_USER_ROLES] PRIMARY KEY CLUSTERED 
(
	[userid] ASC,
	[roleid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 2017/3/8 18:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [varchar](36) NOT NULL,
	[username] [varchar](256) NULL,
	[email] [nvarchar](100) NULL,
	[email_confirmed] [bit] NOT NULL,
	[password_hash] [nvarchar](100) NULL,
	[security_stamp] [nvarchar](100) NULL,
	[phone_number] [nvarchar](25) NULL,
	[phone_number_confirmed] [bit] NOT NULL,
	[two_factor_enabled] [bit] NOT NULL,
	[lockout_end_date_utc] [datetime] NULL,
	[lockout_enabled] [bit] NOT NULL,
	[access_failed_count] [int] NOT NULL,
 CONSTRAINT [PK_USERS_ID] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_USER_LOGINS_USERID]    Script Date: 2017/3/8 18:42:16 ******/
CREATE NONCLUSTERED INDEX [IX_USER_LOGINS_USERID] ON [dbo].[user_logins]
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[user_claims]  WITH CHECK ADD  CONSTRAINT [FK_USER_CLAIMS_USERID] FOREIGN KEY([userid])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[user_claims] CHECK CONSTRAINT [FK_USER_CLAIMS_USERID]
GO
ALTER TABLE [dbo].[user_logins]  WITH CHECK ADD  CONSTRAINT [FK_USER_LOGINS_USER] FOREIGN KEY([userid])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[user_logins] CHECK CONSTRAINT [FK_USER_LOGINS_USER]
GO
ALTER TABLE [dbo].[user_roles]  WITH CHECK ADD  CONSTRAINT [FK_USER_ROLES_USER_ID] FOREIGN KEY([userid])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[user_roles] CHECK CONSTRAINT [FK_USER_ROLES_USER_ID]
GO
ALTER TABLE [dbo].[user_roles]  WITH CHECK ADD  CONSTRAINT [PK_USER_ROLES_ROLE_ID] FOREIGN KEY([roleid])
REFERENCES [dbo].[roles] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[user_roles] CHECK CONSTRAINT [PK_USER_ROLES_ROLE_ID]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'roles', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'roles', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'roles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_claims', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_claims', @level2type=N'COLUMN',@level2name=N'userid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'声明类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_claims', @level2type=N'COLUMN',@level2name=N'claim_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'声明值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_claims', @level2type=N'COLUMN',@level2name=N'claim_value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户声明表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_claims'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户登录提供者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_logins', @level2type=N'COLUMN',@level2name=N'login_provider'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户登录供应商key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_logins', @level2type=N'COLUMN',@level2name=N'provider_key'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_logins', @level2type=N'COLUMN',@level2name=N'userid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户登录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_logins'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_roles', @level2type=N'COLUMN',@level2name=N'userid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_roles', @level2type=N'COLUMN',@level2name=N'roleid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_roles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮件是否确认' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'email_confirmed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加密密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'password_hash'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户安全码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'security_stamp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'phone_number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码是否验证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'phone_number_confirmed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用双重身份认证 eg: 密码 + 手机号验证码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'two_factor_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次锁定时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'lockout_end_date_utc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否被锁定' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'lockout_enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录失败次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users', @level2type=N'COLUMN',@level2name=N'access_failed_count'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'users'
GO
USE [master]
GO
ALTER DATABASE [MyIdentity] SET  READ_WRITE 
GO

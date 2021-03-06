USE [ROHM_WOSDB]
GO
/****** Object:  Table [dbo].[M_Users]    Script Date: 2020-11-30 12:52:51 pm ******/
DROP TABLE [dbo].[M_Users]
GO
/****** Object:  Table [dbo].[M_Users]    Script Date: 2020-11-30 12:52:51 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[M_Users](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[ResetPassword] [nvarchar](50) NULL,
	[UserPhoto] [nvarchar](max) NULL,
	[Division] [nvarchar](max) NULL,
	[Department] [nvarchar](max) NULL,
	[Section] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateID] [nvarchar](20) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateID] [nvarchar](20) NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_M_] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[M_Users] ON 

GO
INSERT [dbo].[M_Users] ([ID], [UserName], [FirstName], [LastName], [Email], [Password], [ResetPassword], [UserPhoto], [Division], [Department], [Section], [IsDeleted], [CreateID], [CreateDate], [UpdateID], [UpdateDate]) VALUES (1, N'Admin', N'Admin', N'Admin', N'Admin@Admin.com', N'E10ADC3949BA59ABBE56E057F20F883E', N'', N'', N'LSI', N'Production', N'Production Engineering', 0, N'Admin', CAST(N'2020-11-30 12:48:32.710' AS DateTime), N'Admin', CAST(N'2020-11-30 12:48:32.710' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[M_Users] OFF
GO

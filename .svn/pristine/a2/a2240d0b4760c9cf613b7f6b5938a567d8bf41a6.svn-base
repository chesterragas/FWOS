USE [ROHM_WOSDB]
GO

/****** Object:  Table [dbo].[M_Suppliers]    Script Date: 2020-12-02 1:54:38 pm ******/
DROP TABLE [dbo].[M_Suppliers]
GO

/****** Object:  Table [dbo].[M_Suppliers]    Script Date: 2020-12-02 1:54:38 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[M_Suppliers](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateID] [nvarchar](20) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateID] [nvarchar](20) NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_M_Suppliers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



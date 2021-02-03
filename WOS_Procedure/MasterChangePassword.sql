USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[MasterChangePassword]    Script Date: 1/26/2021 12:59:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[MasterChangePassword] 
	@UserName NVARCHAR(MAX),
	@Password NVARCHAR(MAX)
AS
BEGIN

UPDATE M_Users SET Password = @Password WHERE EmployeeNo = @UserName

END










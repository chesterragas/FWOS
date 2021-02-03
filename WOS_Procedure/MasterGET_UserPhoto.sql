USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[MasterGET_UserList]    Script Date: 2020-12-01 2:31:00 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	User photo update
-- =============================================

ALTER PROCEDURE [dbo].[MasterGET_UserPhoto] 
	--DECLARE
	@UserName NVARCHAR(MAX),
	@Photo NVARCHAR(MAX)
AS
BEGIN

UPDATE M_Users SET UserPhoto = @Photo
WHERE EmployeeNo = @UserName
AND IsDeleted = 0

END










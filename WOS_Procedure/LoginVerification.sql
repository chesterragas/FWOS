USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[M_SP_PageandAccess]    Script Date: 2020-11-30 7:52:47 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chester Ragas
-- Create date: 11-30-2020
-- Description:	Verify UserLogin

 /*MODIFICATION								MODIFIED BY					DATE MODIFIED
 1. 
 */
-- =============================================
CREATE PROCEDURE [dbo].[LoginVerification]
	--DECLARE
	 @UserName NVARCHAR(100) --= 'Ampatuan'
	,@Password NVARCHAR(MAX) --= 'Master'
AS
BEGIN
	SET NOCOUNT ON;
	SET FMTONLY OFF


  SELECT *
  FROM M_Users
  WHERE UserName = @UserName
  AND Password = @Password


END














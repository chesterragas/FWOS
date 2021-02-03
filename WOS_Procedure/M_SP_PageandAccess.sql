USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[M_SP_PageandAccess]    Script Date: 2020-12-04 9:50:34 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chester Ragas
-- Create date: 10-14-2020
-- Description:	GET Pages and Access

 /*MODIFICATION								MODIFIED BY					DATE MODIFIED
 1. 
 */
-- =============================================
ALTER PROCEDURE [dbo].[M_SP_PageandAccess]
	--DECLARE
	 @UserName NVARCHAR(100) = 'Admin'
	,@PageModule NVARCHAR(20) = 'Master'
AS
BEGIN
	SET NOCOUNT ON;
	SET FMTONLY OFF


  SELECT *
  ,ISNULL((SELECT TOP 1 PageAccess FROM PA_Users SPA WHERE SPA.UserName = @UserName AND PP.ID = SPA.PageID),0) AS AccessType
  FROM PA_Pages PP
  WHERE PP.PageModule = @PageModule


END














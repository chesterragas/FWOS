USE [ROHM_WOSDB]
GO

/****** Object:  StoredProcedure [dbo].[MasterGET_WorkRequestList]    Script Date: 1/22/2021 1:41:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[MasterGET_CriteriaDetailList] 
	--DECLARE
	@PageCount INT = 0,
	@RowCount INT = 10,
	@OrderBy NVARCHAR(max) = '',
	@WorkCategory NVARCHAR(20),-- = 'Supplier',
	@CriteriaHeader BIGINT,-- = 'In-House',
	@Filter NVARCHAR(max) = '',
	@RecordCount INT --OUT
AS
BEGIN




SELECT  ROW_NUMBER() OVER(ORDER BY (select 0) DESC) AS Rownum,
		*,
		(SELECT TOP 1 C.CriteriaName FROM M_CriteriaHeader C WHERE C.ID = CriteriaHeaderID) AS CriteriaName
		

FROM M_CriteriaDetails
WHERE (
DetailName LIKE '%'+@Filter+'%'
OR DetailPoints LIKE '%'+@Filter+'%'
)
AND (CriteriaHeaderID IN (SELECT C.ID FROM M_CriteriaHeader C WHERE C.WorkCategory = @WorkCategory OR @WorkCategory = '' OR @WorkCategory IS NULL ))
AND IsDeleted <> 1
ORDER BY ID
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM M_CriteriaDetails WHERE (
DetailName LIKE '%'+@Filter+'%'
OR DetailPoints LIKE '%'+@Filter+'%'
)
AND (CriteriaHeaderID IN (SELECT C.ID FROM M_CriteriaHeader C WHERE C.WorkCategory = @WorkCategory OR @WorkCategory = '' OR @WorkCategory IS NULL ))
AND IsDeleted <> 1)

END











GO



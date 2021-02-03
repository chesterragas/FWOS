USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[MasterGET_TimeFrameList]    Script Date: 1/24/2021 2:45:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[MasterGET_TimeFrameList] 
	--DECLARE
	@PageCount INT = 0,
	@RowCount INT = 10,
	@OrderBy NVARCHAR(max) = '',
	@WorkCategory NVARCHAR(max) = 'Supplier',
	@Filter NVARCHAR(max) = '',
	@RecordCount INT OUT
AS
BEGIN


--SELECT ROW_NUMBER() OVER(ORDER BY (DateLog) DESC) AS Rownum,*, ISNULL((SELECT TOP 1 FirstName + ' ' + LastName FROM M_Users WHERE UserName = EL.Username), 'Not Registered') AS EmployeeName

SELECT  ROW_NUMBER() OVER(ORDER BY (select 0) DESC) AS Rownum,
		*
FROM M_TimeFrame
WHERE (
WorkCategory LIKE '%'+@Filter+'%'
OR WorkingDays LIKE '%'+@Filter+'%'
OR ScoreFrom LIKE '%'+@Filter+'%'
OR ScoreTo LIKE '%'+@Filter+'%'
)
AND WorkCategory = @WorkCategory
AND IsDeleted <> 1
ORDER BY ID
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM M_TimeFrame WHERE IsDeleted <> 1 AND (WorkCategory = @WorkCategory OR @WorkCategory IS NULL OR @WorkCategory = ''))

END













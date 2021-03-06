USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[MasterGET_UserList]    Script Date: 1/21/2021 9:26:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

CREATE PROCEDURE [dbo].[MasterGET_WorkClassificationList] 
	--DECLARE
	@PageCount INT = 0,
	@RowCount INT = 10,
	@OrderBy NVARCHAR(max) = '',
	@Filter NVARCHAR(max) = '',
	@RecordCount INT OUT
AS
BEGIN


--SELECT ROW_NUMBER() OVER(ORDER BY (DateLog) DESC) AS Rownum,*, ISNULL((SELECT TOP 1 FirstName + ' ' + LastName FROM M_Users WHERE UserName = EL.Username), 'Not Registered') AS EmployeeName

SELECT  ROW_NUMBER() OVER(ORDER BY (select 0) DESC) AS Rownum,
		ID,
		WorkClassificationName
		

FROM M_WorkClassification
WHERE (
WorkClassificationName LIKE '%'+@Filter+'%'
OR WorkClassificationName LIKE '%'+@Filter+'%'
OR WorkClassificationName LIKE '%'+@Filter+'%'
)
AND IsDeleted <> 1
ORDER BY ID
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM M_WorkClassification WHERE IsDeleted <> 1)

END










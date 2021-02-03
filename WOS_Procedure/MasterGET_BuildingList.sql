USE [ROHM_WOSDB]
GO

/****** Object:  StoredProcedure [dbo].[MasterGET_WorkRequestList]    Script Date: 1/21/2021 11:37:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[MasterGET_BuildingList] 
	--DECLARE
	@PageCount INT = 0,
	@RowCount INT = 10,
	@OrderBy NVARCHAR(max) = '',
	@Filter NVARCHAR(max) = '',
	@DivisionID BIGINT,
	@RecordCount INT OUT
AS
BEGIN


--SELECT ROW_NUMBER() OVER(ORDER BY (DateLog) DESC) AS Rownum,*, ISNULL((SELECT TOP 1 FirstName + ' ' + LastName FROM M_Users WHERE UserName = EL.Username), 'Not Registered') AS EmployeeName

SELECT  ROW_NUMBER() OVER(ORDER BY (select 0) DESC) AS Rownum,
		ID,
		DivisionID,
		BuildingName
		

FROM M_Building
WHERE (
BuildingName LIKE '%'+@Filter+'%'
OR BuildingName LIKE '%'+@Filter+'%'
OR BuildingName LIKE '%'+@Filter+'%'
)
AND DivisionID = @DivisionID
AND IsDeleted <> 1
ORDER BY ID
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM M_Building WHERE IsDeleted <> 1 AND DivisionID = @DivisionID)

END











GO



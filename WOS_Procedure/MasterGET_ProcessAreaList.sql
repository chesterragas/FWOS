USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[MasterGET_ProcessAreaList]    Script Date: 1/22/2021 11:02:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[MasterGET_ProcessAreaList] 
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
		(SELECT TOP 1 d.DivisionName FROM M_Division d WHERE d.ID = DivisionID) AS DivisionID,
		ProcessAreaName
FROM M_ProcessArea
WHERE (
ProcessAreaName LIKE '%'+@Filter+'%'
OR ProcessAreaName LIKE '%'+@Filter+'%'
OR ProcessAreaName LIKE '%'+@Filter+'%'
)
AND DivisionID = @DivisionID
AND IsDeleted <> 1
ORDER BY ID
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM M_ProcessArea WHERE IsDeleted <> 1 AND DivisionID = @DivisionID)

END













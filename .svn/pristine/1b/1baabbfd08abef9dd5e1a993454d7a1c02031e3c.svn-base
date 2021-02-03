USE [ROHM_WOSDB]
GO

/****** Object:  StoredProcedure [dbo].[MasterGET_UtilitiesList]    Script Date: 1/27/2021 1:13:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[MasterGET_UtilitiesDetailsList] 
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
		*,
		(SELECT TOP 1 C.UtilitiesName FROM M_Utilities C WHERE C.ID = UtilitiesID) AS UtilitiesName,
		(SELECT TOP 1 C.CriteriaName FROM M_UtilitiesCriteria C WHERE C.ID = CriteriaID) AS CriteriaName
FROM M_UtilitiesDetails
WHERE (
DetailsName LIKE '%'+@Filter+'%'
OR Unit LIKE '%'+@Filter+'%'
)
AND IsDeleted <> 1
ORDER BY ID
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM M_UtilitiesDetails WHERE IsDeleted <> 1)

END











GO



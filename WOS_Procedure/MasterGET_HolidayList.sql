USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[MasterGET_BuildingList]    Script Date: 1/29/2021 4:46:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[MasterGET_HolidayList] 
	--DECLARE
	@PageCount INT = 0,
	@RowCount INT = 10,
	@OrderBy NVARCHAR(max) = '',
	@Filter NVARCHAR(max) = '',
	@TypeHoliday NVARCHAR(max) = '',
	@RecordCount INT OUT
AS
BEGIN


--SELECT ROW_NUMBER() OVER(ORDER BY (DateLog) DESC) AS Rownum,*, ISNULL((SELECT TOP 1 FirstName + ' ' + LastName FROM M_Users WHERE UserName = EL.Username), 'Not Registered') AS EmployeeName

SELECT  ROW_NUMBER() OVER(ORDER BY (select 0) DESC) AS Rownum,
		*
FROM M_Holidays
WHERE (
HolidayName LIKE '%'+@Filter+'%'
OR HolidayDate LIKE '%'+@Filter+'%'
)
AND (Type = @TypeHoliday OR @TypeHoliday = '' OR @TypeHoliday IS NULL)
AND IsDeleted <> 1
ORDER BY ID
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM M_Holidays
WHERE (
HolidayName LIKE '%'+@Filter+'%'
OR HolidayDate LIKE '%'+@Filter+'%'
)
AND IsDeleted <> 1)

END












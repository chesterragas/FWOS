USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[MasterGET_UserList]    Script Date: 1/22/2021 11:01:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[MasterGET_UserList] 
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
		EmployeeNo,
		FirstName,
		LastName,
		Email,
		Password,
		UserPhoto,
		MobileNo,
		LocalNo,
		(SELECT TOP 1 md.DivisionName FROM M_Division md WHERE md.ID = ID) AS DivisionID,
		(SELECT TOP 1 md.DepartmentName FROM M_Department md WHERE md.ID = ID) AS DepartmentID,
		(SELECT TOP 1 md.SectionName FROM M_Section md WHERE md.ID = ID) AS SectionID,
		IsApproved

FROM M_Users
WHERE (
EmployeeNo LIKE '%'+@Filter+'%'
OR FirstName LIKE '%'+@Filter+'%'
OR LastName LIKE '%'+@Filter+'%'
)
AND IsDeleted <> 1
ORDER BY ID
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM M_Users WHERE IsDeleted <> 1)

END










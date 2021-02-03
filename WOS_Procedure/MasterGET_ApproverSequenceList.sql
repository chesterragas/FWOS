USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[MasterGET_ApproverSequenceList]    Script Date: 1/28/2021 5:07:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[MasterGET_ApproverSequenceList] 
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
		(SELECT TOP 1 D.DivisionName FROM M_Division D WHERE D.ID = a.DivisionID) AS DivisionName,
		(SELECT TOP 1 D.DepartmentName FROM M_Department D WHERE D.ID = a.DepartmentID) AS DepartmentName,
		(SELECT TOP 1 D.SectionName FROM M_Section D WHERE D.ID = a.SectionID) AS SectionName,
		EmployeeNo AS EmpNo
FROM M_ApproverSequence a
WHERE (
DivisionID LIKE '%'+@Filter+'%'
OR DepartmentID LIKE '%'+@Filter+'%'
OR SectionID LIKE '%'+@Filter+'%'
OR EmployeeNo LIKE '%'+@Filter+'%'
)
AND IsDeleted <> 1
ORDER BY ID
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM M_ApproverSequence
WHERE (
DivisionID LIKE '%'+@Filter+'%'
OR DepartmentID LIKE '%'+@Filter+'%'
OR SectionID LIKE '%'+@Filter+'%'
OR EmployeeNo LIKE '%'+@Filter+'%'
))

END












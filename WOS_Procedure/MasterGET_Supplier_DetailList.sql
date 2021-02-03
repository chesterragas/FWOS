USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[MasterGET_Supplier_DetailList]    Script Date: 1/22/2021 11:01:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[MasterGET_Supplier_DetailList] 
	--DECLARE
	@PageCount INT = 0,
	@RowCount INT = 10,
	@OrderBy NVARCHAR(max) = '',
	@Filter NVARCHAR(max) = '',
	@SupplierID BIGINT,
	@RecordCount INT OUT
AS
BEGIN


--SELECT ROW_NUMBER() OVER(ORDER BY (DateLog) DESC) AS Rownum,*, ISNULL((SELECT TOP 1 FirstName + ' ' + LastName FROM M_Users WHERE UserName = EL.Username), 'Not Registered') AS EmployeeName

SELECT  ROW_NUMBER() OVER(ORDER BY (select 0) DESC) AS Rownum,
		*
FROM M_SuppliersDetails
WHERE (
ContactFirstName LIKE '%'+@Filter+'%'
OR @Filter = ''
OR @Filter IS NULL
)
AND SupplierID = @SupplierID
AND IsDeleted <> 1
ORDER BY ID
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM M_SuppliersDetails WHERE IsDeleted <> 1)

END











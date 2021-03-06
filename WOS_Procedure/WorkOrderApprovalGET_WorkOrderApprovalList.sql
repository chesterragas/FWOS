USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[WorkOrderApprovalGET_WorkOrderApprovalList]    Script Date: 1/29/2021 2:47:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[WorkOrderApprovalGET_WorkOrderApprovalList] 
	--DECLARE
	@PageCount INT = 0,
	@RowCount INT = 10,
	@OrderBy NVARCHAR(max) = '',
	@Filter NVARCHAR(max) = '',
	@EmployeeNo NVARCHAR(50) = 'TRSection01',
	@RecordCount INT OUT
AS
BEGIN


--SELECT ROW_NUMBER() OVER(ORDER BY (DateLog) DESC) AS Rownum,*, ISNULL((SELECT TOP 1 FirstName + ' ' + LastName FROM M_Users WHERE UserName = EL.Username), 'Not Registered') AS EmployeeName

select 
	wori.ID
,	mdiv.DivisionName
,	mdept.DepartmentName
,	msec.SectionName
,	wori.WorkOrderTitle
,	wori.ReferenceNo
,	wori.WorkOrderNo
,	wori.RequestorEmployeeNo
,	wowd.*
,	CASE WHEN mbuild.BuildingName <> 'Other' then mbuild.BuildingName else wowd.BuildingOther end as BuildingName
,	CASE WHEN mfloor.FloorName <> 'Other' then mfloor.FloorName else wowd.FloorOther end as FloorName
,	CASE WHEN mprocess.ProcessAreaName <> 'Other' then mprocess.ProcessAreaName else wowd.ProcessAreaOther end as ProcessName
,	DATEDIFF(DAY,GETDATE(),wori.DateRequest) as RequestAge
,	musers.LastName + ', ' + musers.FirstName as Requestor
,	wori.Rejected

from WO_RequestorInformation wori
left join M_Division mdiv on wori.DivisionID=mdiv.ID
left join M_Department mdept on wori.DepartmentID=mdept.ID
left join M_Section msec on wori.SectionID=msec.ID
left join WO_WorkDescriptionDetails wowd on wori.ID=wowd.RequestorInformationID
left join M_Building mbuild on wowd.BuildingID=mbuild.ID
left join M_Floor mfloor on wowd.FloorID=mfloor.ID
left join M_ProcessArea mprocess on wowd.ProcessAreaID=mprocess.ID
left join M_Users musers on wori.RequestorEmployeeNo = musers.EmployeeNo and musers.IsDeleted <> 1
LEFT JOIN WO_ApprovalStatus WO
ON WO.RequestorInformationID = wori.ID
--WHERE (
--EmployeeNo LIKE '%'+@Filter+'%'
--OR FirstName LIKE '%'+@Filter+'%'
--OR LastName LIKE '%'+@Filter+'%'
--)
--AND IsDeleted <> 1
WHERE wori.IsDeleted <> 1
AND wori.Rejected <> 1
AND wori.Approved <> 1
AND wori.IsSubmitted = 1
AND wori.ForApproval < WO.ApproverLevel
AND WO.EmployeeNo = @EmployeeNo
ORDER BY wori.UpdateID desc
OFFSET @PageCount * (@RowCount) ROWS
FETCH NEXT @RowCount ROWS ONLY	


SET @RecordCount = (SELECT COUNT(*) FROM WO_RequestorInformation WHERE IsDeleted <> 1 AND Rejected <> 1 AND Approved <> 1 AND IsSubmitted = 1)

END










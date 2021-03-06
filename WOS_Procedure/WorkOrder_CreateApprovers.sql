USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[WorkOrder_CreateApprovers]    Script Date: 1/28/2021 4:56:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Chester Ragas
-- Create date: 01-28-2021
-- Description:	Create Approvers

 /*MODIFICATION								MODIFIED BY					DATE MODIFIED
 1. 
 */
-- =============================================
ALTER PROCEDURE [dbo].[WorkOrder_CreateApprovers]
	--DECLARE
	 @RequestorInformationID BIGINT = 1,
	 @EmployeeID NVARCHAR(50) = 'TRSection01'
AS
BEGIN
	SET NOCOUNT ON;
	SET FMTONLY OFF


  INSERT INTO [dbo].[WO_ApprovalStatus]
           ([RequestorInformationID]
           ,[EmployeeNo]
           ,[ApproverLevel]
           ,[MainBackupApprover]
           ,[Position]
           ,[ApprovedDisapproved]
           ,[ApprovedDisapporvedID]
           ,[ApprovedDisapprovedDate]
           ,[CreateID]
           ,[CreateDate]
           ,[UpdateID]
           ,[UpdateDate])
	SELECT  @RequestorInformationID,
			EmployeeNo,
			OrderNo,
			MainBackupApprover,
			(SELECT TOP 1 Position FROM M_Users WHERE EmployeeNo = @EmployeeID),
			NULL,
			NULL,
			NULL,
			@EmployeeID,
			GETDATE(),
			@EmployeeID,
			GETDATE()
	FROM M_ApproverSequence
	

END















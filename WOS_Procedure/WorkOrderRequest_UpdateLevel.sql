USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[WorkOrderRequest_UpdateLevel]    Script Date: 1/29/2021 2:17:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[WorkOrderRequest_UpdateLevel] 
	--DECLARE
	@ID BIGINT,-- = 1
	@EmployeeNo NVARCHAR(50),-- = '14109'
	@ApprovedReject INT,-- = '1'
	@Remarks NVARCHAR(MAX)
AS
BEGIN


DECLARE @CurrentLevel INT;
DECLARE @EmployeeApprovedLevel INT;
DECLARE @HighApprovalLevel INT;

SET @CurrentLevel = (SELECT TOP 1 ForApproval FROM WO_RequestorInformation WHERE ID = @ID)
SET @EmployeeApprovedLevel = (SELECT TOP 1 ApproverLevel FROM WO_ApprovalStatus WHERE EmployeeNo = @EmployeeNo)
SET @HighApprovalLevel = (SELECT MAX(ApproverLevel) FROM WO_ApprovalStatus WHERE RequestorInformationID = @ID)
IF @ApprovedReject = 2
BEGIN
	UPDATE WO_RequestorInformation 
	SET ForApproval = 0,
		Rejected = 1,
		IsSubmitted = 0
	WHERE ID = @ID

	Update WO_ApprovalStatus
	SET ApprovedDisapporvedID = NULL,
		ApprovedDisapproved = NULL,
		ApprovedDisapprovedDate = NULL
	WHERE RequestorInformationID = @ID
END

ELSE
BEGIN
	UPDATE WO_RequestorInformation 
	SET ForApproval = @EmployeeApprovedLevel
	WHERE ID = @ID

	IF @EmployeeApprovedLevel = @HighApprovalLevel
	BEGIN
		UPDATE WO_RequestorInformation 
		SET Approved = 1
		WHERE ID = @ID
	END
END

UPDATE WO_ApprovalStatus
SET ApprovedDisapproved = @ApprovedReject,
	ApprovedDisapporvedID = @EmployeeNo,
	ApprovedDisapprovedDate = GETDATE()
WHERE EmployeeNo = @EmployeeNo

IF @Remarks <> ''
BEGIN

DECLARE @RevisionNo INT;

SET @RevisionNo = (SELECT TOP 1 RevisionNo FROM WO_RequestorInformation WHERE ID = @ID)

INSERT INTO [dbo].[WO_ApprovalLogs]
           ([RequestorInformationID]
           ,[EmployeeNo]
           ,[Remarks]
		   ,[ResivionNo]
		   ,[ApprovedReject]
           ,[ColorFlag]
           ,[CreateID]
           ,[CreateDate]
           ,[UpdateID]
           ,[UpdateDate])
     VALUES
           (@ID
           ,@EmployeeNo
           ,@Remarks
		   ,@RevisionNo
		   ,@ApprovedReject
           ,1
           ,@EmployeeNo
           ,GETDATE()
           ,@EmployeeNo
           ,GETDATE())

END

END
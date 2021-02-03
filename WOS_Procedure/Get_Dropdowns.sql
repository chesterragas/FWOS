USE [ROHM_WOSDB]
GO
/****** Object:  StoredProcedure [dbo].[Get_Dropdowns]    Script Date: 1/28/2021 4:48:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Chester R.
-- Create date: 06-23-2020
-- Description:	GET Error Logs with name
-- =============================================

ALTER PROCEDURE [dbo].[Get_Dropdowns] 
	--DECLARE
	@ParentID BIGINT = '',
	@Table NVARCHAR(max) = 'M_Utilities'
AS
BEGIN

IF @Table = 'M_Division'
	BEGIN
		SELECT ID, DivisionName AS ItemName
		FROM M_Division
		WHERE IsDeleted <> 1
	END

IF @Table = 'M_Department'
	BEGIN
		SELECT ID, DepartmentName AS ItemName
		FROM M_Department
		WHERE (DivisionID = @ParentID OR @ParentID = '' OR @ParentID IS NULL)
		AND IsDeleted <> 1
	END

IF @Table = 'M_Section'
	BEGIN
		SELECT ID, SectionName AS ItemName
		FROM M_Section
		WHERE DepartmentID = @ParentID OR @ParentID = '' OR @ParentID IS NULL
		AND IsDeleted <> 1
	END

IF @Table = 'M_Building'
BEGIN
	SELECT ID, BuildingName AS ItemName
	FROM M_Building
	WHERE IsDeleted <> 1
END

IF @Table = 'M_Floor'
BEGIN
	SELECT ID, FloorName AS ItemName
	FROM M_Floor
	WHERE IsDeleted <> 1 AND BuildingID=@ParentID
END
	
IF @Table = 'M_ProcessArea'
BEGIN
	SELECT ID, ProcessAreaName AS ItemName
	FROM M_ProcessArea
	WHERE IsDeleted <> 1 AND DivisionID=@ParentID
END
	
IF @Table = 'M_WorkRequest'
BEGIN
	SELECT ID, WorkRequestName AS ItemName
	FROM M_WorkRequest
	WHERE IsDeleted <> 1
END
	
IF @Table = 'M_WorkClassification'
BEGIN
	SELECT ID, WorkClassificationName AS ItemName
	FROM M_WorkClassification
	WHERE IsDeleted <> 1
END
	
IF @Table = 'M_Users'
BEGIN
	SELECT EmployeeNo, CONCAT(LastName,', ',FirstName) AS ItemName
	FROM M_Users
	WHERE IsDeleted <> 1
END

IF @Table = 'M_CriteriaHeader'
BEGIN
	SELECT ID, CriteriaName AS ItemName
	FROM M_CriteriaHeader
	WHERE IsDeleted <> 1
END

IF @Table = 'M_Utilities'
BEGIN
	SELECT ID, UtilitiesName AS ItemName
	FROM M_Utilities
	WHERE IsDeleted <> 1
END

IF @Table = 'M_UtilitiesCriteria'
BEGIN
	SELECT ID, CriteriaName AS ItemName
	FROM M_UtilitiesCriteria
	WHERE IsDeleted <> 1
	AND UtilitiesID = @ParentID OR @ParentID = '' OR @ParentID IS NULL
END


END



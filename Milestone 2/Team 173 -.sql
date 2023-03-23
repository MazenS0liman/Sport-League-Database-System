CREATE DATABASE League

-- 2.1 Basic Structure of the Database
GO
CREATE PROCEDURE createAllTables 
	AS
	CREATE TABLE SystemUser (
		username VARCHAR(20),
		Password VARCHAR(20),
		-- Constraints
		CONSTRAINT  PK_SystemUser PRIMARY KEY (username) 
	)
	CREATE TABLE SystemAdmin (
		username VARCHAR(20) UNIQUE NOT NULL,
		ID INTEGER PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(20) NOT NULL,
		-- Constraints
		CONSTRAINT FK_SystemAdminUsername FOREIGN KEY (username) REFERENCES SystemUser(username) ON UPDATE CASCADE  ON DELETE CASCADE 
	)
	CREATE TABLE SportsAssociationManager(
		username VARCHAR(20) UNIQUE NOT NULL,
		ID INTEGER PRIMARY KEY IDENTITY(1,1),
		name VARCHAR(20) NOT NULL,
		-- Constraints
		CONSTRAINT FK_SportsAssociationManagerUsername FOREIGN KEY (username) REFERENCES SystemUser(username) ON UPDATE CASCADE  ON DELETE CASCADE 
	)
	CREATE TABLE Fan(
		username VARCHAR(20) UNIQUE NOT NULL,
		NationalID VARCHAR(20),
		PhoneNo INTEGER,
		Birth_date DATETIME,
		Name VARCHAR(20) NOT NULL,
		Address VARCHAR(20),
		Status BIT DEFAULT 1, -- 1: Unblocked, 0: Blocked
		-- Constraints
		CONSTRAINT PK_Fan PRIMARY KEY (NationalID),
		CONSTRAINT FK_FanUsername FOREIGN KEY (username) REFERENCES SystemUser(username) ON UPDATE CASCADE  ON DELETE CASCADE 
	)
	CREATE TABLE Club (
		ID INTEGER PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(20) NOT NULL UNIQUE,
		Location VARCHAR(20)
	) 
	CREATE TABLE ClubRepresentative (
		username VARCHAR(20) UNIQUE NOT NULL,
		ID INTEGER PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(20) NOT NULL,
		RepresntativeClubID INTEGER UNIQUE NOT NULL,
		-- Constraints
		CONSTRAINT FK_ClubRepresentativeUsername FOREIGN KEY (username) REFERENCES SystemUser(username) ON UPDATE CASCADE  ON DELETE CASCADE ,
		CONSTRAINT FK_RepresentativeClubeID FOREIGN KEY (RepresntativeClubID) REFERENCES Club(ID) ON UPDATE CASCADE  ON DELETE CASCADE 
	)
	CREATE TABLE Stadium (
		ID INTEGER PRIMARY KEY IDENTITY(1,1),
		Status BIT DEFAULT 1, -- 1: Available, 0: Unavailble
		Name VARCHAR(20) NOT NULL UNIQUE,
		Capacity INTEGER,
		Location VARCHAR(20),
	)
	CREATE TABLE StadiumManager (
		username VARCHAR(20) UNIQUE NOT NULL,
		ID INTEGER PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(20) NOT NULL,
		StadiumID INTEGER UNIQUE NOT NULL,
		-- Constraints
		CONSTRAINT StadiumManagerUsername FOREIGN KEY (username) REFERENCES SystemUser(username) ON UPDATE CASCADE  ON DELETE CASCADE ,
		CONSTRAINT FK_ManagerStadiumID FOREIGN KEY (StadiumID) REFERENCES Stadium(ID) ON UPDATE CASCADE  ON DELETE CASCADE 
	)
	CREATE TABLE Match (
		ID INTEGER PRIMARY KEY IDENTITY(1,1),
		Starttime DATETIME,
		Endtime DATETIME,
		HostID INTEGER,
		GuestID INTEGER,
		StadiumID INTEGER,
		-- Constraints
		CONSTRAINT FK_MatchHostID FOREIGN KEY (HostID) REFERENCES Club(ID),
		CONSTRAINT FK_MatchGuestID FOREIGN KEY (GuestID) REFERENCES Club(ID), 
		CONSTRAINT FK_MatchStadiumID FOREIGN KEY (StadiumID) REFERENCES Stadium(ID) ON UPDATE SET NULL ON DELETE SET NULL
	)
	CREATE TABLE Hostrequest (
		ID INTEGER PRIMARY KEY IDENTITY(1,1),
		Status VARCHAR(20),
		Match_ID INTEGER,
		RepresentativeID INTEGER,
		StadiumManagerID INTEGER
		-- Constraints
		CONSTRAINT FK_RepresentativeID FOREIGN KEY (RepresentativeID) REFERENCES ClubRepresentative(ID) ON UPDATE CASCADE ON DELETE CASCADE,
		CONSTRAINT FK_StadiumManagerID FOREIGN KEY (StadiumManagerID) REFERENCES StadiumManager(ID),
		CONSTRAINT FK_Match_ID FOREIGN KEY (Match_ID) REFERENCES Match(ID) ON UPDATE CASCADE ON DELETE CASCADE
	)
	CREATE TABLE Ticket (
		ID INTEGER PRIMARY KEY IDENTITY(1,1),
		Status BIT DEFAULT 1, -- 1: Available, 0: Unavailable
		FanNationalID VARCHAR(20),
		MatchID INTEGER,
		-- Constraints
		CONSTRAINT FK_TicketMatchID FOREIGN KEY (MatchID) REFERENCES Match(ID) ON UPDATE CASCADE  ON DELETE CASCADE,
	)
GO

GO
CREATE PROCEDURE dropAllTables
	AS
	DROP TABLE Ticket
	DROP TABLE Hostrequest
	DROP TABLE Match
	DROP TABLE StadiumManager
	DROP TABLE ClubRepresentative
	DROP TABLE Fan
	DROP TABLE SportsAssociationManager
	DROP TABLE SystemAdmin
	DROP TABLE SystemUser
	DROP TABLE Stadium
	DROP TABLE Club
GO

GO
CREATE PROCEDURE  dropAllProceduresFunctionsViews 
	AS
	DROP PROCEDURE createAllTables;
	DROP PROCEDURE dropAllTables;
	DROP PROCEDURE clearAllTables;
	DROP PROCEDURE addAssociationManager;
	DROP PROCEDURE addNewMatch;
	DROP PROCEDURE deleteMatch;
	DROP PROCEDURE deleteMatchesOnStadium;
	DROP PROCEDURE addClub;
	DROP PROCEDURE addTicket;
	DROP PROCEDURE deleteClub;
	DROP PROCEDURE addStadium;
	DROP PROCEDURE deleteStadium;
	DROP PROCEDURE blockFan;
	DROP PROCEDURE unblockFan;
	DROP PROCEDURE addRepresentative;
	DROP PROCEDURE addHostRequest;
	DROP PROCEDURE addStadiumManager;
	DROP PROCEDURE acceptRequest;
	DROP PROCEDURE rejectRequest;
	DROP PROCEDURE addFan;
	DROP PROCEDURE purchaseTicket;
	DROP PROCEDURE updateMatchHost;

	DROP FUNCTION viewAvailableStadiumsOn;
	DROP FUNCTION allUnassignedMatches;
	DROP FUNCTION allPendingRequests;
	DROP FUNCTION upcomingMatchesOfClub;
	DROP FUNCTION availableMatchesToAttend;
	DROP FUNCTION clubsNeverPlayed;
	DROP FUNCTION matchWithHighestAttendance;
	DROP FUNCTION matchesRankedByAttendance;
	DROP FUNCTION requestsFromClub;

	DROP VIEW allAssocManagers;
	DROP VIEW allClubRepresentatives;
	DROP VIEW allStadiumManagers;
	DROP VIEW allFans;
	DROP VIEW allMatches;
	DROP VIEW allTickets;
	DROP VIEW allCLubs;
	DROP VIEW allStadiums;
	DROP VIEW allRequests;
	DROP VIEW clubsWithNoMatches;
	DROP VIEW matchesPerTeam;
	DROP VIEW clubsNeverMatched;
GO

GO
CREATE PROCEDURE clearAllTables
	AS
	DELETE FROM Ticket
	DELETE FROM Hostrequest
	DELETE FROM Match	
	DELETE FROM StadiumManager
	DELETE FROM ClubRepresentative
	DELETE FROM Fan
	DELETE FROM SportsAssociationManager
	DELETE FROM SystemAdmin
	DELETE FROM SystemUser
	DELETE FROM Stadium
	DELETE FROM Club
GO

-------------------------------------------------------------------------------------------------------
-- 2.2 Basic Data Retrieval
-- a
GO
CREATE VIEW allAssocManagers 
	AS
	SELECT SPA.username, S.Password, SPA.name
	FROM SportsAssociationManager SPA INNER JOIN SystemUser S ON SPA.username = S.username
GO

-- b
GO
CREATE VIEW allClubRepresentatives
	AS
	SELECT R.username, S.Password, R.Name, C.Name AS 'Club Name'
	FROM ClubRepresentative R INNER JOIN Club C ON  R.RepresntativeClubID = C.ID
							  INNER JOIN SystemUser S ON R.username = S.username 
GO

-- c
GO
CREATE VIEW allStadiumManagers
	AS
	SELECT SM.username, SU.Password, SM.Name, S.Name AS 'Stadium Name'
	FROM StadiumManager SM INNER JOIN Stadium S ON SM.StadiumID = S.ID
						   INNER JOIN SystemUser SU ON SM.username = SU.username
GO

-- d
GO
CREATE VIEW allFans
	AS
	SELECT S.username, S.Password, F.Name, F.NationalID, F.Birth_date, F.Status
	FROM Fan F INNER JOIN SystemUser S ON  F.username = S.username
GO

-- e
GO
CREATE VIEW allMatches
	AS
	SELECT  H.Name AS 'Host Club Name', G.Name AS 'Guest Club Name', M.Starttime 
	FROM Match M INNER JOIN Club H ON M.HostID = H.ID
				INNER JOIN Club G ON M.GuestID = G.ID
GO

GO
CREATE VIEW allMatches2
	AS
	SELECT  H.Name AS 'Host Club Name', G.Name AS 'Guest Club Name', M.Starttime, M.Endtime
	FROM Match M INNER JOIN Club H ON M.HostID = H.ID
				INNER JOIN Club G ON M.GuestID = G.ID
GO

-- f
GO
CREATE VIEW allTickets
	AS
	SELECT H.Name AS 'Host Club Name', G.Name AS 'Guest Club Name', S.Name 'Stadium Name', M.Starttime
	FROM Ticket T INNER JOIN Match M ON T.MatchID = M.ID
				  INNER JOIN Club H ON M.HostID = H.ID
				  INNER JOIN Club G ON M.GuestID = G.ID
				  INNER JOIN Stadium S ON M.StadiumID = S.ID
GO

-- g
GO
CREATE VIEW allCLubs
	AS
	SELECT C.Name, C.Location
	FROM Club C
GO

-- h
GO
CREATE VIEW allStadiums
	AS 
	SELECT S.Name, S.Location, S.Capacity, S.Status
	FROM Stadium S
GO

SELECT * FROM allStadiums

-- i
GO
CREATE VIEW allRequests
	AS 
	SELECT R.username AS 'Representative username', SM.username AS 'Stadium Manager username', HR.Status 
	FROM Hostrequest HR INNER JOIN ClubRepresentative R ON HR.RepresentativeID = R.ID
						INNER JOIN StadiumManager SM ON HR.StadiumManagerID = SM.ID
GO
-------------------------------------------------------------------------------------------------------
-- 2.3 All Other Requirements
-- i
GO
CREATE PROCEDURE addAssociationManager
	@name VARCHAR(20),
	@username VARCHAR(20),
	@password VARCHAR(20)
	AS
	IF NOT EXISTS (
								SELECT SU.username
								FROM SystemUser SU
								WHERE SU.username = @username
						   )
	BEGIN
		INSERT INTO SystemUser VALUES (@username, @password)
		INSERT INTO SportsAssociationManager (username,name) VALUES (@username,@name)
	END
GO

-- ii
GO
CREATE PROCEDURE addNewMatch
	@hostname VARCHAR(20),
	@guestname VARCHAR(20),
	@starttime DATETIME,
	@endtime DATETIME
	AS
		DECLARE @hostID INTEGER
		DECLARE @guestID INTEGER

		SELECT  @hostID = C.ID
		FROM Club C
		WHERE C.Name = @hostname

		SELECT @guestID = C.ID
		FROM Club C
		WHERE C.Name = @guestname

		INSERT INTO Match (Starttime, Endtime, HostID, GuestID) VALUES (@starttime, @endtime, @hostID, @guestID)
GO

-- iii
GO
CREATE VIEW clubsWithNoMatches
	AS
		SELECT C.Name
		FROM Club C LEFT OUTER JOIN Match M1 ON C.ID = M1.HostID
					LEFT OUTER JOIN Match M2 ON C.ID = M2.GuestID
		WHERE M1.ID IS NULL AND M2.ID IS NULL
GO

-- iv
GO
CREATE PROCEDURE deleteMatch
	@hostname VARCHAR(20),
	@guestname VARCHAR(20)
	AS
		DECLARE @hostID INTEGER
		DECLARE @guestID INTEGER

		SELECT @hostID = C.ID
		FROM Club C
		WHERE C.Name = @hostname

		SELECT  @guestID = C.ID
		FROM Club C
		WHERE C.Name = @guestname

		DELETE FROM Match
		WHERE HostID = @hostID AND GuestID = @guestID
GO

-- v
GO
CREATE PROCEDURE deleteMatchesOnStadium
	@stadiumname VARCHAR(20)
	AS
		DECLARE @stadiumID INTEGER

		SELECT @stadiumID = S.ID
		FROM Stadium S
		WHERE S.Name = @stadiumname

		DELETE FROM Match
		WHERE Starttime >= CURRENT_TIMESTAMP AND StadiumID = @stadiumID
GO

-- vi
GO
CREATE PROCEDURE addClub
	@clubname VARCHAR(20),
	@location VARCHAR(20)
	AS
		INSERT INTO Club (name, location) VALUES (@clubname, @location)
GO

-- vii
GO
CREATE PROCEDURE addTicket
	@hostname VARCHAR(20),
	@guestname VARCHAR(20),
	@starttime DATETIME
	AS
		DECLARE @hostID INTEGER
		DECLARE @guestID INTEGER
		DECLARE @matchID INTEGER

		SELECT  @hostID = C.ID
		FROM Club C
		WHERE C.Name = @hostname

		SELECT @guestID = C.ID
		FROM Club C
		WHERE C.Name = @guestname

		SELECT @matchID = M.ID
		FROM Match M
		WHERE  M.Starttime =  @starttime AND M.HostID = @hostID AND M.GuestID = @guestID 

		INSERT INTO Ticket (Status, MatchID) VALUES (1, @matchID)
GO

-- viii
GO
CREATE PROCEDURE deleteClub
	@clubname VARCHAR(20)
	AS
	DECLARE @clubID INTEGER;
	DECLARE @username VARCHAR(20);

	SELECT @clubID = C.ID
	FROM Club C
	WHERE C.Name = @clubname;

	DELETE FROM Match
	WHERE HostID = @clubID OR GuestID = @clubID;

	SELECT @username = R.username
	FROM ClubRepresentative R
	WHERE R.RepresntativeClubID = @clubID

	DELETE FROM Club
	WHERE Name = @clubname

	DELETE FROM SystemUser
	WHERE username = @username
GO

-- ix
GO
CREATE PROCEDURE addStadium
	@stadiumname VARCHAR(20),
	@location VARCHAR(20),
	@capacity INTEGER
	AS
		INSERT INTO Stadium (Status, Name, Capacity, Location) VALUES ('True',@stadiumname, @capacity, @location)
GO

-- x
GO
CREATE PROCEDURE deleteStadium
	@stadiumname VARCHAR(20)
	AS 
		DECLARE @stadiumID INTEGER
		DECLARE @stadiummanagerID INTEGER
		DECLARE @username VARCHAR(20)
	
		SELECT @stadiumID = S.ID
		FROM Stadium S
		WHERE S.Name = @stadiumname

		SELECT @stadiummanagerID = SM.ID, @username = SM.username
		FROM StadiumManager SM
		WHERE SM.StadiumID = @stadiumID

		DELETE FROM Hostrequest
		WHERE StadiumManagerID = @stadiummanagerID

		EXEC deleteMatchesOnStadium @stadiumname
		
		DELETE FROM Stadium 
		WHERE Name = @stadiumname

		DELETE FROM SystemUser
		WHERE username = @username
GO

-- xi
GO
CREATE PROCEDURE blockFan
	@fannationalid VARCHAR(20)
	AS
		UPDATE Fan
		SET Status = 'False'
		WHERE NationalID = @fannationalid
GO

-- xii
GO
CREATE PROCEDURE unblockFan
	@fannationalid VARCHAR(20)
	AS
		UPDATE Fan
		SET Status = 'True'
		WHERE NationalID = @fannationalid
GO

-- xiii
GO
CREATE PROCEDURE addRepresentative
	@name VARCHAR(20),
	@clubname VARCHAR(20),
	@username VARCHAR(20),
	@password VARCHAR(20)
	AS
		DECLARE @clubID INTEGER
		
		SELECT @clubID = C.ID
		FROM Club C
		WHERE C.Name = @clubname

		IF NOT EXISTS (
						SELECT R.ID
						FROM ClubRepresentative R
						WHERE R.RepresntativeClubID = @clubID
				   )
		BEGIN
			IF NOT EXISTS (
								SELECT SU.username
								FROM SystemUser SU
								WHERE SU.username = @username
						   )
			BEGIN
				INSERT INTO SystemUser (username, Password) VALUES (@username, @password)
				INSERT INTO ClubRepresentative (username, Name, RepresntativeClubID) VALUES (@username, @name, @clubID)
			END
		END
GO

-- xiv
GO
CREATE FUNCTION viewAvailableStadiumsOn (@starttime DATETIME) 
	RETURNS TABLE 
	AS 
	RETURN 
		(SELECT S.Name, S.Location, S.Capacity
		FROM Stadium S
		WHERE S.Status = 'True')
		EXCEPT
		(SELECT S.Name, S.Location, S.Capacity
		FROM Match M INNER JOIN Stadium S ON M.StadiumID = S.ID
		WHERE S.Status = 'True' AND  @starttime BETWEEN M.Starttime AND M.Endtime)	
GO

-- xv
GO
CREATE PROCEDURE addHostRequest
	@clubname VARCHAR(20),
	@stadiumname VARCHAR(20),
	@starttime DATETIME
	AS
		DECLARE @hostID INTEGER
		DECLARE @representativeID INTEGER
		DECLARE @matchID INTEGER
		DECLARE @stadiumID INTEGER
		DECLARE @stadiummanagerID INTEGER

		SELECT @hostID = C.ID
		FROM Club C
		WHERE C.Name = @clubname

		SELECT @representativeID = R.ID
		FROM ClubRepresentative R
		WHERE R.RepresntativeClubID = @hostID
		
		SELECT @matchID = M.ID
		FROM Match M
		WHERE M.HostID = @hostID AND M.Starttime = @starttime

		SELECT @stadiumID = S.ID
		FROM Stadium S
		WHERE S.Name = @stadiumname

		SELECT @stadiummanagerID = SM.ID
		FROM StadiumManager SM
		WHERE SM.StadiumID = @stadiumID

		INSERT INTO Hostrequest (Status, Match_ID, RepresentativeID, StadiumManagerID) VALUES ('unhandled', @matchID, @representativeID, @stadiummanagerID)
GO

-- xvi
GO
CREATE FUNCTION allUnassignedMatches (@hostname VARCHAR(20)) 
	RETURNS TABLE 
	AS
	RETURN
	SELECT G.Name, M.Starttime
	FROM Match M INNER JOIN Club H ON M.HostID = H.ID
				 INNER JOIN Club G ON M.GuestID = G.ID
	WHERE M.StadiumID IS NULL AND H.Name = @hostname;
GO

-- xvii
GO
CREATE PROCEDURE addStadiumManager
	@name VARCHAR(20),
	@stadiumname VARCHAR(20),
	@username VARCHAR(20),
	@password VARCHAR(20)
	AS
	DECLARE @stadiumID INTEGER
	DECLARE @flag BIT
		
	SELECT @stadiumID = S.ID
	FROM Stadium S
	WHERE S.Name = @stadiumname

	IF NOT EXISTS (
						SELECT SM.ID
						FROM StadiumManager SM
						WHERE SM.StadiumID = @stadiumID
				   )
	BEGIN
		IF NOT EXISTS (
								SELECT SU.username
								FROM SystemUser SU
								WHERE SU.username = @username
						   )
		BEGIN
			INSERT INTO SystemUser (username, Password) VALUES (@username, @password)
			INSERT INTO StadiumManager (username, Name, StadiumID) VALUES (@username, @name, @stadiumID)
		END
	END
GO

-- xviii
GO
CREATE FUNCTION allPendingRequests (@stadiummanagerusername VARCHAR(20)) 
	RETURNS TABLE 
	AS 
	RETURN
	SELECT R.Name AS 'Representative Name', G.Name AS 'Guest Name', M.Starttime
	FROM StadiumManager SM INNER JOIN Hostrequest HR ON SM.ID = HR.StadiumManagerID
						   INNER JOIN ClubRepresentative R ON HR.RepresentativeID = R.ID
						   INNER JOIN Match M ON HR.Match_ID = M.ID
						   INNER JOIN Club H ON M.HostID = H.ID 
						   INNER JOIN Club G ON M.GuestID = G.ID
	WHERE SM.username = @stadiummanagerusername AND HR.Status = 'unhandled'
GO

GO
CREATE PROCEDURE acceptRequest
	@stadiumanagerusername VARCHAR(20), 
	@hostname VARCHAR(20),
	@guestname VARCHAR(20),
	@starttime DATETIME
	AS
	DECLARE @stadiummanagerID INTEGER
	DECLARE @hostID INTEGER
	DECLARE @guestID INTEGER
	DECLARE @matchID INTEGER
	DECLARE @representativeID INTEGER
	DECLARE @stadiumID INTEGER
	DECLARE @capacity INTEGER
	
	SELECT  @hostID = C.ID
	FROM Club C
	WHERE C.Name = @hostname;

	SELECT @guestID = C.ID
	FROM Club C
	WHERE C.Name = @guestname;

	SELECT @representativeID = R.ID
	FROM ClubRepresentative R
	WHERE R.RepresntativeClubID = @hostID

	SELECT @matchID = M.ID
	FROM Match M
	WHERE M.Starttime = @starttime 
	AND M.HostID = @hostID 
	AND M.GuestID = @guestID;

	SELECT @stadiummanagerID = SM.ID, @stadiumID = SM.StadiumID
	FROM StadiumManager SM
	WHERE SM.username = @stadiumanagerusername

	SELECT @capacity = S.Capacity
	FROM Stadium S
	WHERE S.ID = @stadiumID

	UPDATE Hostrequest
	SET Status = 'accepted'
	WHERE Match_ID = @matchID AND RepresentativeID = @representativeID AND Status = 'unhandled' AND StadiumManagerID = @stadiummanagerID

	UPDATE Match
	SET StadiumID = @stadiumID
	WHERE ID = @matchID

	WHILE @capacity > 0 
		BEGIN
			EXEC addTicket @hostname, @guestname, @starttime;
			SET @capacity = @capacity - 1;
		END;
GO

-- xx 
GO
CREATE PROCEDURE rejectRequest 
	@stadiumanagerusername VARCHAR(20), 
	@hostname VARCHAR(20),
	@guestname VARCHAR(20),
	@starttime DATETIME
	AS
	DECLARE @stadiummanagerID INTEGER
	DECLARE @hostID INTEGER
	DECLARE @guestID INTEGER
	DECLARE @matchID INTEGER
	DECLARE @representativeID INTEGER
	DECLARE @stadiumID INTEGER
	DECLARE @capacity INTEGER
	
	SELECT  @hostID = C.ID
	FROM Club C
	WHERE C.Name = @hostname;

	SELECT @guestID = C.ID
	FROM Club C
	WHERE C.Name = @guestname;

	SELECT @representativeID = R.ID
	FROM ClubRepresentative R
	WHERE R.RepresntativeClubID = @hostID

	SELECT @matchID = M.ID
	FROM Match M
	WHERE M.Starttime = @starttime 
	AND M.HostID = @hostID 
	AND M.GuestID = @guestID;

	SELECT @stadiummanagerID = SM.ID, @stadiumID = SM.StadiumID
	FROM StadiumManager SM
	WHERE SM.username = @stadiumanagerusername

	SELECT @capacity = S.Capacity
	FROM Stadium S
	WHERE S.ID = @stadiumID

	UPDATE Hostrequest
	SET Status = 'rejected'
	WHERE Match_ID = @matchID AND RepresentativeID = @representativeID AND Status = 'unhandled' AND StadiumManagerID = @stadiummanagerID
GO

-- xxi
GO
CREATE PROCEDURE addFan 
	@fanname VARCHAR(20),
	@fanusername VARCHAR(20),
	@fanpassword VARCHAR(20),
	@nationalID VARCHAR(20),
	@birthdate DATETIME,
	@address VARCHAR(20),
	@phonenumber INTEGER
	AS
	IF NOT EXISTS (
								SELECT SU.username
								FROM SystemUser SU
								WHERE SU.username = @fanusername
						   )
	BEGIN
		INSERT INTO SystemUser VALUES (@fanusername, @fanpassword);
		INSERT INTO Fan VALUES (@fanusername,@nationalID, @phonenumber, @birthdate, @fanname, @address, 'True')
	END
GO


-- xxii
GO
CREATE FUNCTION upcomingMatchesOfClub (@clubname VARCHAR(20)) 
	RETURNS TABLE
	AS
	RETURN 
	SELECT H.Name AS 'Given Club Name', G.Name AS 'Competing Club Name', M.Starttime, S.Name AS 'Stadium Name'
	FROM Match M INNER JOIN Club H ON M.HostID = H.ID
				 INNER JOIN Club G ON M.GuestID = G.ID
				 INNER JOIN Stadium S ON M.StadiumID = S.ID
	WHERE H.Name = @clubname AND M.starttime >= CURRENT_TIMESTAMP
	UNION
	SELECT G.Name AS 'Given Club Name', H.Name AS 'Competing Club Name', M.Starttime, S.Name AS 'Stadium Name'
	FROM Match M INNER JOIN Club H ON M.HostID = H.ID
				 INNER JOIN Club G ON M.GuestID = G.ID
				 INNER JOIN Stadium S ON M.StadiumID = S.ID
	WHERE G.Name = @clubname AND M.starttime >= CURRENT_TIMESTAMP
GO	

-- xxiii
GO
CREATE FUNCTION availableMatchesToAttend (@availabletime DATETIME) 
	RETURNS TABLE 
	AS
	RETURN
	SELECT H.Name AS 'Host Name', G.Name AS 'Guest Name',  M.Starttime, S.Name
	FROM Match M INNER JOIN Club H ON M.HostID = H.ID
				 INNER JOIN Club G ON M.GuestID = G.ID
				 INNER JOIN Stadium S ON M.StadiumID = S.ID
	WHERE M.Starttime >= @availabletime AND EXISTS (
														SELECT T.ID
														FROM Ticket T
														WHERE T.MatchID = M.ID AND T.Status = 'TRUE' AND T.FanNationalID IS NULL
													)
GO

-- xxiv
GO
CREATE PROCEDURE purchaseTicket 
	@fannationalID VARCHAR(20),
	@hostname VARCHAR(20),
	@guestname VARCHAR(20),
	@starttime DATETIME
	AS
	DECLARE @hostID INTEGER;
	DECLARE @guestID INTEGER;
	DECLARE @matchID INTEGER;
	DECLARE @ticketID INTEGER;

	IF EXISTS (
					SELECT F.NationalID
					FROM Fan F
					WHERE F.NationalID = @fannationalID AND F.Status = 'TRUE'
			   )
	BEGIN
		SELECT @hostID = C.ID
		FROM Club C
		WHERE C.Name = @hostname 
	
		SELECT @guestID = C.ID 
		FROM Club C
		WHERE C.Name = @guestname

		SELECT @matchId = M.ID
		FROM Match M
		WHERE M.Starttime = @starttime AND M.HostID = @hostID AND M.GuestID = @guestID;

		
		SELECT TOP (1) @ticketId = T.ID
		FROM Ticket T
		WHERE T.MatchID = @matchId AND T.FanNationalID IS NULL AND T.Status = 'TRUE';

		UPDATE Ticket 
		SET Status = 'False', FanNationalID = @fannationalID
		WHERE ID = @ticketID;
	END
GO

-- xxv
GO
CREATE PROCEDURE updateMatchHost
	@hostname VARCHAR(20),
	@otherclubname VARCHAR(20),
	@starttime DATETIME
	AS
	DECLARE @hostID INTEGER;
	DECLARE @otherclubID INTEGER;
	DECLARE @matchID INTEGER;

	SELECT @hostID = C.ID
	FROM Club C
	WHERE C.Name = @hostname

	SELECT @otherclubID = C.ID
	FROM Club C
	WHERE C.Name = @otherclubname

	SELECT @matchID = M.ID
	FROM Match M
	WHERE M.Starttime = @starttime AND M.HostID = @hostID AND M.GuestID = @otherclubID

	UPDATE Match
	SET HostID = @otherclubID, GuestID = @hostID, StadiumID = NULL
	WHERE ID = @matchID;
GO

-- xxvii
GO
CREATE VIEW matchesPerTeam 
	AS 
	SELECT ClubName, COUNT(MatchID) AS 'Number of Matches'
	FROM (
			SELECT C.ID, C.Name, M.ID
			FROM Club C INNER JOIN Match M ON C.ID = M.GuestID
			WHERE M.Endtime <= CURRENT_TIMESTAMP AND M.StadiumID IS NOT NULL
			UNION
			SELECT C.ID, C.Name, M.ID
			FROM Club C INNER JOIN Match M ON C.ID = M.HostID
			WHERE M.Endtime <= CURRENT_TIMESTAMP AND M.StadiumID IS NOT NULL
		  ) AS D(ClubID, ClubName, MatchID)
	GROUP BY ClubID, ClubName;
GO
-- xxviii
GO
CREATE VIEW clubsNeverMatched
	AS
	SELECT C1.Name AS 'First Club Name', C2.Name AS 'Second Club Name'
	FROM Club C1, Club C2
	WHERE C1.ID < C2.ID AND NOT EXISTS (
											SELECT M.HostID, M.GuestID
											FROM Match M
											WHERE C1.ID = M.HostID AND C2.ID = M.GuestID
											UNION
											SELECT M.GuestID, M.HostID
											FROM Match M
											WHERE C1.ID = M.GuestID AND C2.ID = M.HostID
										);
GO
-- xxix
GO
CREATE FUNCTION clubsNeverPlayed (@clubname VARCHAR(20)) 
	RETURNS TABLE 
	AS
	RETURN
	SELECT M.[Second Club Name]
	FROM clubsNeverMatched M
	WHERE M.[First Club Name] = @clubname
	UNION
	SELECT M.[First Club Name]
	FROM clubsNeverMatched M
	WHERE M.[Second Club Name] = @clubname
GO
-- xxx
GO
CREATE FUNCTION matchWithHighestAttendance () 
	RETURNS TABLE 
	AS
	RETURN
	SELECT H.Name AS 'Hosting Club Name', G.Name AS 'Competing Club Name'
	FROM Ticket T INNER JOIN Match M ON T.MatchID = M.ID
					INNER JOIN Club H ON M.HostID = H.ID
					INNER JOIN Club G ON M.GuestID = G.ID
	WHERE T.Status = 'False' AND T.FanNationalID IS NOT NULL AND M.StadiumID IS NOT NULL
	GROUP BY T.MatchID, H.Name, G.Name
	HAVING COUNT(T.ID) = (
						SELECT MAX(D.NumberOfTickets)
						FROM ( 
								SELECT COUNT(T.ID) 
								FROM Ticket T INNER JOIN Match M ON T.MatchID = M.ID
												INNER JOIN Club H ON M.HostID = H.ID
												INNER JOIN Club G ON M.GuestID = G.ID
								WHERE T.Status = 'False' AND T.FanNationalID IS NOT NULL AND M.StadiumID IS NOT NULL AND M.Endtime <= CURRENT_TIMESTAMP
								GROUP BY T.MatchID, H.Name, G.Name
								) AS D(NumberOfTickets)
						)
GO

-- xxxi
GO
CREATE FUNCTION matchesRankedByAttendance () 
	RETURNS TABLE 
	AS
	RETURN
	SELECT TOP 100 PERCENT D.HostName, D.GuestName
	FROM (
			SELECT H.Name AS 'HostName' , G.Name AS 'GuestName', COUNT(T.ID) AS 'NumberOfTickets' 
			FROM Ticket T INNER JOIN Match M ON T.MatchID = M.ID
						  INNER JOIN Club H ON M.HostID = H.ID
						  INNER JOIN Club G ON M.GuestID = G.ID
			WHERE T.Status = 'False' AND T.FanNationalID IS NOT NULL AND M.StadiumID IS NOT NULL AND M.Endtime <= CURRENT_TIMESTAMP
			GROUP BY T.MatchID, H.Name, G.Name
		  ) AS D(HostName, GuestName, NumberOfTickets)
	ORDER BY D.NumberOfTickets DESC;
GO

-- xxxii
GO
CREATE FUNCTION requestsFromClub (@stadiumname VARCHAR(20), @clubname VARCHAR(20)) 
	RETURNS TABLE 
	AS
	RETURN
	SELECT H.Name AS 'Hosting Club', G.Name AS 'Competing Club'
	FROM Hostrequest HR INNER JOIN StadiumManager SM ON HR.StadiumManagerID = SM.ID
						INNER JOIN Stadium S ON SM.StadiumID = S.ID
						INNER JOIN Match M ON HR.Match_ID = M.ID
						INNER JOIN Club H ON H.ID = M.HostID
						INNER JOIN Club G ON G.ID = M.GuestID
	WHERE S.Name = @stadiumname AND H.Name = @clubname;
GO


-------------------------------------------------------------------------------------------------------
-- Extra Procedure for Milestone 3

-- 2.2 Sport Association Manager
GO
CREATE PROCEDURE deleteCertainMatch
	@hostname VARCHAR(20),
	@guestname VARCHAR(20),
	@starttime DATETIME,
	@endtime DATETIME
	AS
		DECLARE @hostID INTEGER
		DECLARE @guestID INTEGER

		SELECT @hostID = C.ID
		FROM Club C
		WHERE C.Name = @hostname

		SELECT  @guestID = C.ID
		FROM Club C
		WHERE C.Name = @guestname

		DELETE FROM Match
		WHERE HostID = @hostID AND GuestID = @guestID AND Starttime = @starttime AND Endtime = @endtime
GO

GO
CREATE View alreadyPlayedMatch
	AS
	SELECT H.Name AS 'Host Club Name', G.Name AS 'Guest Club Name', M.Starttime AS 'Start Time', M.Endtime AS 'End Time'
	FROM Match M INNER JOIN Club H ON M.HostID = H.ID
				 INNER JOIN Club G ON M.GuestID = G.ID
	WHERE Endtime <= CURRENT_TIMESTAMP
GO

GO
CREATE View upcomingMatch
	AS
	SELECT H.Name AS 'Host Club Name', G.Name AS 'Guest Club Name', M.Starttime AS 'Start Time', M.Endtime AS 'End Time'
	FROM Match M INNER JOIN Club H ON M.HostID = H.ID
				 INNER JOIN Club G ON M.GuestID = G.ID
	WHERE Starttime >= CURRENT_TIMESTAMP
GO

-- 2.3 Club Representative
GO
CREATE PROCEDURE HaveRepresentative
	@clubname VARCHAR(20),
	@success INTEGER OUTPUT
	AS
		IF EXISTS (
					SELECT *
					FROM ClubRepresentative R INNER JOIN Club C ON R.RepresntativeClubID = C.ID
					WHERE C.Name = @clubname
				  )
			BEGIN
				SET @success = 1;
			END
		ELSE
			SET @success = 0;
GO

GO
CREATE FUNCTION upcomingMatchesOfClub2 (@clubname VARCHAR(20)) 
	RETURNS TABLE
	AS
	RETURN 
	SELECT H.Name AS 'Host Name', G.Name AS 'Guest Name', M.Starttime, M.Endtime, S.Name AS 'Stadium Name'
	FROM Match M INNER JOIN Club H ON M.HostID = H.ID
				 INNER JOIN Club G ON M.GuestID = G.ID
				 LEFT OUTER JOIN Stadium S ON M.StadiumID = S.ID
	WHERE H.Name = @clubname AND M.starttime >= CURRENT_TIMESTAMP
	UNION
	SELECT H.Name AS 'Host Name', G.Name AS 'Guest Name', M.Starttime, M.Endtime, S.Name AS 'Stadium Name'
	FROM Match M INNER JOIN Club H ON M.HostID = H.ID
				 INNER JOIN Club G ON M.GuestID = G.ID
				 LEFT OUTER JOIN Stadium S ON M.StadiumID = S.ID
	WHERE G.Name = @clubname AND M.starttime >= CURRENT_TIMESTAMP
GO

-- 2.4 Stadium Manager
GO
CREATE PROCEDURE HaveStadiumManager
	@stadiumname VARCHAR(20),
	@success INTEGER OUTPUT
	AS
		IF EXISTS (
					SELECT *
					FROM StadiumManager SM INNER JOIN Stadium S ON SM.StadiumID = S.ID
					WHERE S.Name = @stadiumname
				  )
			BEGIN
				SET @success = 1;
			END
		ELSE
			SET @success = 0;
GO

GO
CREATE FUNCTION allReceivedRequests (@stadiummanagerusername VARCHAR(20)) 
	RETURNS TABLE 
	AS 
	RETURN
	SELECT R.Name AS 'Representative Name', H.Name AS 'Host Name', G.Name AS 'Guest Name', M.Starttime, M.Endtime, HR.Status
	FROM StadiumManager SM INNER JOIN Hostrequest HR ON SM.ID = HR.StadiumManagerID
						   INNER JOIN ClubRepresentative R ON HR.RepresentativeID = R.ID
						   INNER JOIN Match M ON HR.Match_ID = M.ID
						   INNER JOIN Club H ON M.HostID = H.ID 
						   INNER JOIN Club G ON M.GuestID = G.ID
	WHERE SM.username = @stadiummanagerusername
GO

GO
CREATE FUNCTION allUnhandeledRequests (@stadiummanagerusername VARCHAR(20)) 
	RETURNS TABLE 
	AS 
	RETURN
	SELECT H.Name AS 'Host Name', G.Name AS 'Guest Name', M.Starttime
	FROM StadiumManager SM INNER JOIN Hostrequest HR ON SM.ID = HR.StadiumManagerID
						   INNER JOIN ClubRepresentative R ON HR.RepresentativeID = R.ID
						   INNER JOIN Match M ON HR.Match_ID = M.ID
						   INNER JOIN Club H ON M.HostID = H.ID 
						   INNER JOIN Club G ON M.GuestID = G.ID
	WHERE SM.username = @stadiummanagerusername AND HR.Status = 'unhandled'
GO

GO
CREATE PROCEDURE AcceptedRequests 
	@hostname VARCHAR(20),
	@guestname VARCHAR(20),
	@starttime DATETIME,
	@success INTEGER OUTPUT
	AS 
	IF EXISTS (
					SELECT *
					FROM Hostrequest HR INNER JOIN Match M ON HR.Match_ID = M.ID
										INNER JOIN Club H ON M.HostID = H.ID 
										INNER JOIN Club G ON M.GuestID = G.ID
					WHERE H.Name = @hostname AND G.Name = @guestname AND M.Starttime = @starttime AND HR.Status = 'accepted'
			   )
		SET @success = 1;
	ELSE
		SET @success = 0;
GO

-- System Admin
INSERT INTO SystemUser VALUES ('mazen', '12345');
INSERT INTO SystemAdmin VALUES ('mazen', 'Mazen');


Exec clearAllTables
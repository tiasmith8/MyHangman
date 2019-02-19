-- Switch to the system (aka master) database
USE master;
GO

-- Delete the HangmanPlayers Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='HangmanPlayers')
DROP DATABASE HangmanPlayers;
GO

-- Create a new EmployeeDB Database
CREATE DATABASE HangmanPlayers;
GO

-- Switch to the EmployeeDB Database
USE HangmanPlayers 
GO

BEGIN TRANSACTION;

CREATE TABLE players (
	player_id integer identity NOT NULL, --System generated number
	first_name varchar(30) NOT NULL, --Player enters first name
	last_name varchar(30) NOT NULL, --Player enters last name
	username varchar(100) UNIQUE NOT NULL, --Player chooses a unique username
	[password] varchar(10) NOT NULL, --Player chooses a password

	CONSTRAINT pk_player_id PRIMARY KEY (player_id)
);

SET IDENTITY_INSERT players ON;

--Set initial player
INSERT INTO players (player_id, first_name, last_name, username, password)
VALUES(1, 'Player', 'One', 'ReadyPlayerOne', 'player');

SET IDENTITY_INSERT players OFF;

COMMIT TRANSACTION;

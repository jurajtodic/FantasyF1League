DROP TABLE FantasyTeamToDriver
DROP TABLE Driver
DROP TABLE Constructor
DROP TABLE ScoringRules
DROP TABLE FantasyTeam
DROP TABLE FantasyLeague

CREATE TABLE FantasyLeague (
    FantasyLeagueId uniqueidentifier NOT NULL PRIMARY KEY,
	FantasyLeagueName varchar(255) NOT NULL,
    Budget FLOAT NOT NULL,
    MaximumTeams int NOT NULL,
	MaximumFreeSubs int NULL,
	MaximumDriversPerTeam int NULL,
	CreationDate DateTime NOT NULL,
	LastUpdated DateTime NOT NULL
);

CREATE TABLE FantasyTeam (
    FantasyTeamId uniqueidentifier NOT NULL PRIMARY KEY,
	FantasyTeamName varchar(255) NOT NULL,
	Username varchar(255) NOT NULL,
	FreeSubsLeft int NOT NULL,
	CreationDate DateTime NOT NULL,
	LastUpdated DateTime NOT NULL,
	FantasyLeagueId uniqueidentifier FOREIGN KEY REFERENCES FantasyLeague(FantasyLeagueId) ON DELETE CASCADE,
	RemainingBudget FLOAT NOT NULL,
	TotalPoints int NOT NULL
);

CREATE TABLE ScoringRules (
	 ScoringRulesId uniqueidentifier NOT NULL PRIMARY KEY,
	 Position1 int NOT NULL, 
	 Position2 int NOT NULL, 
	 Position3 int NOT NULL, 
	 Position4 int NOT NULL, 
	 Position5 int NOT NULL, 
	 Position6 int NOT NULL, 
	 Position7 int NOT NULL, 
	 Position8 int NOT NULL, 
	 Position9 int NOT NULL, 
	 Position10 int NOT NULL, 
	 CreationDate DateTime NOT NULL,
	 LastUpdated DateTime NOT NULL
);

CREATE TABLE Constructor (
	 ConstructorId uniqueidentifier NOT NULL PRIMARY KEY,
	 ConstructorName varchar(255) NOT NULL,
);

CREATE TABLE Driver (
    DriverId uniqueidentifier NOT NULL PRIMARY KEY,
	DriverName varchar(255) NOT NULL,
	DriverSurname varchar(255) NOT NULL,
	Age int NOT NULL,
	IsTurboDriver BIT NOT NULL,
	Price FLOAT NOT NULL,
	TotalPoints int NOT NULL,
	CreationDate DateTime NOT NULL,
	LastUpdated DateTime NOT NULL,
	ConstructorId uniqueidentifier FOREIGN KEY REFERENCES Constructor(ConstructorId) ON DELETE CASCADE,
	ScoringRulesId uniqueidentifier FOREIGN KEY REFERENCES ScoringRules(ScoringRulesId) ON DELETE CASCADE
);

CREATE TABLE FantasyTeamToDriver (
	FantasyTeamId uniqueidentifier FOREIGN KEY REFERENCES FantasyTeam(FantasyTeamId) ON DELETE CASCADE,
	DriverId uniqueidentifier FOREIGN KEY REFERENCES Driver(DriverId) ON DELETE CASCADE
	CONSTRAINT FantasyTeamFK_DriverIdFK PRIMARY KEY (FantasyTeamId,DriverId)
);

-- CONSTRUCTORS LOOKUP TABLE
INSERT INTO Constructor VALUES (NEWID(), 'RED BULL RACING RBPT');
INSERT INTO Constructor VALUES (NEWID(), 'FERRARI');
INSERT INTO Constructor VALUES (NEWID(), 'MERCEDES');
INSERT INTO Constructor VALUES (NEWID(), 'MCLAREN MERCEDES');
INSERT INTO Constructor VALUES (NEWID(), 'ALPINE RENAULT');
INSERT INTO Constructor VALUES (NEWID(), 'ALFA ROMEO FERRARI');
INSERT INTO Constructor VALUES (NEWID(), 'HAAS FERRARI');
INSERT INTO Constructor VALUES (NEWID(), 'ALPHATAURI RBPT');
INSERT INTO Constructor VALUES (NEWID(), 'ASTON MARTIN ARAMCO MERCEDES');
INSERT INTO Constructor VALUES (NEWID(), 'WILLIAMS MERCEDES');

-- SCORING RULES
INSERT INTO ScoringRules VALUES(NEWID(),25,18,15,12,10,8,6,4,2,1,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP);
--INSERT INTO ScoringRules VALUES(NEWID(),50,36,30,24,20,16,12,8,4,2,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP);

-- DRIVER
--Driver (DriverId, DriverName, DriverSurname, Age, IsTurboDriver, Price, TotalPoints, CreationDate, LastUpdated, ConstructorId, ScoringRulesId);
INSERT INTO Driver VALUES(NEWID(), 'Max', 'Verstappen',23,0,33.2,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='RED BULL RACING RBPT'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Charles', 'Leclerc',24,0,23.7,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='FERRARI'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Checo', 'Perez',36,0,18.3,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='RED BULL RACING RBPT'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Carlos', 'Sainz',26,0,15.2,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='FERRARI'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'George', 'Russell',25,0,13.5,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='MERCEDES'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Lewis', 'Hamilton',37,0,28.9,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='MERCEDES'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Lando', 'Norris',25,0,19.4,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='MCLAREN MERCEDES'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Esteban', 'Ocon',26,0,12.3,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='ALPINE RENAULT'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Valtteri', 'Bottas',33,0,11.5,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='ALFA ROMEO FERRARI'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Fernando', 'Alonso',39,0,13.6,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='ALPINE RENAULT'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Kevin', 'Magnussen',29,0,9.5,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='HAAS FERRARI'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Daniel', 'Ricciardo',29,0,10.5,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='MCLAREN MERCEDES'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Pierre', 'Gasly',25,0,9.7,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='ALPHATAURI RBPT'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Sebastian', 'Vettel',38,0,8.5,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='ASTON MARTIN ARAMCO MERCEDES'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Mick', 'Schumacher',23,0,6.5,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='HAAS FERRARI'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Yuki', 'Tsunoda',23,0,3.5,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='ALPHATAURI RBPT'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Guanyu', 'Zhou',25,0,2.5,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='ALFA ROMEO FERRARI'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Alexander', 'Albon',26,0,3.5,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='WILLIAMS MERCEDES'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Lance', 'Stroll',27,0,3.3,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='ASTON MARTIN ARAMCO MERCEDES'),(SELECT ScoringRulesId FROM ScoringRules));
INSERT INTO Driver VALUES(NEWID(), 'Nicholas', 'Latifi',29,0,1.5,0,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP, (SELECT ConstructorId FROM Constructor WHERE ConstructorName='WILLIAMS MERCEDES'),(SELECT ScoringRulesId FROM ScoringRules));

-- FANTASY LEAGUE
INSERT INTO FantasyLeague VALUES (NEWID(), 'The Best League', 100, 10, 10, 5, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO FantasyLeague VALUES (NEWID(), 'The Worst League', 200, 10, 10, 3, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

-- FANTASY TEAM
INSERT INTO FantasyTeam VALUES (NEWID(), '2001 A Spa Odyssey','juki231', (SELECT MaximumFreeSubs FROM FantasyLeague WHERE FantasyLeagueName='The Best League'), CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, (SELECT FantasyLeagueId FROM FantasyLeague WHERE FantasyLeagueName='The Best League'), (SELECT Budget FROM FantasyLeague WHERE FantasyLeagueName='The Best League'),0);
INSERT INTO FantasyTeam VALUES (NEWID(), 'Mercedes masterace','hamiltonfanboy39', (SELECT MaximumFreeSubs FROM FantasyLeague WHERE FantasyLeagueName='The Best League'), CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, (SELECT FantasyLeagueId FROM FantasyLeague WHERE FantasyLeagueName='The Best League'), (SELECT Budget FROM FantasyLeague WHERE FantasyLeagueName='The Best League'),0);
INSERT INTO FantasyTeam VALUES (NEWID(), 'Aces Wild','supermax4832', (SELECT MaximumFreeSubs FROM FantasyLeague WHERE FantasyLeagueName='The Worst League'), CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, (SELECT FantasyLeagueId FROM FantasyLeague WHERE FantasyLeagueName='The Worst League'), (SELECT Budget FROM FantasyLeague WHERE FantasyLeagueName='The Worst League'),0);
INSERT INTO FantasyTeam VALUES (NEWID(), 'Mario Kart','hamiltonfanboy39', (SELECT MaximumFreeSubs FROM FantasyLeague WHERE FantasyLeagueName='The Worst League'), CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, (SELECT FantasyLeagueId FROM FantasyLeague WHERE FantasyLeagueName='The Worst League'), (SELECT Budget FROM FantasyLeague WHERE FantasyLeagueName='The Worst League'),0);

-- FANTASY TEAM TO DRIVER
INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='2001 A Spa Odyssey'),(SELECT DriverId FROM Driver WHERE DriverSurname='Hamilton'))
INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='2001 A Spa Odyssey'),(SELECT DriverId FROM Driver WHERE DriverSurname='Leclerc'))
INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='2001 A Spa Odyssey'),(SELECT DriverId FROM Driver WHERE DriverSurname='Verstappen'))
INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='2001 A Spa Odyssey'),(SELECT DriverId FROM Driver WHERE DriverSurname='Perez'))
INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='2001 A Spa Odyssey'),(SELECT DriverId FROM Driver WHERE DriverSurname='Schumacher'))

INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='Mercedes masterace'),(SELECT DriverId FROM Driver WHERE DriverSurname='Hamilton'))
INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='Mercedes masterace'),(SELECT DriverId FROM Driver WHERE DriverSurname='Russell'))

INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='Aces Wild'),(SELECT DriverId FROM Driver WHERE DriverSurname='Verstappen'))
INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='Aces Wild'),(SELECT DriverId FROM Driver WHERE DriverSurname='Russell'))

INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='Mario Kart'),(SELECT DriverId FROM Driver WHERE DriverSurname='Hamilton'))
INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='Mario Kart'),(SELECT DriverId FROM Driver WHERE DriverSurname='Latifi'))
INSERT INTO FantasyTeamToDriver VALUES ((SELECT FantasyTeamId FROM FantasyTeam WHERE FantasyTeamName='Mario Kart'),(SELECT DriverId FROM Driver WHERE DriverSurname='Albon'))

SELECT * FROM FantasyTeam
SELECT * FROM FantasyLeague



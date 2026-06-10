-- MySQL Setup Script for ArenaX (Use with XAMPP/phpMyAdmin)
CREATE DATABASE IF NOT EXISTS ArenaXDB;
USE ArenaXDB;

SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS Scores;
DROP TABLE IF EXISTS Matches;
DROP TABLE IF EXISTS Robots;
DROP TABLE IF EXISTS Teams;
DROP TABLE IF EXISTS Tournaments;
DROP TABLE IF EXISTS Users;
SET FOREIGN_KEY_CHECKS = 1;

CREATE TABLE Users (
    UserID INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    FullName VARCHAR(100),
    IsActive BOOLEAN DEFAULT TRUE,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastLoginAt DATETIME,
    ModifiedAt DATETIME ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT CK_Users_Role CHECK (Role IN ('Admin', 'Judge', 'Operator'))
);

CREATE TABLE Tournaments (
    TournamentID INT PRIMARY KEY AUTO_INCREMENT,
    TournamentName VARCHAR(200) NOT NULL,
    Description TEXT,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Planning',
    TournamentFormat VARCHAR(20) NOT NULL DEFAULT 'Knockout',
    MaxTeams INT,
    Location VARCHAR(200),
    CreatedBy INT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Tournaments_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    CONSTRAINT CK_Tournaments_Status CHECK (Status IN ('Planning', 'Active', 'Paused', 'Completed', 'Cancelled')),
    CONSTRAINT CK_Tournaments_Format CHECK (TournamentFormat IN ('Knockout', 'RoundRobin', 'GroupStage'))
);

CREATE TABLE Teams (
    TeamID INT PRIMARY KEY AUTO_INCREMENT,
    TournamentID INT,
    TeamName VARCHAR(100) NOT NULL,
    University VARCHAR(150),
    CaptainName VARCHAR(100) NOT NULL,
    CaptainEmail VARCHAR(100) NOT NULL,
    CaptainPhone VARCHAR(20),
    ApprovalStatus VARCHAR(20) NOT NULL DEFAULT 'Pending',
    RegistrationDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ApprovedDate DATETIME,
    ApprovedBy INT,
    TeamDescription TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Teams_Tournament FOREIGN KEY (TournamentID) REFERENCES Tournaments(TournamentID),
    CONSTRAINT FK_Teams_ApprovedBy FOREIGN KEY (ApprovedBy) REFERENCES Users(UserID),
    CONSTRAINT CK_Teams_ApprovalStatus CHECK (ApprovalStatus IN ('Pending', 'Approved', 'Rejected', 'Withdrawn'))
);

CREATE TABLE Robots (
    RobotID INT PRIMARY KEY AUTO_INCREMENT,
    TeamID INT NOT NULL,
    RobotName VARCHAR(100) NOT NULL,
    Weight DECIMAL(10, 2) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    WeaponType VARCHAR(100),
    Description TEXT,
    IsApproved BOOLEAN DEFAULT FALSE,
    ApprovedDate DATETIME,
    ApprovedBy INT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Robots_Team FOREIGN KEY (TeamID) REFERENCES Teams(TeamID) ON DELETE CASCADE,
    CONSTRAINT FK_Robots_ApprovedBy FOREIGN KEY (ApprovedBy) REFERENCES Users(UserID)
);

CREATE TABLE Matches (
    MatchID INT PRIMARY KEY AUTO_INCREMENT,
    TournamentID INT NOT NULL,
    Round INT NOT NULL,
    MatchNumber INT NOT NULL,
    TeamA_ID INT NOT NULL,
    TeamB_ID INT NOT NULL,
    ScheduledTime DATETIME,
    StartTime DATETIME,
    EndTime DATETIME,
    MatchStatus VARCHAR(20) NOT NULL DEFAULT 'Scheduled',
    WinnerTeamID INT,
    WinnerDetermined VARCHAR(20),
    MatchNotes TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Matches_Tournament FOREIGN KEY (TournamentID) REFERENCES Tournaments(TournamentID),
    CONSTRAINT FK_Matches_TeamA FOREIGN KEY (TeamA_ID) REFERENCES Teams(TeamID),
    CONSTRAINT FK_Matches_TeamB FOREIGN KEY (TeamB_ID) REFERENCES Teams(TeamID),
    CONSTRAINT FK_Matches_Winner FOREIGN KEY (WinnerTeamID) REFERENCES Teams(TeamID),
    CONSTRAINT CK_Matches_Status CHECK (MatchStatus IN ('Scheduled', 'InProgress', 'Paused', 'Completed', 'Cancelled'))
);

CREATE TABLE Scores (
    ScoreID INT PRIMARY KEY AUTO_INCREMENT,
    MatchID INT NOT NULL,
    JudgeID INT NOT NULL,
    TeamID INT NOT NULL,
    Points INT NOT NULL DEFAULT 0,
    Penalties INT DEFAULT 0,
    FinalScore INT,
    ScoreNotes TEXT,
    SubmittedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT FK_Scores_Match FOREIGN KEY (MatchID) REFERENCES Matches(MatchID) ON DELETE CASCADE,
    CONSTRAINT FK_Scores_Judge FOREIGN KEY (JudgeID) REFERENCES Users(UserID),
    CONSTRAINT FK_Scores_Team FOREIGN KEY (TeamID) REFERENCES Teams(TeamID)
);

-- Indices
CREATE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_Teams_Name ON Teams(TeamName);
CREATE INDEX IX_Robots_Name ON Robots(RobotName);

-- Default Admin User (Password: admin123)
INSERT INTO Users (Username, PasswordHash, Role, Email, FullName)
VALUES ('admin', 'PBKDF2_V1$10000$7MFy92cPB6pubETqDsPdCIc67BRNr+IYHKoVpcOaQw1VByrGysfZjVew/hw+AGoPJDdmaDUBaohHfDcg8Synsg==', 'Admin', 'admin@arenax.com', 'System Administrator');

-- Sample Tournament
INSERT INTO Tournaments (TournamentName, StartDate, EndDate, Status)
VALUES ('XAMPP Challenge 2024', NOW(), DATE_ADD(NOW(), INTERVAL 7 DAY), 'Planning');

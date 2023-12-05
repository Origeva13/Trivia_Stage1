Create Database [Trivia]
Go
 Use Trivia
 Go

-- create table [TypeOfPlayer](
-- [NumPlayerType] int identity (1,1) not null,
-- [PlayerType] nvarchar not null
-- )
-- GO 

-- CREATE TABLE [Player](
-- [PlayerId] int identity (1,1) not null,
-- [PlayerName]  nvarchar not null,
-- [Email] nvarchar unique,
-- [NumOfPoints] int not null,
-- [NumPlayerType] int not null,
-- CONSTRAINT [FK_Player_TypeOfPlayer] FOREIGN KEY (NumPlayerType) REFERENCES [TypeOfPlayer] ([NumPlayerType]),
-- CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED ([PlayerId] Asc)
-- )
-- GO

--DROP TABLE TypeOfPlayer;
--GO


-- create table [TypeOfPlayer](
-- [NumPlayerType] int identity (1,1) not null,
-- [PlayerType] nvarchar not null,
-- CONSTRAINT [PK_TypeOfPlayer] PRIMARY KEY CLUSTERED ([NumPlayerType] Asc)
-- )
-- GO 

--  CREATE TABLE [Player](
-- [PlayerId] int identity (1,1) not null,
-- [PlayerName]  nvarchar not null,
-- [Email] nvarchar unique,
-- [NumOfPoints] int not null,
-- [NumPlayerType] int not null,
-- CONSTRAINT [FK_Player_TypeOfPlayer] FOREIGN KEY (NumPlayerType) REFERENCES [TypeOfPlayer] ([NumPlayerType]),
-- CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED ([PlayerId] Asc)
-- )
-- GO

 CREATE Table SubQuestion(
 [SubID] int identity (1,1) not null,
 [SubOfQuestion] nvarchar (50) not null,
 CONSTRAINT [PK_SubQuestion] PRIMARY KEY CLUSTERED ([SubID] Asc)
 )
 GO


 Drop table [TypeOfPlayer]
 Go


 Drop table Player
 Go




  create table [TypeOfPlayer](
 [NumPlayerType] int identity (1,1) not null,
 [PlayerType] nvarchar (30) not null,
 CONSTRAINT [PK_TypeOfPlayer] PRIMARY KEY CLUSTERED ([NumPlayerType] Asc)
 )
 GO 

  CREATE TABLE [Player](
 [PlayerId] int identity (1,1) not null,
 [PlayerName]  nvarchar (60) not null,
 [Email] nvarchar (120) unique,
 [NumOfPoints] int not null,
 [NumPlayerType] int not null,
 CONSTRAINT [FK_Player_TypeOfPlayer] FOREIGN KEY (NumPlayerType) REFERENCES [TypeOfPlayer] ([NumPlayerType]),
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED ([PlayerId] Asc)
 )
 GO

 CREATE Table QuestionStatus(
 [StatusID] int identity (1,1) not null,
 [StatusOfQuestion] nvarchar (50) not null,
  CONSTRAINT [PK_QuestionStatus] PRIMARY KEY CLUSTERED ([StatusID] Asc)
 )

 CREATE Table Questions(
 [QuestionNum] int identity (1,1) not null,
 [PlayerId] int not null,
 [StatusIDQuestion] int not null,
 [SubID] int not null,
 [QuestionContent] nvarchar (240) not null,
 [CorrectAnswer] nvarchar (240) not null,
 [WrongAnswer1] nvarchar (240) not null,
 [WrongAnswer2] nvarchar (240) not null,
 [WrongAnswer3] nvarchar (240) not null,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([QuestionNum] Asc),
 CONSTRAINT [FK_Questions_SubQuestion] FOREIGN KEY (SubID) REFERENCES [SubQuestion] (SubID),
 CONSTRAINT [FK_Questions_QuestionStatus] FOREIGN KEY (StatusIDQuestion) REFERENCES [QuestionStatus] (StatusID),
 CONSTRAINT [FK_Questions_Player] FOREIGN KEY (PlayerId) REFERENCES [Player] (PlayerId)
)
Go


INSERT INTO TypeOfPlayer ([PlayerType])
VALUES ('Manager');

INSERT INTO TypeOfPlayer ([PlayerType])
VALUES ('Master');

INSERT INTO TypeOfPlayer ([PlayerType])
VALUES ('Trainee');

DELETE FROM TypeOfPlayer WHERE [NumPlayerType]=5;

SELECT * FROM Player

INSERT INTO QuestionStatus ([StatusOfQuestion]) Values ('pending')

INSERT INTO QuestionStatus ([StatusOfQuestion]) Values ('approved')
INSERT INTO QuestionStatus ([StatusOfQuestion]) Values ('denied')

SELECT * FROM Questions

SELECT * FROM SubQuestion
INSERT INTO Player ([PlayerName], [Email], [NumOfPoints],  [NumPlayerType],pass) VALUES ('Bob', 'bob123@gmail.com', 0, 1,'123456789');

SELECT * FRom Player
 INSERT INTO SubQuestion ([SubOfQuestion]) VALUES ('Sports');
 INSERT INTO SubQuestion ([SubOfQuestion]) VALUES ('Politics');
 INSERT INTO SubQuestion ([SubOfQuestion]) VALUES ('History');
 INSERT INTO SubQuestion ([SubOfQuestion]) VALUES ('Science');
 INSERT INTO SubQuestion ([SubOfQuestion]) VALUES ('Ramon High School');

INSERT INTO Questions ([PlayerId], [StatusIDQuestion], [SubID], [QuestionContent], [CorrectAnswer], [WrongAnswer1], [WrongAnswer2], [WrongAnswer3]) values (1,2, 1, 'When was the first Olympics Games?', '1896', '1987', '1876', '1800')
INSERT INTO Questions ([PlayerId], [StatusIDQuestion], [SubID], [QuestionContent], [CorrectAnswer], [WrongAnswer1], [WrongAnswer2], [WrongAnswer3]) values (1,2, 2, 'Who was the first Prime Minister?', 'David Ben Gurion', 'Golda Meir', 'Ofer Zadikrio', 'Efraim Katzir')
INSERT INTO Questions ([PlayerId], [StatusIDQuestion], [SubID], [QuestionContent], [CorrectAnswer], [WrongAnswer1], [WrongAnswer2], [WrongAnswer3]) values (1,2, 3, 'What year was the Boston Tea Party?', '1773', '1778', '1776', '1873')
INSERT INTO Questions ([PlayerId], [StatusIDQuestion], [SubID], [QuestionContent], [CorrectAnswer], [WrongAnswer1], [WrongAnswer2], [WrongAnswer3]) values (1,2, 4, 'What is the first element in the periodic table of elements?', 'Hydrogen', 'Helium', 'Gold', 'Lithium')
INSERT INTO Questions ([PlayerId], [StatusIDQuestion], [SubID], [QuestionContent], [CorrectAnswer], [WrongAnswer1], [WrongAnswer2], [WrongAnswer3]) values (1,2, 5, 'Who is the current principal of Ramon High School?', 'Hannah Dalmadigo', 'Ofer Zadikrio', 'Smadar Vechter', 'Tal Duzly')

Alter Table Player ADD pass nvarchar(100);
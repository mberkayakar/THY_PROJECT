-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- THY_ARAC_TAKIP_SISTEMI.dbo.ButtonEntries definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.ButtonEntries;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.ButtonEntries (
	id int IDENTITY(1,1) NOT NULL,
	plate nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	buttonNo int NOT NULL,
	doorNo int NOT NULL,
	dateLogin datetime2 NULL,
	CONSTRAINT PK_ButtonEntries PRIMARY KEY (id)
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.Configs definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.Configs;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.Configs (
	[key] nvarchar(450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	value nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Configs PRIMARY KEY ([key])
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.DoorAssigns definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.DoorAssigns;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.DoorAssigns (
	id int IDENTITY(1,1) NOT NULL,
	doorNo int NOT NULL,
	plate nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	reservationId int NOT NULL,
	dateAssign datetime2 NULL,
	CONSTRAINT PK_DoorAssigns PRIMARY KEY (id)
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.EventLogs (
	id int IDENTITY(1,1) NOT NULL,
	message nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	eventLogType int NOT NULL,
	dateCreated datetime2 NOT NULL,
	CONSTRAINT PK_EventLogs PRIMARY KEY (id)
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.LoginLogs definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.LoginLogs;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.LoginLogs (
	id int IDENTITY(1,1) NOT NULL,
	userId nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	authority nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	email nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	dateLogin datetime2 NULL,
	CONSTRAINT PK_LoginLogs PRIMARY KEY (id)
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.NLogs definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.NLogs;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.NLogs (
	id int IDENTITY(1,1) NOT NULL,
	[Date] datetime2 NOT NULL,
	[Level] int NOT NULL,
	Message nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_NLogs PRIMARY KEY (id)
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.PTSLogs definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.PTSLogs;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.PTSLogs (
	id int IDENTITY(1,1) NOT NULL,
	doorNo int NOT NULL,
	isButtonEntry bit NOT NULL,
	buttonNo int NOT NULL,
	plate nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	brand nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	model nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[type] nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	color nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	dateLogin datetime2 NULL,
	fileName nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_PTSLogs PRIMARY KEY (id)
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.ReaderModules definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.ReaderModules;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.ReaderModules (
	id int IDENTITY(1,1) NOT NULL,
	readerName nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	readerId int NOT NULL,
	readerIp nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	readerData nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	doorCountStart int NOT NULL,
	doorCountFinish int NOT NULL,
	dateCreated datetime2 NOT NULL,
	dateUpdated datetime2 NULL,
	dateLastRead datetime2 NULL,
	CONSTRAINT PK_ReaderModules PRIMARY KEY (id)
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.Reservations definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.Reservations;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.Reservations (
	id int IDENTITY(1,1) NOT NULL,
	reservationType int NULL,
	agentId bigint NULL,
	agentName nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	driverName nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	driverSurname nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	driverPhoneNumber nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	carPlate nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	carType nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	dateEstimatedArriveStart datetime2 NULL,
	dateEstimatedArriveFinish datetime2 NULL,
	dateCarArrived datetime2 NULL,
	dateCreated datetime2 NULL,
	dateUpdated datetime2 NULL,
	isArrived bit NULL,
	isUnload bit NULL,
	isWaiting bit NULL,
	isActive bit NULL,
	sccText nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	doorNumber int NULL,
	dateDoorAssigned datetime2 NULL,
	CONSTRAINT PK_Reservations PRIMARY KEY (id)
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.SCCs definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.SCCs;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.SCCs (
	id int IDENTITY(1,1) NOT NULL,
	code nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	strength int NOT NULL,
	CONSTRAINT PK_SCCs PRIMARY KEY (id)
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.Users definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.Users;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.Users (
	id int IDENTITY(1,1) NOT NULL,
	userId nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	name nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	surname nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	title nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[role] nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	authority nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	phoneNumber nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	email nvarchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	password nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	dateLastLogin datetime2 NULL,
	dateCreated datetime2 NULL,
	dateUpdated datetime2 NULL,
	CONSTRAINT Users_PK PRIMARY KEY (id),
	CONSTRAINT Users_UN UNIQUE (email)
);
CREATE UNIQUE NONCLUSTERED INDEX Users_UN ON THY_ARAC_TAKIP_SISTEMI.dbo.Users (email);


-- THY_ARAC_TAKIP_SISTEMI.dbo.[__EFMigrationsHistory] definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.[__EFMigrationsHistory];

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.[__EFMigrationsHistory] (
	MigrationId nvarchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	ProductVersion nvarchar(32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
);


-- THY_ARAC_TAKIP_SISTEMI.dbo.AWBs definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.AWBs;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.AWBs (
	id int IDENTITY(1,1) NOT NULL,
	awbText nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	weight int NULL,
	weightUnit nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	pieces int NULL,
	destination nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	origin nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	handlingCode nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	flightDetails nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	dateFlight datetime2 NULL,
	sccText nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	reservationId int NOT NULL,
	CONSTRAINT PK_AWBs PRIMARY KEY (id),
	CONSTRAINT FK_AWBs_Reservations_reservationId FOREIGN KEY (reservationId) REFERENCES THY_ARAC_TAKIP_SISTEMI.dbo.Reservations(id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_AWBs_reservationId ON dbo.AWBs (  reservationId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- THY_ARAC_TAKIP_SISTEMI.dbo.Doors definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.Doors;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.Doors (
	id int IDENTITY(1,1) NOT NULL,
	doorNumber int NOT NULL,
	doorName nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	operationArea nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	state nvarchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[order] nvarchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	isNotEmpty bit NOT NULL,
	isBusy bit NOT NULL,
	dateCreated datetime2 NOT NULL,
	dateUpdated datetime2 NULL,
	lastOwnerReservationId int NOT NULL,
	lastOwnerPlate nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	dateLastOwned datetime2 NULL,
	dateLastExit datetime2 NULL,
	reservationId int NOT NULL,
	readerModuleId int NOT NULL,
	CONSTRAINT PK_Doors PRIMARY KEY (id),
	CONSTRAINT FK_Doors_ReaderModules_readerModuleId FOREIGN KEY (readerModuleId) REFERENCES THY_ARAC_TAKIP_SISTEMI.dbo.ReaderModules(id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_Doors_readerModuleId ON dbo.Doors (  readerModuleId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- THY_ARAC_TAKIP_SISTEMI.dbo.Lats definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.Lats;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.Lats (
	id int IDENTITY(1,1) NOT NULL,
	lat datetime2 NULL,
	sccId int NOT NULL,
	CONSTRAINT PK_Lats PRIMARY KEY (id),
	CONSTRAINT FK_Lats_SCCs_sccId FOREIGN KEY (sccId) REFERENCES THY_ARAC_TAKIP_SISTEMI.dbo.SCCs(id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_Lats_sccId ON dbo.Lats (  sccId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- THY_ARAC_TAKIP_SISTEMI.dbo.SCCTexts definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.SCCTexts;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.SCCTexts (
	id int IDENTITY(1,1) NOT NULL,
	sccText nvarchar COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	awbId int NOT NULL,
	CONSTRAINT PK_SCCTexts PRIMARY KEY (id),
	CONSTRAINT FK_SCCTexts_AWBs_awbId FOREIGN KEY (awbId) REFERENCES THY_ARAC_TAKIP_SISTEMI.dbo.AWBs(id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_SCCTexts_awbId ON dbo.SCCTexts (  awbId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- THY_ARAC_TAKIP_SISTEMI.dbo.WaitingQueues definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.WaitingQueues;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.WaitingQueues (
	id int IDENTITY(1,1) NOT NULL,
	dateCreated datetime2 NOT NULL,
	reservationId int NOT NULL,
	CONSTRAINT PK_WaitingQueues PRIMARY KEY (id),
	CONSTRAINT FK_WaitingQueues_Reservations_reservationId FOREIGN KEY (reservationId) REFERENCES THY_ARAC_TAKIP_SISTEMI.dbo.Reservations(id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_WaitingQueues_reservationId ON dbo.WaitingQueues (  reservationId ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;


-- THY_ARAC_TAKIP_SISTEMI.dbo.DoorSCC definition

-- Drop table

-- DROP TABLE THY_ARAC_TAKIP_SISTEMI.dbo.DoorSCC;

CREATE TABLE THY_ARAC_TAKIP_SISTEMI.dbo.DoorSCC (
	doorListid int NOT NULL,
	sccListid int NOT NULL,
	CONSTRAINT PK_DoorSCC PRIMARY KEY (doorListid,sccListid),
	CONSTRAINT FK_DoorSCC_Doors_doorListid FOREIGN KEY (doorListid) REFERENCES THY_ARAC_TAKIP_SISTEMI.dbo.Doors(id) ON DELETE CASCADE,
	CONSTRAINT FK_DoorSCC_SCCs_sccListid FOREIGN KEY (sccListid) REFERENCES THY_ARAC_TAKIP_SISTEMI.dbo.SCCs(id) ON DELETE CASCADE
);
 CREATE NONCLUSTERED INDEX IX_DoorSCC_sccListid ON dbo.DoorSCC (  sccListid ASC  )  
	 WITH (  PAD_INDEX = OFF ,FILLFACTOR = 100  ,SORT_IN_TEMPDB = OFF , IGNORE_DUP_KEY = OFF , STATISTICS_NORECOMPUTE = OFF , ONLINE = OFF , ALLOW_ROW_LOCKS = ON , ALLOW_PAGE_LOCKS = ON  )
	 ON [PRIMARY ] ;




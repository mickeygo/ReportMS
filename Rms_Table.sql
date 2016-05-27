
/*===== RMS Report =====*/

-- RMS_Rdbms
CREATE TABLE [dbo].[RMS_Rdbms](
	[RdbmsId]		uniqueidentifier	NOT NULL,
	[Name]			varchar(30)			NOT NULL,
	[Description]	nvarchar(100)		NULL,
	[Server]		varchar(200)		NOT NULL,
	[Catalog]		varchar(30)			NOT NULL,
	[UserId]		varchar(40)			NOT NULL,
	[Password]		varchar(40)			NOT NULL,
	[ReadOnly]		bit					NOT NULL,
	[Provider]		varchar(50)			NOT NULL,
	[Enabled]		bit					NOT NULL,
	[CreatedDate]	datetime			NULL,
	CONSTRAINT	[PK_RMS_Rdbms] PRIMARY KEY ([RdbmsId])
);


/*===== RMS_User =====*/

-- RMS_User
CREATE TABLE [dbo].[RMS_User](
	[UserId]					uniqueidentifier	NOT NULL	PRIMARY KEY,
	[UserName]					varchar(80)			NOT NULL,
	[Password]					varchar(128)		NOT NULL,
	[EmployeeNo]				varchar(30)			NULL,
	[Email]						varchar(80)			NULL,
	[EnglishName]				varchar(80)			NULL,
	[LocalName]					nvarchar(100)		NULL,
	[Company]					varchar(20)			NULL,
	[Organization]				varchar(30)			NULL,
	[OrganizationDescription]	nvarchar(100)		NULL,
	[Department]				nvarchar(100)		NULL,
	[Job]						nvarchar(100)		NULL,
	[Tel]						varchar(60)			NULL,
	[Extension]					varchar(50)			NULL,
	[VOIP]						varchar(20)			NULL,
	[OnBoardDate]				datetime			NULL,
	[Manager]					varchar(80)			NULL,
	[Agent]						varchar(50)			NULL,
	[Grade]						varchar(5)			NULL,
	[Shift]						nvarchar(60)		NULL,
	[Enabled]					bit					NOT NULL,
	[CreatedBy]					varchar(50)			NULL,
	[CreatedDate]				datetime			NULL,
	[UpdatedBy]					varchar(50)			NULL,
	[UpdatedDate]				datetime			NULL
);

-- RMS_Action
CREATE TABLE [dbo].[RMS_Action](
	[ActionId]		uniqueidentifier	NOT NULL	PRIMARY KEY,
	[Area]			varchar(30)			NULL,
	[Controller]	varchar(30)			NOT NULL,
	[Action]		varchar(30)			NOT NULL,
	[Description]	nvarchar(100)		NULL,
	[Enabled]		bit					NOT NULL,
	[CreatedBy]		varchar(50)			NULL,
	[CreatedDate]	datetime			NULL,
	[UpdatedBy]		varchar(50)			NULL,
	[UpdatedDate]	datetime			NULL
);

/*===== Subscriber =====*/

-- Topic
CREATE TABLE RMS_Topic (
	TopicId			uniqueidentifier	not null Primary Key,
	TopicName		varchar(30)			not null,
	Description		nvarchar(100)		null,
	Subject			nvarchar(100)		null,
	Body			nvarchar(max)		null,
	Enabled			bit					not null,
	CreatedBy		varchar(50)			not null,
	CreatedDate		datetime			null,
	UpdatedBy		varchar(50)			null,
	UpdatedDate		datetime			null
);

-- AttachmentTopic
CREATE TABLE RMS_AttachmentTopic (
	TopicId			uniqueidentifier	not null Primary Key,
	ReportId		uniqueidentifier	not null,
	SqlStatement	varchar(max)		not null,
	Parameter		nvarchar(max)		null
);

ALTER TABLE [RMS_AttachmentTopic] ADD CONSTRAINT [FK_RMS_AttachmentTopic_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [RMS_Topic]([TopicId]);

-- Subscriber
CREATE TABLE RMS_Subscriber (
	SubscriberId	uniqueidentifier	not null Primary Key,
	TopicId			uniqueidentifier	not null,
	Email			varchar(80)			not null
);

ALTER TABLE [RMS_Subscriber] ADD CONSTRAINT [FK_RMS_Subscriber_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [RMS_Topic]([TopicId]);

-- RMS_TopicTask
CREATE TABLE RMS_TopicTask (
	TopicTaskId		uniqueidentifier	not null Primary Key,
	TopicId			uniqueidentifier	not null,
	TaskSchedule	int		not null,
	Month			int		null,
	Week			int		null,
	Day				int		null,
	Hour			int		null,
	Minute			int		null
);

ALTER TABLE [RMS_TopicTask] ADD CONSTRAINT [FK_RMS_TopicTask_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [RMS_Topic]([TopicId]);

-- TaskRecord
CREATE TABLE RMS_TaskRecord (
	TaskRecordId		uniqueidentifier	not null,
	TopicTaskId			uniqueidentifier	not null,
	ExecuteStartTime	datetime			not	null,
	ExecuteEndTime		datetime			not	null,
	HostName			varchar(50)			not	null,
	ExecutedResult		bit					not null,
	ErrorMessage		nvarchar(3000)		null,
	CONSTRAINT	[PK_RMS_TaskRecord] PRIMARY KEY ([TaskRecordId])
);

/*===== End Subscriber =====*/
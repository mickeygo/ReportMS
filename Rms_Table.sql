/*===== RMS Report =====*/
-- RMS_Database
CREATE TABLE [dbo].[RMS_Database](
	[DatabaseId]	uniqueidentifier	NOT NULL,
	[Name]			varchar(30)			NOT NULL,
	[Description]	nvarchar(100)		NULL,
	[Server]		varchar(30)			NOT NULL,
	[Catalog]		varchar(30)			NOT NULL,
	[UserId]		varchar(40)			NOT NULL,
	[Password]		varchar(40)			NOT NULL,
	[Provider]		varchar(50)			NOT NULL,
	[Enabled]		bit					NOT NULL
 CONSTRAINT [PK_RMS_Database] PRIMARY KEY CLUSTERED 
(
	[DatabaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];


/*===== RMS_User =====*/

-- RMS_User
CREATE TABLE [dbo].[RMS_User](
	[UserId]					uniqueidentifier	NOT NULL,
	[UserName]					varchar(80)			NOT NULL,
	[Password]					varchar(128)		NOT NULL,
	[EmployeeNo]				varchar(30)			NULL,
	[Email]						varchar(80)			NULL,
	[EnglishName]				varchar(80)			NULL,
	[LocalName]					nvarchar(100)		NULL,
	[Company]					varchar(20)			NULL,
	[Organization]				varchar(30)			NULL,
	[OrganizationDescription	nvarchar(100)		NULL,
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
	[UpdatedDate]				datetime			NULL,
 CONSTRAINT [PK_RMS_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

-- RMS_Action
CREATE TABLE [dbo].[RMS_Action](
	[ActionId]		uniqueidentifier	NOT NULL,
	[Area]			varchar(30)			NULL,
	[Controller]	varchar(30)			NOT NULL,
	[Action]		varchar(30)			NOT NULL,
	[Description]	nvarchar(100)		NULL,
	[Enabled]		bit					NOT NULL,
	[CreatedBy]		varchar(50)			NULL,
	[CreatedDate]	datetime			NULL,
	[UpdatedBy]		varchar(50)			NULL,
	[UpdatedDate]	datetime			NULL,
 CONSTRAINT [PK_RMS_Action] PRIMARY KEY CLUSTERED 
(
	[ActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

/*===== Subscriber =====*/

-- Topic
CREATE TABLE RMS_Topic (
	TopicId			uniqueidentifier	not null,
	TopicName		varchar(30)			not null,
	Description		nvarchar(100)		null,
	Subject			nvarchar(100)		null,
	Body			nvarchar(max)		null,
	Enabled			bit					not null,
	CreatedBy		varchar(50)			null,
	CreatedDate		datetime			null,
	UpdatedBy		varchar(50)			null,
	UpdatedDate		datetime			null,
	CONSTRAINT	[PK_RMS_Topic] PRIMARY KEY ([TopicId])
);

-- AttachmentTopic
CREATE TABLE RMS_AttachmentTopic (
	TopicId			uniqueidentifier	not null,
	ReportId		uniqueidentifier	not null,
	SqlStatement	varchar(max)		not null,
	Parameter		nvarchar(max)		null,
	CONSTRAINT	[PK_RMS_AttachmentTopic] PRIMARY KEY ([TopicId])
);

ALTER TABLE [RMS_AttachmentTopic] ADD CONSTRAINT [FK_RMS_AttachmentTopic_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [RMS_Topic]([TopicId]);

-- Subscriber
CREATE TABLE RMS_Subscriber (
	SubscriberId	uniqueidentifier	not null,
	TopicId			uniqueidentifier	not null,
	Email			varchar(80)			not null,
	CONSTRAINT	[PK_RMS_Subscriber] PRIMARY KEY ([SubscriberId])
);

ALTER TABLE [RMS_Subscriber] ADD CONSTRAINT [FK_RMS_Subscriber_TopicId] FOREIGN KEY ([TopicId]) REFERENCES [RMS_Topic]([TopicId]);

-- RMS_TopicTask
CREATE TABLE RMS_TopicTask (
	TopicTaskId		uniqueidentifier	not null,
	TopicId			uniqueidentifier	not null,
	TaskSchedule	int		not null,
	Month			int		null,
	Week			int		null,
	Day				int		null,
	Hour			int		null,
	Minute			int		null,
	CONSTRAINT	[PK_RMS_TopicTask] PRIMARY KEY ([TopicTaskId])
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

ALTER TABLE [RMS_TaskRecord] ADD CONSTRAINT [FK_RMS_TaskRecord_TopicTaskId] FOREIGN KEY ([TopicTaskId]) REFERENCES [RMS_TopicTask]([TopicTaskId]);

/*===== End Subscriber =====*/
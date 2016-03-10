/*===== RMS_User =====*/


/*===== Subscriber =====*/

-- Topic
CREATE TABLE RMS_Topic (
	TopicId			uniqueidentifier	not null,
	TopicName		varchar(30)			not null,
	Description		nvarchar(100)		null,
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
	TaskRecordId	uniqueidentifier	not null,
	TopicTaskId		uniqueidentifier	not null,
	ExecuteTime		datetime			not	null,
	ExecuteResult	bit					not null,
	ErrorMessage	nvarchar(3000)		null,
	CONSTRAINT	[PK_RMS_TaskRecord] PRIMARY KEY ([TaskRecordId])
);

ALTER TABLE [RMS_TaskRecord] ADD CONSTRAINT [FK_RMS_TaskRecord_TopicTaskId] FOREIGN KEY ([TopicTaskId]) REFERENCES [RMS_TopicTask]([TopicTaskId]);

/*===== End Subscriber =====*/
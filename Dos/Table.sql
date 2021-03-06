CREATE TABLE [dbo].[content](
	[id] [int] IDENTITY(1,1) NOT NULL,  --ID
	[title] [nvarchar](50) NOT NULL,   --标题
	[content] [nvarchar](max) NOT NULL,  --内容
	[status] [int] NOT NULL,  --状态
	[add_time] [datetime] NOT NULL,  --创建时间
	[modify_time] [datetime] NULL,  --修改时间
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[content] ADD  CONSTRAINT [DF_Content_status]  DEFAULT ((1)) FOR [status]
GO

ALTER TABLE [dbo].[content] ADD  CONSTRAINT [DF_content_add_time]  DEFAULT (getdate()) FOR [add_time]
GO

CREATE TABLE [dbo].[comment](
	[id] [int] IDENTITY(1,1) NOT NULL,  --ID
	[content_id] [int] NOT NULL,  --内容ID
	[content] [nvarchar](512) NOT NULL,  --内容
	[add_time] [datetime] NOT NULL,  --创建时间
 CONSTRAINT [PK_comment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[comment] ADD  CONSTRAINT [DF_comment_add_time]  DEFAULT (getdate()) FOR [add_time]
GO
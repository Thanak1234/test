USE [Workflow]
GO

/****** Object:  Table [BPMDATA].[DOCUMENTS]    Script Date: 01/Dec/15 3:44:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [BPMDATA].[DOCUMENTS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SERIAL] [varchar](100) NULL,
	[NAME] [varchar](100) NULL,
	[COMMENT] [varchar](100) NULL,
	[FILE_CONTENT] [nvarchar](max) NULL,
	[CREATED_DATE] [datetime] NOT NULL,
 CONSTRAINT [PK_BPMDATA_DOCUMENTS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [BPMDATA].[DOCUMENTS] ADD  CONSTRAINT [DF__DOCUMENTS__CREAT__6CF8245B]  DEFAULT (getdate()) FOR [CREATED_DATE]
GO


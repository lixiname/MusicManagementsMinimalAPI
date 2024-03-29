USE [sqlserver1]
GO
/****** Object:  Table [dbo].[Music]    Script Date: 2024/3/5 15:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Music](
	[Id] [int] NOT NULL,
	[MusicName] [nvarchar](max) NOT NULL,
	[MusicImageUrl] [nvarchar](max) NULL,
	[MusicContentUrl] [nvarchar](max) NULL,
	[Author] [nvarchar](max) NULL,
	[MusicType] [int] NULL,
	[UploadUserId] [int] NOT NULL,
	[Review] [int] NOT NULL,
	[DownLoadNum] [int] NOT NULL,
	[AgreedNum] [int] NOT NULL,
	[TalkNum] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TalkRelate]    Script Date: 2024/3/5 15:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TalkRelate](
	[MusicId] [int] NOT NULL,
	[UploadUserId] [int] NOT NULL,
	[TalkId] [int] NOT NULL,
	[Contents] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMusicRelate]    Script Date: 2024/3/5 15:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMusicRelate](
	[MusicId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_UserMusicRelate] PRIMARY KEY CLUSTERED 
(
	[MusicId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserS]    Script Date: 2024/3/5 15:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserS](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[profilePictureUrl] [nvarchar](max) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Music] ([Id], [MusicName], [MusicImageUrl], [MusicContentUrl], [Author], [MusicType], [UploadUserId], [Review], [DownLoadNum], [AgreedNum], [TalkNum]) VALUES (1, N'music1', N'E:\\fileCatalog\\localAssertDB\\g.png', N'E:\\fileCatalog\\localAssertDB\\Live.mp3', NULL, NULL, 1, 1, 0, 0, 0)
INSERT [dbo].[Music] ([Id], [MusicName], [MusicImageUrl], [MusicContentUrl], [Author], [MusicType], [UploadUserId], [Review], [DownLoadNum], [AgreedNum], [TalkNum]) VALUES (2, N'music2', N'E:\\fileCatalog\\localAssertDB\\g.png', N'E:\\fileCatalog\\localAssertDB\\Live.mp3', NULL, NULL, 1, 1, 0, 1, 1)
INSERT [dbo].[Music] ([Id], [MusicName], [MusicImageUrl], [MusicContentUrl], [Author], [MusicType], [UploadUserId], [Review], [DownLoadNum], [AgreedNum], [TalkNum]) VALUES (3, N'music3', N'E:\\fileCatalog\\localAssertDB\\g.png', N'E:\\fileCatalog\\localAssertDB\\Live.mp3', NULL, NULL, 1, 1, 0, 0, 0)
GO
INSERT [dbo].[TalkRelate] ([MusicId], [UploadUserId], [TalkId], [Contents]) VALUES (2, 1, 2, N'string')
GO
INSERT [dbo].[UserMusicRelate] ([MusicId], [UserId]) VALUES (2, 1)
GO
INSERT [dbo].[UserS] ([Id], [Name], [Password], [profilePictureUrl], [Email], [Phone]) VALUES (1, N'wz', N'1', NULL, NULL, NULL)
INSERT [dbo].[UserS] ([Id], [Name], [Password], [profilePictureUrl], [Email], [Phone]) VALUES (2, N'xj', N'1', NULL, NULL, NULL)
GO

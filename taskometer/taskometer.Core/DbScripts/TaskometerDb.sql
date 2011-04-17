USE [TaskometerDb]
GO

CREATE TABLE [dbo].[Account](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[DateCreated] DateTime NOT NULL,
	[DateLastLogin] DateTime NOT NULL,
	[Password] NVarChar(50),
	[Provider] NVarChar(50),
	[ProviderType] NVarChar(50),
	[Status] NVarChar(50) NOT NULL,
	[Username] NVarChar(50) NOT NULL,
	[Tenant] BigInt NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Category](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[IsRoot] Bit NOT NULL,
	[Name] NVarChar(200) NOT NULL,
	[UniquePath] NVarChar(500) NOT NULL,
	[UrlAlias] NVarChar(100) NOT NULL,
	[DefaultPage] NVarChar(100) NOT NULL,
	[PermissionSet] BigInt NOT NULL,
	[Tenant] BigInt NOT NULL,
	[Website] BigInt NOT NULL,
	[Parent] BigInt,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Comment](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[AddedByDisplayName] NVarChar(100) NOT NULL,
	[AddedByEmail] NVarChar(200) NOT NULL,
	[AddedByWebsite] NVarChar(200),
	[AddedOn] DateTime NOT NULL,
	[Body] NText NOT NULL,
	[IsApproved] Bit NOT NULL,
	[IPAddress] NVarChar(25) NOT NULL,
	[Page] BigInt NOT NULL,
	[Tenant] BigInt NOT NULL,
	[AddedBy] BigInt,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Control](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Name] NVarChar(100) NOT NULL,
	[Type] NVarChar(100) NOT NULL,
	[VirtualPath] NVarChar(500) NOT NULL,
 CONSTRAINT [PK_Control] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[File](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Filename] NVarChar(250) NOT NULL,
	[Title] NVarChar(250) NOT NULL,
	[PermissionSet] BigInt NOT NULL,
	[Tenant] BigInt NOT NULL,
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[IssuesPortal](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Domain] NVarChar(200) NOT NULL,
	[IsActive] Bit NOT NULL,
	[Path] NVarChar(200) NOT NULL,
	[Tenant] BigInt NOT NULL,
 CONSTRAINT [PK_IssuesPortal] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Meta](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Text] NVarChar(100) NOT NULL,
	[Url] NVarChar(500) NOT NULL,
	[Tenant] BigInt NOT NULL,
	[Website] BigInt NOT NULL,
 CONSTRAINT [PK_Meta] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Page](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[AllowComments] Bit NOT NULL,
	[ContentType] NVarChar(50) NOT NULL,
	[DateTime] DateTime NOT NULL,
	[Excerpt] NVarChar(500),
	[Html] NText,
	[MainContentHtml] NText,
	[Syndicate] Bit NOT NULL,
	[Tags] NVarChar(200) NOT NULL,
	[Title] NVarChar(200) NOT NULL,
	[UniquePath] NVarChar(500) NOT NULL,
	[PermissionSet] BigInt NOT NULL,
	[DisplayTemplate] BigInt NOT NULL,
	[Tenant] BigInt NOT NULL,
	[Author] BigInt NOT NULL,
	[Category] BigInt NOT NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Permission](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Assignee] BigInt NOT NULL,
	[AssigneeType] NVarChar(50) NOT NULL,
	[Edit] Bit NOT NULL,
	[View] Bit NOT NULL,
	[PermissionSet] BigInt NOT NULL,
	[Tenant] BigInt NOT NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PermissionSet](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Tenant] BigInt NOT NULL,
 CONSTRAINT [PK_PermissionSet] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[ProjectPortal](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Domain] NVarChar(200) NOT NULL,
	[IsActive] Bit NOT NULL,
	[Path] NVarChar(200) NOT NULL,
	[Tenant] BigInt NOT NULL,
 CONSTRAINT [PK_ProjectPortal] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Revision](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Contents] NText NOT NULL,
	[DateTime] DateTime NOT NULL,
	[Html] NText,
	[MainContentHtml] NText,
	[Page] BigInt NOT NULL,
	[Tenant] BigInt NOT NULL,
 CONSTRAINT [PK_Revision] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Role](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Name] NVarChar(50) NOT NULL,
	[Tenant] BigInt NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Template](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Html] NText NOT NULL,
	[Name] NVarChar(50) NOT NULL,
	[Placeholders] NText NOT NULL,
	[Tenant] BigInt NOT NULL,
	[Website] BigInt NOT NULL,
 CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Tenant](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Domain] NVarChar(50) NOT NULL,
	[Logo] NVarChar(250) NOT NULL,
	[Name] NVarChar(50) NOT NULL,
	[Plan] NVarChar(50) NOT NULL,
	[Timezone] NVarChar(10) NOT NULL,
 CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[User](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[FirstName] NVarChar(50) NOT NULL,
	[LastName] NVarChar(50) NOT NULL,
	[Tenant] BigInt NOT NULL,
	[Account] BigInt NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[Website](
	[Id] BigInt IDENTITY(1,1) NOT NULL,
	[Domain] NVarChar(200) NOT NULL,
	[Name] NVarChar(50) NOT NULL,
	[Path] NVarChar(200) NOT NULL,
	[Title] NVarChar(200) NOT NULL,
	[Type] NVarChar(50) NOT NULL,
	[Tenant] BigInt NOT NULL,
 CONSTRAINT [PK_Website] PRIMARY KEY CLUSTERED (
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[AccountRoleMap](
	[Account] BigInt NOT NULL,
	[Role] BigInt NOT NULL,
 CONSTRAINT [PK_AccountRoleMap] PRIMARY KEY CLUSTERED (
	[Account] ASC,
	[Role] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[TemplateControlMap](
	[Control] BigInt NOT NULL,
	[Template] BigInt NOT NULL,
 CONSTRAINT [PK_TemplateControlMap] PRIMARY KEY CLUSTERED (
	[Control] ASC,
	[Template] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AccountRoleMap]  WITH CHECK ADD  CONSTRAINT [FK_AccountRoleMap_Account_Account_Roles] FOREIGN KEY([Account])
REFERENCES [dbo].[Account] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AccountRoleMap] CHECK CONSTRAINT [FK_AccountRoleMap_Account_Account_Roles]
GO

ALTER TABLE [dbo].[AccountRoleMap]  WITH CHECK ADD  CONSTRAINT [FK_AccountRoleMap_Role_Role_Accounts] FOREIGN KEY([Role])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AccountRoleMap] CHECK CONSTRAINT [FK_AccountRoleMap_Role_Role_Accounts]
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Account_Account_User_OneToOne] FOREIGN KEY([Account])
REFERENCES [dbo].[Account] ([Id])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Account_Account_User_OneToOne]
GO

ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Category_Category_Pages] FOREIGN KEY([Category])
REFERENCES [dbo].[Category] ([Id])
GO

ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Category_Category_Pages]
GO

ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Parent_Category_ChildCategories] FOREIGN KEY([Parent])
REFERENCES [dbo].[Category] ([Id])
GO

ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Parent_Category_ChildCategories]
GO

ALTER TABLE [dbo].[TemplateControlMap]  WITH CHECK ADD  CONSTRAINT [FK_TemplateControlMap_Control_Control_Templates] FOREIGN KEY([Control])
REFERENCES [dbo].[Control] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TemplateControlMap] CHECK CONSTRAINT [FK_TemplateControlMap_Control_Control_Templates]
GO

ALTER TABLE [dbo].[TemplateControlMap]  WITH CHECK ADD  CONSTRAINT [FK_TemplateControlMap_Template_Template_Controls] FOREIGN KEY([Template])
REFERENCES [dbo].[Template] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[TemplateControlMap] CHECK CONSTRAINT [FK_TemplateControlMap_Template_Template_Controls]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Page_Page_Comments] FOREIGN KEY([Page])
REFERENCES [dbo].[Page] ([Id])
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Page_Page_Comments]
GO

ALTER TABLE [dbo].[Revision]  WITH CHECK ADD  CONSTRAINT [FK_Revision_Page_Page_Revisions] FOREIGN KEY([Page])
REFERENCES [dbo].[Page] ([Id])
GO

ALTER TABLE [dbo].[Revision] CHECK CONSTRAINT [FK_Revision_Page_Page_Revisions]
GO

ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_PermissionSet_PermissionSet_File_OneToOne] FOREIGN KEY([PermissionSet])
REFERENCES [dbo].[PermissionSet] ([Id])
GO

ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_PermissionSet_PermissionSet_File_OneToOne]
GO

ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_PermissionSet_PermissionSet_Page_OneToOne] FOREIGN KEY([PermissionSet])
REFERENCES [dbo].[PermissionSet] ([Id])
GO

ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_PermissionSet_PermissionSet_Page_OneToOne]
GO

ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_PermissionSet_PermissionSet_Category_OneToOne] FOREIGN KEY([PermissionSet])
REFERENCES [dbo].[PermissionSet] ([Id])
GO

ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_PermissionSet_PermissionSet_Category_OneToOne]
GO

ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_PermissionSet_PermissionSet_Permissions] FOREIGN KEY([PermissionSet])
REFERENCES [dbo].[PermissionSet] ([Id])
GO

ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_PermissionSet_PermissionSet_Permissions]
GO

ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_DisplayTemplate_Template_Pages] FOREIGN KEY([DisplayTemplate])
REFERENCES [dbo].[Template] ([Id])
GO

ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_DisplayTemplate_Template_Pages]
GO

ALTER TABLE [dbo].[Revision]  WITH CHECK ADD  CONSTRAINT [FK_Revision_Tenant_Tenant_Revisions] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[Revision] CHECK CONSTRAINT [FK_Revision_Tenant_Tenant_Revisions]
GO

ALTER TABLE [dbo].[Website]  WITH CHECK ADD  CONSTRAINT [FK_Website_Tenant_Tenant_Websites] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[Website] CHECK CONSTRAINT [FK_Website_Tenant_Tenant_Websites]
GO

ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_Tenant_Tenant_Files] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_Tenant_Tenant_Files]
GO

ALTER TABLE [dbo].[IssuesPortal]  WITH CHECK ADD  CONSTRAINT [FK_IssuesPortal_Tenant_Tenant_IssuesPortal_OneToOne] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[IssuesPortal] CHECK CONSTRAINT [FK_IssuesPortal_Tenant_Tenant_IssuesPortal_OneToOne]
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Tenant_Tenant_Users] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Tenant_Tenant_Users]
GO

ALTER TABLE [dbo].[ProjectPortal]  WITH CHECK ADD  CONSTRAINT [FK_ProjectPortal_Tenant_Tenant_ProjectPortal_OneToOne] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[ProjectPortal] CHECK CONSTRAINT [FK_ProjectPortal_Tenant_Tenant_ProjectPortal_OneToOne]
GO

ALTER TABLE [dbo].[PermissionSet]  WITH CHECK ADD  CONSTRAINT [FK_PermissionSet_Tenant_Tenant_PermissionSets] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[PermissionSet] CHECK CONSTRAINT [FK_PermissionSet_Tenant_Tenant_PermissionSets]
GO

ALTER TABLE [dbo].[Template]  WITH CHECK ADD  CONSTRAINT [FK_Template_Tenant_Tenant_Templates] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[Template] CHECK CONSTRAINT [FK_Template_Tenant_Tenant_Templates]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Tenant_Tenant_Comments] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Tenant_Tenant_Comments]
GO

ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Tenant_Tenant_Accounts] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Tenant_Tenant_Accounts]
GO

ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Tenant_Tenant_Categories] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Tenant_Tenant_Categories]
GO

ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Tenant_Tenant_Pages] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Tenant_Tenant_Pages]
GO

ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_Tenant_Tenant_Roles] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_Tenant_Tenant_Roles]
GO

ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Tenant_Tenant_Permissions] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_Tenant_Tenant_Permissions]
GO

ALTER TABLE [dbo].[Meta]  WITH CHECK ADD  CONSTRAINT [FK_Meta_Tenant_Tenant_Metas] FOREIGN KEY([Tenant])
REFERENCES [dbo].[Tenant] ([Id])
GO

ALTER TABLE [dbo].[Meta] CHECK CONSTRAINT [FK_Meta_Tenant_Tenant_Metas]
GO

ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Author_User_Pages] FOREIGN KEY([Author])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Author_User_Pages]
GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_AddedBy_User_Comments] FOREIGN KEY([AddedBy])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_AddedBy_User_Comments]
GO

ALTER TABLE [dbo].[Meta]  WITH CHECK ADD  CONSTRAINT [FK_Meta_Website_Website_Meta] FOREIGN KEY([Website])
REFERENCES [dbo].[Website] ([Id])
GO

ALTER TABLE [dbo].[Meta] CHECK CONSTRAINT [FK_Meta_Website_Website_Meta]
GO

ALTER TABLE [dbo].[Template]  WITH CHECK ADD  CONSTRAINT [FK_Template_Website_Website_Templates] FOREIGN KEY([Website])
REFERENCES [dbo].[Website] ([Id])
GO

ALTER TABLE [dbo].[Template] CHECK CONSTRAINT [FK_Template_Website_Website_Templates]
GO

ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Website_Website_Categories] FOREIGN KEY([Website])
REFERENCES [dbo].[Website] ([Id])
GO

ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Website_Website_Categories]
GO


USE SchoolDb
GO

INSERT INTO [dbo].[Subjects]([Name],CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
VALUES('Lengua Espanola','JCT',GETUTCDATE(),'JCT',GETUTCDATE()),
('Ciencia Sociales','JCT',GETUTCDATE(),'JCT',GETUTCDATE()),
('Naturales','JCT',GETUTCDATE(),'JCT',GETUTCDATE())
,('Matematica','JCT',GETUTCDATE(),'JCT',GETUTCDATE())
GO

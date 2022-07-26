-- Foods Table ========================
CREATE TABLE Foods (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    IsDeactivated bit  NOT NULL,
    ArchiveLogIds nvarchar(200)  NULL,
    Name nvarchar(200)  NOT NULL,
    Description nvarchar(200)  NULL,
    Image_FileName nvarchar(1500)  NULL,
    Price int  NULL,
    Shop uniqueidentifier  NULL
);
CREATE INDEX [IX_Foods->Shop] ON Foods (Shop);

GO


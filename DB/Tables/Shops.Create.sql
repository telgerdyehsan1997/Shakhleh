-- Shops Table ========================
CREATE TABLE Shops (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    IsDeactivated bit  NOT NULL,
    ArchiveLogIds nvarchar(200)  NULL,
    Name nvarchar(200)  NULL,
    Address nvarchar(MAX)  NULL,
    Description nvarchar(200)  NULL,
    Email nvarchar(200)  NULL,
    Phone nvarchar(200)  NULL
);


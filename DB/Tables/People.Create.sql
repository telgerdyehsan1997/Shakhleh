-- People Table ========================
CREATE TABLE People (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    FirstName nvarchar(200)  NOT NULL,
    LastName nvarchar(200)  NOT NULL,
    Email nvarchar(100)  NOT NULL,
    MobileNumber nvarchar(200)  NULL
);


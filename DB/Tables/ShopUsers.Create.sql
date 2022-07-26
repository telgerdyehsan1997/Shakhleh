-- ShopUsers Table ========================
CREATE TABLE ShopUsers (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    IsAdmin bit  NOT NULL,
    Shop uniqueidentifier  NULL
);
CREATE INDEX [IX_ShopUsers->Shop] ON ShopUsers (Shop);

GO


-- ShopCustomers Table ========================
CREATE TABLE ShopCustomers (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    Shop uniqueidentifier  NOT NULL
);
CREATE INDEX [IX_ShopCustomers->Shop] ON ShopCustomers (Shop);

GO


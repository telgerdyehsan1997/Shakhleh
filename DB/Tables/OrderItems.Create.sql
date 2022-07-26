-- OrderItems Table ========================
CREATE TABLE OrderItems (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    [Count] int  NOT NULL,
    [Order] uniqueidentifier  NOT NULL,
    Food uniqueidentifier  NOT NULL
);
CREATE INDEX [IX_OrderItems->Order] ON OrderItems ([Order]);

GO


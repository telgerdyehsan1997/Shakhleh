-- Discounts Table ========================
CREATE TABLE Discounts (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    IsDeactivated bit  NOT NULL,
    ArchiveLogIds nvarchar(200)  NULL,
    Name nvarchar(200)  NULL,
    Description nvarchar(200)  NULL,
    [Percent] numeric(27, 2)  NULL,
    Amount int  NULL,
    Shop uniqueidentifier  NOT NULL,
    IsUserSpecific bit  NOT NULL,
    MinimumAmountOfPriceToUse numeric(27, 2)  NULL,
    MaximumAmountOfPriceToUse numeric(27, 2)  NULL,
    [Start] datetime2  NULL,
    [End] datetime2  NULL,
    CalculationType uniqueidentifier  NOT NULL,
    FoodType uniqueidentifier  NULL
);
CREATE INDEX [IX_Discounts->Shop] ON Discounts (Shop);

GO


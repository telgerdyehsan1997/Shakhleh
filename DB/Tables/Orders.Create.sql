-- Orders Table ========================
CREATE TABLE Orders (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    Details nvarchar(MAX)  NULL,
    DateReceived datetime2  NOT NULL,
    Shop uniqueidentifier  NOT NULL,
    Customer uniqueidentifier  NULL,
    UsedDiscount uniqueidentifier  NULL,
    TotalPriceWithDiscount int  NULL
);
CREATE INDEX [IX_Orders->Shop] ON Orders (Shop);

GO


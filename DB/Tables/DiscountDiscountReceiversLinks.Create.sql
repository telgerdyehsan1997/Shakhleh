-- DiscountDiscountReceiversLinks Table ========================
CREATE TABLE DiscountDiscountReceiversLinks (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    Discount uniqueidentifier  NOT NULL,
    Shopcustomer uniqueidentifier  NOT NULL
);
CREATE INDEX [IX_DiscountDiscountReceiversLinks->Discount] ON DiscountDiscountReceiversLinks (Discount);

GO


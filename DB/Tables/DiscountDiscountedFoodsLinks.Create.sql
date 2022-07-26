-- DiscountDiscountedFoodsLinks Table ========================
CREATE TABLE DiscountDiscountedFoodsLinks (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    Discount uniqueidentifier  NOT NULL,
    Food uniqueidentifier  NOT NULL
);
CREATE INDEX [IX_DiscountDiscountedFoodsLinks->Discount] ON DiscountDiscountedFoodsLinks (Discount);

GO


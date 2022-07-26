-- DiscountExcludedFoodsLinks Table ========================
CREATE TABLE DiscountExcludedFoodsLinks (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    Discount uniqueidentifier  NOT NULL,
    Food uniqueidentifier  NOT NULL
);
CREATE INDEX [IX_DiscountExcludedFoodsLinks->Discount] ON DiscountExcludedFoodsLinks (Discount);

GO


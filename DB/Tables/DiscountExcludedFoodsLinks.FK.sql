ALTER TABLE DiscountExcludedFoodsLinks ADD Constraint
                [FK_DiscountExcludedFoodsLink.Discount]
                FOREIGN KEY (Discount)
                REFERENCES Discounts (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE DiscountExcludedFoodsLinks ADD Constraint
                [FK_DiscountExcludedFoodsLink.Food]
                FOREIGN KEY (Food)
                REFERENCES Foods (ID)
                ON DELETE NO ACTION;
GO
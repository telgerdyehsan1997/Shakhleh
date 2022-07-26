ALTER TABLE DiscountDiscountedFoodsLinks ADD Constraint
                [FK_DiscountDiscountedFoodsLink.Discount]
                FOREIGN KEY (Discount)
                REFERENCES Discounts (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE DiscountDiscountedFoodsLinks ADD Constraint
                [FK_DiscountDiscountedFoodsLink.Food]
                FOREIGN KEY (Food)
                REFERENCES Foods (ID)
                ON DELETE NO ACTION;
GO
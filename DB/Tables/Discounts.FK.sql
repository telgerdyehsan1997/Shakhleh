ALTER TABLE Discounts ADD Constraint
                [FK_Discount.Shop]
                FOREIGN KEY (Shop)
                REFERENCES Shops (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE Discounts ADD Constraint
                [FK_Discount.CalculationType->DiscountCalculationType]
                FOREIGN KEY (CalculationType)
                REFERENCES DiscountCalculationTypes (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE Discounts ADD Constraint
                [FK_Discount.FoodType->DiscountFoodType]
                FOREIGN KEY (FoodType)
                REFERENCES DiscountFoodTypes (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE OrderItems ADD Constraint
                [FK_OrderItem.Order]
                FOREIGN KEY ([Order])
                REFERENCES Orders (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE OrderItems ADD Constraint
                [FK_OrderItem.Food]
                FOREIGN KEY (Food)
                REFERENCES Foods (ID)
                ON DELETE NO ACTION;
GO
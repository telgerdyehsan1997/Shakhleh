ALTER TABLE Foods ADD Constraint
                [FK_Food.Shop]
                FOREIGN KEY (Shop)
                REFERENCES Shops (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE Orders ADD Constraint
                [FK_Order.Shop]
                FOREIGN KEY (Shop)
                REFERENCES Shops (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE Orders ADD Constraint
                [FK_Order.Customer->ShopCustomer]
                FOREIGN KEY (Customer)
                REFERENCES ShopCustomers (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE Orders ADD Constraint
                [FK_Order.UsedDiscount->Discount]
                FOREIGN KEY (UsedDiscount)
                REFERENCES Discounts (ID)
                ON DELETE NO ACTION;
GO
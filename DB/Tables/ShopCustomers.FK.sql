ALTER TABLE ShopCustomers ADD Constraint
                [FK_ShopCustomer.Shop]
                FOREIGN KEY (Shop)
                REFERENCES Shops (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE ShopCustomers ADD CONSTRAINT 
[FK_ShopCustomer.Id->User] FOREIGN KEY (Id) 
REFERENCES Users (ID)
 ON DELETE CASCADE;


GO
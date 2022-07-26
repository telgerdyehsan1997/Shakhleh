ALTER TABLE ShopUsers ADD Constraint
                [FK_ShopUser.Shop]
                FOREIGN KEY (Shop)
                REFERENCES Shops (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE ShopUsers ADD CONSTRAINT 
[FK_ShopUser.Id->User] FOREIGN KEY (Id) 
REFERENCES Users (ID)
 ON DELETE CASCADE;


GO
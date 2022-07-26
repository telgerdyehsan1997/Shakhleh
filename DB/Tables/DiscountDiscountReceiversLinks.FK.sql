ALTER TABLE DiscountDiscountReceiversLinks ADD Constraint
                [FK_DiscountDiscountReceiversLink.Discount]
                FOREIGN KEY (Discount)
                REFERENCES Discounts (ID)
                ON DELETE NO ACTION;
GO
ALTER TABLE DiscountDiscountReceiversLinks ADD Constraint
                [FK_DiscountDiscountReceiversLink.Shopcustomer->ShopCustomer]
                FOREIGN KEY (Shopcustomer)
                REFERENCES ShopCustomers (ID)
                ON DELETE NO ACTION;
GO
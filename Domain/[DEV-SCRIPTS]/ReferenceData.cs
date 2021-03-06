using Olive;
using Olive.Entities;
using Olive.Entities.Data;
using Olive.Security;
using System.Threading.Tasks;

namespace Domain
{
    public class ReferenceData : IReferenceData
    {
        static IDatabase Database => Context.Current.Database();

        async Task<T> Create<T>(T item) where T : IEntity
        {
            await Context.Current.Database().Save(item);
            return item;
        }

        public async Task Create()
        {
            await Create(new Settings { Name = "Current", PasswordResetTicketExpiryMinutes = 2 });

            await CreateAdmins();

            await CreateDiscountFoodTypes();
            await CreateDiscountCalculationTypes();

            //await CreateShops();

        }

        async Task CreateAdmins()
        {
#pragma warning disable GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
            await AddAdmin("test", "testington", "admin@uat.co");
        }

        async Task CreateDiscountFoodTypes()
        {
            await Create(new DiscountFoodType
            {
                Name = nameof(DiscountFoodType.AllFoodsButThereIsExclusion),
                DisplayName = "همه غذاها به همراه استثناء"
            });
            await Create(new DiscountFoodType
            {
                Name = nameof(DiscountFoodType.OnlySpecifiedFoods),
                DisplayName = "فقط غذاهای مشخص شده"
            });
        }

        async Task CreateDiscountCalculationTypes()
        {
            await Create(new DiscountCalculationType
            {
                Name = nameof(DiscountCalculationType.Amount),
                DisplayName = "براساس مبلغ"
            });
            await Create(new DiscountCalculationType
            {
                Name = nameof(DiscountCalculationType.Percentage),
                DisplayName = "براساس درصد"
            });
        }

        async Task CreateShops()
        {
            var mokhtar = await Create(new Shop
            {
                Name = "مختار",
                Address = "تقاطع امت و ایثار",
                Phone = "09211370996",
                Description = "برای مختار و یاران",
                Email = "telgerdyehsan@gmail.com"
            });
            var pass = SecurePassword.Create("test");
            var mokhtarAdmin = await Create(new ShopUser
            {
                Shop= mokhtar,
                FirstName="مختار",
                LastName="مختاری",
                IsAdmin= true,
                Email = "mokhtar@uat.co",
                Phone= "09211370995",
                Password = pass.Password,
                Salt = pass.Salt
            });
            var bandari = await Create(new Food { 
            Name="بندری",
            Description="با سوسیس اضافه",
            Shop=mokhtar,
            Price=30000
            });
            var hotdog = await Create(new Food
            {
                Name = "هات داگ",
                Description = "با سوسیس اضافه",
                Shop = mokhtar,
                Price = 35000
            });
            var soda = await Create(new Food
            {
                Name = "نوشابه",
                Description = "خانواده",
                Shop = mokhtar,
                Price = 13000
            });
            var openingDiscount = await Create(new Discount
            {
                Name = "تخفیف افتتاحیه",
                Shop = mokhtar,
                Percent = 50,
                CalculationType = DiscountCalculationType.Percentage,
                Start = System.DateTime.Today,
                End = System.DateTime.Today,
            });
        }
        private Task<Administrator> AddAdmin(string firstName, string lastName, string email)
        {
            var pass = SecurePassword.Create("test");
            return Create(new Administrator
            {
#pragma warning disable GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                Email = email,
#pragma warning restore GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                FirstName = firstName,
                LastName = lastName,
                Password = pass.Password,
                Salt = pass.Salt
            });
        }
    }
}

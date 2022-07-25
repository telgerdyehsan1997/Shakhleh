using Domain;
using MSharp;

namespace Modules
{
    public class FactorOverview : GenericModule
    {
        public FactorOverview()
        {
            HeaderText("نمای کلی فاکتور");

            Markup(@"
@{
        var foodItems = await Model.Order.FoodItems.GetList();
    }
<div class='grid-wrapper'>
        <table class='grid'>
            <thead>
                <tr>
                    <th>نام غذا</th>
                    <th>تعداد</th>
                    <th>قیمت این تعداد غذا</th>
                </tr>
            </thead>
            <tbody>
                    @foreach (var item in foodItems)
                    {
                <tr>
                        <td>@item.Food.Name</td>
                        <td>@item.Count</td>
                        <td>@(item.Count*item.Food.Price)</td>
                </tr>
                    }
            </tbody>
        </table>
        <div class='view-body'>
            <div class='form-group row'>
                <label class='control-label'>
                    قیمت کل
                </label>
                <div class='group-control'>
                    @Model.Order.TotalPrice
                </div>
            </div>
            @if (Model.Order.TotalPriceWithDiscount.HasValue)
            {
                <div class='form-group row'>
                    <label class='control-label'>
                        تخفیف اعمال شده
                    </label>
                    <div class='group-control'>
                        @Model.Order.UsedDiscount.Name
                    </div>
                </div>
                <div class='form-group row'>
                    <label class='control-label'>
                        قیمت کل با اعمال تخفیف
                    </label>
                    <div class='group-control'>
                        @Model.Order.TotalPriceWithDiscount
                    </div>
                </div>
            } 
<div class='form-group row'>
                    <div class='group-control'>
                        <a name='PrintReceipt' data-add-subform='FoodItems' target=""_blank"" class='btn btn-secondary' href='/Factor/Index?order=@Model.Order.ID'>
                            <i class='fas fa-print'></i>
                            پرینت رسید
                        </a>
                    </div>
                </div>

         <div class='form-group row'>
            <div class='group-control'>
               <button type='submit' name='PrintReceipt' formtarget='_blank' data-add-subform='FoodItems' class='btn btn-secondary' formaction='@Url.Index(""ShopUserOrders"")'>
                  بازگشت به لیست سفارشات </button>
            </ div >
         </ div >
        </div>
    </div>");

            ViewModelProperty<Domain.Order>("order").FromRequestParam("order");

        }
    }
}

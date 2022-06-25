using MSharp;
using Olive;

namespace Modules
{
    class TransitOfficeFileErrorList : ListModule<Domain.TransitOfficeFileError>
    {
        public TransitOfficeFileErrorList()
        {

            HeaderText(@"Error Log - " + LocalTime.Now)
                .PageSize(10)
                .Sortable();

            Column(x => x.RowNumber);
            Column(x => x.ErrorReason);

            DataSource("await info.TransitOfficeFile.Errors.GetList()");
            ViewModelProperty<Domain.TransitOfficeFile>("TransitOfficeFile").FromRequestParam("transitOfficeFile");

            Button("Back").OnClick(x => x.ReturnToPreviousPage());
        }
    }
}
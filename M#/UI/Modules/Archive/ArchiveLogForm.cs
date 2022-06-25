using Domain;
using MSharp;
using Olive;
using Olive.Entities;

namespace Modules
{
    class ArchiveLogForm : FormModule<ArchiveLog>
    {
        public ArchiveLogForm()
        {
            Header(@"<h2>@(""Unarchive"".OnlyWhen(info.Entity?.IsDeactivated ?? false).Or(""Archive""))</h2>
                   
                    <div class=""form-body"">
                             @{
                             var archiveLog = await Database.Of<ArchiveLog>().Where(x => x.EntityId == info.Entity.ID).OrderByDescending(x => x.DateAndTime).FirstOrDefault();
                             }
                   @if (archiveLog != null)
                    {
                      <div class=""form-group row"">
                           
                                <label for=""ReadOnlyReason"" class=""control-label"">
                                    @(""Archive"".OnlyWhen(info.Entity?.IsDeactivated ?? false).Or(""Unarchive"")) Reason
                                </label >
                                <div class=""group-control"">
                                    @archiveLog?.Reason
                                </div>
                       </div>

                    }
                  @if (info.Entity?.IsDeactivated == true && archiveLog?.TrackingNumber.HasValue() == true)
                    {
                      <div class=""form-group row"">
                           
                                <label for=""ReadOnlyReason"" class=""control-label"">
                                  Replacement tracking Number
                                </label >
                                <div class=""group-control"">
                                   @archiveLog?.TrackingNumber
                                </div>
                       </div>

                    }
                    @if((info.Entity?.IsDeactivated ?? false))
                    {
                       <div class=""form-group row"">
                                <label for=""ReadOnlyReason"" class=""control-label"">
                                    Logged in user
                                </label>
                                <div class=""group-control"">
                                    @archiveLog?.LoggedInUser.Name
                                </div>
                      </div>
                    
                    <div class=""form-group row"">
                                <label for=""ReadOnlyReason"" class=""control-label"">
                                    Date and time
                                </label>
                                <div class=""group-control"">
                                    @archiveLog?.DateAndTime.ToString()
                                </div>
                      </div> 
                  }
                </div>                     
             ");

            Field(x => x.Reason)
            .Label("Please Explain Why")
            .Mandatory();

            Button("Cancel")
                .OnClick(x => x.CloseModal());


            ViewModelProperty<IArchivable>("Entity")
                .FromRequestParam("entity");

            ViewModelProperty<Consignment>("Consignment")
                .FromRequestParam("consignment");

        }
    }
}
using MSharp;

namespace Modules
{
    class NoteList : BaseListModule<Domain.Note>
    {
        public NoteList()
        {
            HeaderText("Notes").DataSource(" await info.Company.Notes.GetList()");

            Column(x => x.DateAndtime);
            Column(x => x.AddedBy).DisplayExpression("@item.AddedBy.Name");
            Column(x => x.Description).LabelText("Note");

            Button("New Note").IsDefault().Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Company.Note.EnterPage>().Send("company", "info.Company.ID").SendReturnUrl());

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
        }
    }
}
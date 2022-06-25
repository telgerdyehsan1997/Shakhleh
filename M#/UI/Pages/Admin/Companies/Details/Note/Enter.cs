using MSharp;

namespace Admin.Company.Note
{
    class EnterPage : SubPage<NotePage>
    {
        public EnterPage()
        {
            Add<Modules.NoteForm>();
        }
    }
}
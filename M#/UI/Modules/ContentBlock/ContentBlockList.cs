using Domain;
using MSharp;

namespace Modules
{
    public class ContentBlockList : ListModule<Domain.ContentBlock>
    {
        public ContentBlockList()
        {
            HeaderText("Content Blocks");

            LinkColumn(x => x.Key)
                .OnClick(x =>
                {
                    x.Go<Admin.Settings.ContentBlocks.EnterPage>()
                        .SendReturnUrl()
                        .Send("item", "item.ID");
                });

            Column(x => x.Content)
                .DisplayExpression(cs("item.Content.OrEmpty().RemoveHtmlTags().Summarize(80)"));

            Button("New Content Block").IsDefault()
                .Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.ContentBlocks.EnterPage>().SendReturnUrl());
        }
    }
}

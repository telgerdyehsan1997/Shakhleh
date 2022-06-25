using MSharp;

namespace Modules
{
    public class PostCodeLookupApiModule : GenericModule
    {
        public PostCodeLookupApiModule()
        {
            OnControllerClassCode("Get addresses").Code(@"
                [HttpPost(""/api/postcodelookup"")]
                public async Task<string> GetAddresses(string postcode)
                {
                    return await new PostCodeLookupService(Database).FindAddresses(postcode);
                }");

        }
    }
}
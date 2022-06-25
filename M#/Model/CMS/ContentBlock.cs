using MSharp;

namespace Domain
{
    public class ContentBlock : EntityType
    {
        public ContentBlock()
        {
            InstanceAccessors("PasswordSuccessfullyReset", "PasswordSuccessfullySet", "LoginIntro");

            DefaultToString = String("Key").Mandatory().Unique();
            BigString("Content").Mandatory();
        }
    }
}

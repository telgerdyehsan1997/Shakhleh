using Olive;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace APIContracts
{
    public class AttachmentContract : IValidatableObject
    {
        public string Name { get; set; }

        public string Attachment { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Name.IsEmpty() && Attachment.HasValue())
            {
                yield return new ValidationResult("Name and Attachment must have value.");
            }
            else if (Name.HasValue() && Attachment.IsEmpty())
            {
                yield return new ValidationResult("Name and Attachment must have value.");
            }

        }
    }

}

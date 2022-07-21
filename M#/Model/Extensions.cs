using MSharp;
using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public static class Extensions
    {
        public static void Archivable(this EntityType entity)
        {
            entity.Implements("IArchivable");

            entity.Bool("Is deactivated")
                  .Mandatory()
                  .TrueText("Archived")
                  .FalseText("Active")
                  .NullText("All");

            entity.String("ArchiveLogIds");

        }
    }
}

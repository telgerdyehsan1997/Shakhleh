using MSharp;

namespace Domain
{
    class Currency : EntityType
    {
        public Currency()
        {
            String("Name").Mandatory().Unique().Max(3).MinLength(3);
            this.Archivable();
           // ToStringExpression("GetListDisplay()");
        }
    }
}
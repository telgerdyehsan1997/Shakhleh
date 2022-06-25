using MSharp;

namespace Domain
{
    class UNCode : EntityType
    {
        public UNCode()
        {
            this.Archivable();
            String("UN no");
            String("Name and description").Max(1000);
            String("Nom et description").Max(1000);
            String("Class");
            String("Classification code");
            String("Packing group");
            String("Labels");
            String("Special provisions");
            String("Limited and excepted quantities 3.4");
            String("Limited and excepted quantities 3.5");
            String("Packing instructions");
            String("Special packing provisions");
            String("Mixed packing provisions");
            String("Instructions");
            String("Tank code");
            String("Vehicle for tank carriage");
            String("Transport category (Tunnel restriction code)");
            String("Packages");
            String("Bulk");
            String("Loading, unloading and handling");
            String("Operation");
            String("Hazard identification No.");

        }
    }
}
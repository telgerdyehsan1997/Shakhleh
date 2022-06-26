namespace Domain
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;

    partial class Settings
    {

        static SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        private static readonly DateTime Date = LocalTime.Now;
        public static string ResourceVersion => Config.Get("App.Resource.Version");


    }
}
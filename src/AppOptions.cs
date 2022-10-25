using System;
namespace trip_guide_generator
{
    public class AppOptions
    {
        public const string Section = "AppSettings";
        public string COSMOS_ENDPOINT { get; set; }
        public string COSMOS_KEY { get; set; }
    }
}


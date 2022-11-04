using System;
namespace ksbnet_api_v1.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string name { get; set; }
        public string thumbnail { get; set; }
        public string description { get; set; }
        public int project_type { get; set; }
        public string slug { get; set; }

    }
}


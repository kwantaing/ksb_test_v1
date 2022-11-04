using System;
namespace ksbnet_api_v1.Models
{
    public class Message
    {
        public int messageID { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
    }
}


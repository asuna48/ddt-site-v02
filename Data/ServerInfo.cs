using System.ComponentModel.DataAnnotations;

namespace ddt_site_v02.Data
{
    public class ServerInfo
    {
        [Key]
        public int Id { get; set; }
        public int ServerID { get; set; }
        public string? ServerName { get; set; }
        public int ServerVersion { get; set; }
        public string? ServerIP { get; set; }
        public string? ServerPort { get; set; }
        public string? FlashURL { get; set; }
        public string? RequestURL { get; set; }
        public string? ResourceURL { get; set; }
        public string? ConfigURL { get; set; }
    }
}
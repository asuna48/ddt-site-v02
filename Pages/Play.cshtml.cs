using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using ddt_site_v02.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace ddt_site_v02.Pages
{
    [Authorize]
    public class PlayModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public PlayModel(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

    public string? FlashUrl { get; set; }
        public string? ConfigUrl { get; set; }
        public string? UserName { get; set; }
        public string Key { get; set; } = Guid.NewGuid().ToString();
        public string FlashEmbedUrl { get; set; } = string.Empty;
    public System.Collections.Generic.List<ServerInfo> Servers { get; set; } = new();

        public void OnGet(int? serverId)
        {
            // Kullanıcı adı
            UserName = User.Identity?.Name;

            // Load servers for UI
            Servers = _db.ServerInfos.ToList();

            // If a serverId was provided use that server, otherwise fallback to first
            ServerInfo? serverInfo = null;
            if (serverId.HasValue)
            {
                serverInfo = _db.ServerInfos.FirstOrDefault(s => s.ServerID == serverId.Value || s.Id == serverId.Value);
            }
            serverInfo ??= _db.ServerInfos.FirstOrDefault();

            FlashUrl = serverInfo?.FlashURL ?? "http://127.0.0.1/Flash_3400_v01/";
            ConfigUrl = serverInfo?.ConfigURL ?? "http://127.0.0.1/config.xml";

            // Flash embed url oluştur
            FlashEmbedUrl = $"{FlashUrl}Loading.swf?user={UserName}&key={Key}&config={ConfigUrl}";
        }
    }
}
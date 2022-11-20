using WebApp.Domain.Identity;

namespace WebApp.Domain;
// generate crud pages
// dotnet aspnet-codegenerator controller -name CesarsController -actions -m Cesar -dc ApplicationDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f


public class Cesar
{
    // Auto-detected primary key
    public int Id { get; set; }  
    
    public int ShiftAmount { get; set; }
    public string PlainText { get; set; } = default!;
    public string CipherText { get; set; } = default!;


    public string AppUserId { get; set; } = default!;
    public AppUser? AppUser { get; set; }
}
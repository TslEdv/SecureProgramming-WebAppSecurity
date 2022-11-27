using WebApp.Domain.Identity;

namespace WebApp.Domain;

public class RSA
{
    public int Id { get; set; }  

    public long PrimeP { get; set; }
    public long PrimeQ { get; set; }
    
    public string PlainText { get; set; } = default!;
    public string CipherText { get; set; } = default!;
    
    public string AppUserId { get; set; } = default!;
    public AppUser? AppUser { get; set; }
}
using WebApp.Domain.Identity;

namespace WebApp.Domain;

public class DH
{
    public int Id { get; set; }  
    
    public long Prime { get; set; }
    public long Base { get; set; }
    public long ValueA { get; set; }
    public long ValueB { get; set; }

    public long CalculationX { get; set; }
    public long CalculationY { get; set; }

    public string AppUserId { get; set; } = default!;
    public AppUser? AppUser { get; set; }
}
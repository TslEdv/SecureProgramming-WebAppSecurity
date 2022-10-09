namespace WebApp.Domain;

public class Cesar
{
    public int Id { get; set; }
    public int ShiftAmount { get; set; }
    public string PlainText { get; set; } = default!;
    public string CipherText { get; set; } = default!;
}
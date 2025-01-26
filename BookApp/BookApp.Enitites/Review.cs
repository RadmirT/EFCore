namespace BookApp.Entities;
public class Review
{
    public int ReviewId { get; private set; }
    public required string VoterName { get; set; }
    public int NumStars { get; set; }
    public string? Comment { get; set; }
    public int BookId { get; private set; }
}

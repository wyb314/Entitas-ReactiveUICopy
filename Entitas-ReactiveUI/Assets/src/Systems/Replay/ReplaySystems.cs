

public class ReplaySystems : Feature
{
    public ReplaySystems(Contexts contexs) : base("ReplaySystems")
    {
        this.Add(new ReplaySystem(contexs));
    }
    
}


public class ListenerSystems : Feature
{
    public ListenerSystems(Contexts contexs) : base("ListenerSystems")
    {
        this.Add(new CleanupConsumtionHistorySystem(contexs));
        this.Add(new NotifyTickListenersSystem(contexs));
        this.Add(new NotifyPauseListenersSystem(contexs));
        this.Add(new NotifyElixirListenersSystem(contexs));
    }
    
}

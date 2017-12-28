

public class ListenerSystems : Feature
{
    public ListenerSystems(Contexts contexts) : base("ListenerSystems")
    {
        this.Add(new CleanupConsumtionHistorySystem(contexts));
        this.Add(new NotifyTickListenersSystem(contexts));
        this.Add(new NotifyPauseListenersSystem(contexts));
        this.Add(new NotifyElixirListenersSystem(contexts));
        this.Add(new AddReactiveUIListenersSystem(contexts));
        this.Add(new RemoveReactiveUIListenersSystem(contexts));
    }
    
}

public class ReactiveUISystems : Feature
{
    public ReactiveUISystems(Contexts contexs)
    {
        this.Add(new ReplaySystem(contexs));
        this.Add(new CleanupConsumtionHistorySystem(contexs));
        this.Add(new NotifyTickListenersSystem(contexs));
        this.Add(new NotifyPauseListenersSystem(contexs));
        this.Add(new NotifyElixirListenersSystem(contexs));

        var logicSystems = new LogicSystems(contexs);
        contexs.game.CreateEntity().AddLogicSystems(logicSystems);
        //contexs.game.SetLogicSystems(logicSystems);
        this.Add(logicSystems);
    }
}
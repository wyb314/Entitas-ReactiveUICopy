

using System.Diagnostics;
using Entitas;public class GameStateSystems : Feature
{

    private Contexts contexts;

    public GameStateSystems(Contexts contexts) : base("GameStateSystem")
    {
        this.contexts = contexts;
        this.Add(new StartGameSystem(contexts,this));
        this.Add(new EndGameSystem(contexts,this));
        this.Add(new AddReactiveUISystem(contexts));
        this.Add(new RemoveReactiveUISystem(contexts));
        this.Add(new TickUpdateSystem());
        this.Add(new ElixirProduceSystem(contexts));
        this.Add(new ElixirConsumeSystem(contexts));
        this.Add(new ElixirConsumePersistSystem(contexts));
    }
    
}
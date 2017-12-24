
using Entitas;

public class LogicSystems : Systems
{
    public LogicSystems(Contexts contexs)
    {
        this.Add(new TickUpdateSystem());
        this.Add(new ElixirProduceSystem(contexs));
        this.Add(new ElixirConsumeSystem(contexs));
        this.Add(new ElixirConsumePersistSystem(contexs));
        this.Add(new ElixirConsumeCleanupSystem(contexs));
    }
}

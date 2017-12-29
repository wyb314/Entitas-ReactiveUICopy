using System;
using System.Collections.Generic;
using Entitas;

public class ElixirProduceSystem : ReactiveSystem<GameEntity>, IInitializeSystem, ICleanupSystem, ITearDownSystem
{
    int count = 0;

    // This should be inside of a config file
    public const float ElixirCapacity = 10f;
    const int ProductionFrequency = 3;
    const float ProductionStep = 0.01f;

    public ElixirProduceSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Tick.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (count == 0)
        {

            var newAmount = Math.Min(ElixirCapacity, Contexts.sharedInstance.game.elixir.amount + ProductionStep);
            Contexts.sharedInstance.game.ReplaceElixir(newAmount);
            //_pool.ReplaceElixir(newAmount);
        }
        count = ((count + 1) % ProductionFrequency);
    }

    //public TriggerOnEvent trigger { get { return Matcher.Tick.OnEntityAdded(); } }

    public void Initialize()
    {
        
    }

    public void Cleanup()
    {
    }

    public void TearDown()
    {
        
    }
}
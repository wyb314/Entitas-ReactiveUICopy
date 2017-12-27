using System;
using System.Collections.Generic;
using Entitas;

public class ElixirConsumePersistSystem : ReactiveSystem<InputEntity>,ICleanupSystem, ITearDownSystem
{

    public ElixirConsumePersistSystem(Contexts contexts) : base(contexts.input)
    {
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Consume.Added());
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        if (Contexts.sharedInstance.input.isPause)
        {
            return;
        }
        var previousEntries = Contexts.sharedInstance.game.hasConsumtionHistory ? Contexts.sharedInstance.game.consumtionHistory.entires : new List<ConsumptionEntry>();
        foreach (var entity in entities)
        {
            previousEntries.Add(new ConsumptionEntry(Contexts.sharedInstance.game.tick.currentTick, entity.consume.amount));

        }
        Contexts.sharedInstance.game.ReplaceConsumtionHistory(previousEntries);
    }

    public void Cleanup()
    {
    }

    public void TearDown()
    {
    }
}

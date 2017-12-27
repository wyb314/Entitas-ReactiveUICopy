using System;
using System.Collections.Generic;
using Entitas;

public class CleanupConsumtionHistorySystem : ReactiveSystem<InputEntity>
{
    public CleanupConsumtionHistorySystem(Contexts contexts) : base(contexts.input)
    {
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Pause.Removed());
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var actions = Contexts.sharedInstance.game.hasConsumtionHistory ? Contexts.sharedInstance.game.consumtionHistory.entires : new List<ConsumptionEntry>();
        int count = 0;
        for (int index = actions.Count - 1; index >= 0; index--)
        {
            if (actions[index].tick > Contexts.sharedInstance.game.tick.currentTick)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        actions.RemoveRange(actions.Count - count, count);
    }
    
}

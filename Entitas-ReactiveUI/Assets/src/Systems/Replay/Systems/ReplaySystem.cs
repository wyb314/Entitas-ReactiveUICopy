using System;
using System.Collections.Generic;
using System.Diagnostics;
using Entitas;

public class ReplaySystem : ReactiveSystem<GameEntity>
{

    public ReplaySystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.JumpInTime.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (!Contexts.sharedInstance.input.isPause)
        {
            return;
        }

        var logicSystems = Contexts.sharedInstance.game.logicSystems.systems;
        logicSystems.Initialize();
        var actions = Contexts.sharedInstance.game.hasConsumtionHistory ? Contexts.sharedInstance.game.consumtionHistory.entires : new List<ConsumptionEntry>();
        var actionIndex = 0;
        for (int tick = 0; tick <= Contexts.sharedInstance.game.jumpInTime.targetTick; tick++)
        {
            Contexts.sharedInstance.game.ReplaceTick(tick);
            if (actions.Count > actionIndex && actions[actionIndex].tick == tick)
            {
                Contexts.sharedInstance.input.CreateEntity().AddConsume(actions[actionIndex].amount);
                actionIndex++;
            }
            logicSystems.Execute();
        }
    }

    //public void Execute(List<Entity> entities)
    //{
    //    var logicSystems = _pool.logicSystems.systems;
    //    logicSystems.Initialize();
    //    var actions = _pool.hasConsumtionHistory ? _pool.consumtionHistory.entires : new List<ConsumptionEntry>();
    //    var actionIndex = 0;
    //    for (int tick = 0; tick <= _pool.jumpInTime.targetTick; tick++)
    //    {
    //        _pool.ReplaceTick(tick);
    //        if (actions.Count > actionIndex && actions[actionIndex].tick == tick)
    //        {
    //            _pool.CreateEntity().AddConsume(actions[actionIndex].amount);
    //            actionIndex++;
    //        }
    //        logicSystems.Execute();
    //    }
    //}

    //public TriggerOnEvent trigger { get { return Matcher.JumpInTime.OnEntityAdded(); } }

    //public void SetPool(Pool pool) { _pool = pool; }

    //public IMatcher ensureComponents { get { return Matcher.JumpInTime; } }
}
using System;
using System.Collections.Generic;
using Entitas;

public class TickUpdateSystem : IInitializeSystem, IExecuteSystem
{
    public void Initialize()
    {
        Contexts.sharedInstance.game.ReplaceTick(0);
        //_pool.ReplaceTick(0);
    }

    public void Execute()
    {
        if (!Contexts.sharedInstance.game.isPause)
        {
            long currentTick = Contexts.sharedInstance.game.tick.currentTick;
            Contexts.sharedInstance.game.ReplaceTick(currentTick + 1);
            //_pool.ReplaceTick(_pool.tick.currentTick + 1);
        }
    }
}


public class ElixirProduceSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    int count = 0;

    // This should be inside of a config file
    public const float ElixirCapacity = 10f;
    const int ProductionFrequency = 3;
    const float ProductionStep = 0.01f;

    public ElixirProduceSystem(Contexts contexts):base(contexts.game)
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
        Contexts.sharedInstance.game.ReplaceElixir(0);
    }
    
}

public class ElixirConsumeSystem : ReactiveSystem<GameEntity>
{

    public ElixirConsumeSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Consume.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {

            if (entity.consume.amount > Contexts.sharedInstance.game.elixir.amount)
            {
                UnityEngine.Debug.LogError("Consume more than produced. Should not happen");
            }
            var newAmount = Math.Max(0, Contexts.sharedInstance.game.elixir.amount - entity.consume.amount);
            Contexts.sharedInstance.game.ReplaceElixir(newAmount);
        }
    }

    //public void Execute(List<Entity> entities)
    //{
    //    foreach (var entity in entities)
    //    {
    //        if (entity.consume.amount > _pool.elixir.amount)
    //        {
    //            UnityEngine.Debug.LogError("Consume more than produced. Should not happen");
    //        }
    //        var newAmount = Math.Max(0, _pool.elixir.amount - entity.consume.amount);
    //        _pool.ReplaceElixir(newAmount);
    //    }
    //}

    //public TriggerOnEvent trigger { get { return Matcher.Consume.OnEntityAdded(); } }

    //public void SetPool(Pool pool) { _pool = pool; }

    //public IMatcher ensureComponents { get { return Matcher.Consume; } }
}

public class ElixirConsumePersistSystem : ReactiveSystem<GameEntity>
{

    public ElixirConsumePersistSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Consume.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (Contexts.sharedInstance.game.isPause)
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

    //public void Execute(List<Entity> entities)
    //{
    //    if (_pool.isPause)
    //    {
    //        return;
    //    }
    //    var previousEntries = _pool.hasConsumtionHistory ? _pool.consumtionHistory.entires : new List<ConsumptionEntry>();
    //    foreach (var entity in entities)
    //    {
    //        previousEntries.Add(new ConsumptionEntry(_pool.tick.currentTick, entity.consume.amount));

    //    }
    //    _pool.ReplaceConsumtionHistory(previousEntries);
    //}

    //public TriggerOnEvent trigger { get { return Matcher.Consume.OnEntityAdded(); } }

    //public void SetPool(Pool pool) { _pool = pool; }

    //public IMatcher ensureComponents { get { return Matcher.Consume; } }
}

public class ElixirConsumeCleanupSystem : ReactiveSystem<GameEntity>
{
    public ElixirConsumeCleanupSystem(Contexts contexts) : base(contexts.game)
    {
    }


    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Consume.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            //Contexts.sharedInstance.game
            entity.Destroy();
            //_pool.DestroyEntity(entity);
        }
    }

    //public void Execute(List<Entity> entities)
    //{
    //    foreach (var entity in entities)
    //    {
    //        _pool.DestroyEntity(entity);
    //    }
    //}

    //public TriggerOnEvent trigger { get { return Matcher.Consume.OnEntityAdded(); } }

    //public void SetPool(Pool pool) { _pool = pool; }

    //public IMatcher ensureComponents { get { return Matcher.Consume; } }
}

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
        var logicSystems = Contexts.sharedInstance.game.logicSystems.systems;
        logicSystems.Initialize();
        var actions = Contexts.sharedInstance.game.hasConsumtionHistory ? Contexts.sharedInstance.game.consumtionHistory.entires : new List<ConsumptionEntry>();
        var actionIndex = 0;
        for (int tick = 0; tick <= Contexts.sharedInstance.game.jumpInTime.targetTick; tick++)
        {
            Contexts.sharedInstance.game.ReplaceTick(tick);
            if (actions.Count > actionIndex && actions[actionIndex].tick == tick)
            {
                Contexts.sharedInstance.game.CreateEntity().AddConsume(actions[actionIndex].amount);
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

public class CleanupConsumtionHistorySystem : ReactiveSystem<GameEntity>
{
    public CleanupConsumtionHistorySystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Pause.Removed());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
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

    //public void Execute(List<Entity> entities)
    //{
    //    var actions = _pool.hasConsumtionHistory ? _pool.consumtionHistory.entires : new List<ConsumptionEntry>();
    //    int count = 0;
    //    for (int index = actions.Count - 1; index >= 0; index--)
    //    {
    //        if (actions[index].tick > _pool.tick.currentTick)
    //        {
    //            count++;
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    actions.RemoveRange(actions.Count - count, count);
    //}

    //public TriggerOnEvent trigger { get { return Matcher.Pause.OnEntityRemoved(); } }

    //public void SetPool(Pool pool) { _pool = pool; }

    //public IMatcher excludeComponents { get { return Matcher.Pause; } }
}

public class NotifyTickListenersSystem : ReactiveSystem<GameEntity>
{


    IGroup<GameEntity> listeners;

    public NotifyTickListenersSystem(Contexts contexts) : base(contexts.game)
    {
        this.listeners = contexts.game.GetGroup(GameMatcher.TickListener);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TickListener.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in listeners.GetEntities())
        {
            entity.tickListener.listener.TickChanged();
        }
    }

    //public void Execute(List<Entity> entities)
    //{
    //    foreach (var entity in listeners.GetEntities())
    //    {
    //        entity.tickListener.listener.TickChanged();
    //    }
    //}

    //public TriggerOnEvent trigger { get { return Matcher.Tick.OnEntityAddedOrRemoved(); } }

    //public void SetPool(Pool pool)
    //{
    //    _pool = pool;
    //    listeners = Contexts.sharedInstance.game.GetGroup();
    //    listeners = _pool.GetGroup(Matcher.TickListener);
    //}
}

public class NotifyPauseListenersSystem : ReactiveSystem<GameEntity>
{
    //Pool _pool;
    IGroup<GameEntity> listeners;

    public NotifyPauseListenersSystem(Contexts contexts) : base(contexts.game)
    {
        this.listeners = contexts.game.GetGroup(GameMatcher.PauseListener);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Pause.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in listeners.GetEntities())
        {
            entity.pauseListener.listener.PauseStateChanged();
        }
    }

    //public void Execute(List<Entity> entities)
    //{
    //    foreach (var entity in listeners.GetEntities())
    //    {
    //        entity.pauseListener.listener.PauseStateChanged();
    //    }
    //}

    //public TriggerOnEvent trigger { get { return Matcher.Pause.OnEntityAddedOrRemoved(); } }

    //public void SetPool(Pool pool)
    //{
    //    _pool = pool;
    //    listeners = =
    //    listeners = _pool.GetGroup(Matcher.PauseListener);
    //}
}

public class NotifyElixirListenersSystem : ReactiveSystem<GameEntity>
{
    //Pool _pool;
    IGroup<GameEntity> listeners;

    public NotifyElixirListenersSystem(Contexts contexts) : base(contexts.game)
    {
        this.listeners = contexts.game.GetGroup(GameMatcher.ElixirListener);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Elixir.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in listeners.GetEntities())
        {
            entity.elixirListener.listener.ElixirAmountChanged();
        }
    }

    //public void Execute(List<Entity> entities)
    //{
    //    foreach (var entity in listeners.GetEntities())
    //    {
    //        entity.elixirListener.listener.ElixirAmountChanged();
    //    }
    //}

    //public TriggerOnEvent trigger { get { return Matcher.Elixir.OnEntityAddedOrRemoved(); } }

    //public void SetPool(Pool pool)
    //{
    //    _pool = pool;
    //    listeners = _pool.GetGroup(Matcher.ElixirListener);
    //}
}
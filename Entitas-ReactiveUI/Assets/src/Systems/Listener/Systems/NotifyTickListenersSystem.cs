using System;
using System.Collections.Generic;
using Entitas;



public class NotifyTickListenersSystem : ReactiveSystem<GameEntity>,ICleanupSystem,ITearDownSystem
{


    IGroup<GameEntity> listeners;

    public NotifyTickListenersSystem(Contexts contexts) : base(contexts.game)
    {
        this.listeners = contexts.game.GetGroup(GameMatcher.TickListener);
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
        foreach (var entity in listeners.GetEntities())
        {
            entity.tickListener.listener.TickChanged();
        }
    }

    public void Cleanup()
    {

    }

    public void TearDown()
    {
        foreach (var entity in listeners.GetEntities())
        {
            entity.Destroy();
        }
    }

}
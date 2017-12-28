using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;




public class NotifyElixirListenersSystem : ReactiveSystem<GameEntity>, ITearDownSystem
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
            entity.elixirListener.listener();
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

    public void TearDown()
    {
        foreach (var entity in listeners.GetEntities())
        {
            entity.Destroy();
        }
        this.listeners = null;
    }
}

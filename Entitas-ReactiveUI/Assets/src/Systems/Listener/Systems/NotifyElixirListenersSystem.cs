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
        return context.CreateCollector(GameMatcher.Elixir.Added());
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
    
    public void TearDown()
    {
        foreach (var entity in listeners.GetEntities())
        {
            entity.Destroy();
        }
        this.listeners = null;
    }
}

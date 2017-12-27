using System;
using System.Collections.Generic;
using Entitas;


public class NotifyPauseListenersSystem : ReactiveSystem<InputEntity>, ITearDownSystem
{
    //Pool _pool;
    IGroup<InputEntity> listeners;

    public NotifyPauseListenersSystem(Contexts contexts) : base(contexts.input)
    {
        this.listeners = contexts.input.GetGroup(InputMatcher.PauseListener);
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Pause.AddedOrRemoved());
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var entity in listeners.GetEntities())
        {
            entity.pauseListener.listener.PauseStateChanged();
        }
    }

    public void TearDown()
    {
        foreach (var entity in listeners.GetEntities())
        {
            entity.Destroy();
        }
    }

}

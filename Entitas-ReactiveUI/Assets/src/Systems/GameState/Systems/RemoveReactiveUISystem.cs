using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Entitas;
using UnityEngine;

public class RemoveReactiveUISystem : ReactiveSystem<InputEntity>,ICleanupSystem
{
    private Contexts _contexts;
    IGroup<InputEntity> _group;
    public RemoveReactiveUISystem(Contexts contexts) : base(contexts.input)
    {
        _contexts = contexts;
        this._group = this._contexts.input.GetGroup(InputMatcher.DestroyReactiveUI);
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.DestroyReactiveUI.Added());
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.isDestroyReactiveUI;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        var reactiveUIEntity = this._contexts.game.reactiveUIEntity;

        if (reactiveUIEntity != null)
        {
            GameObject.Destroy(reactiveUIEntity.reactiveUI.uiContent);
            reactiveUIEntity.Destroy();
        }

        this._contexts.input.isPause = false;
    }
    

    public void Cleanup()
    {
        foreach (var entity in _group.GetEntities())
        {
            entity.Destroy();
        }
    }
}
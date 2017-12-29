using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Entitas;
using UnityEngine;

public class RemoveReactiveUISystem : ReactiveSystem<InputEntity>,ICleanupSystem
{
    private Contexts _contexts;
    public RemoveReactiveUISystem(Contexts contexts) : base(contexts.input)
    {
        _contexts = contexts;
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
        
    }
    

    public void Cleanup()
    {
        
    }
}
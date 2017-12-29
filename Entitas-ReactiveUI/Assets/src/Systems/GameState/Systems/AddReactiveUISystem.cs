using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddReactiveUISystem : ReactiveSystem<InputEntity>,ICleanupSystem
{
  
    private Contexts _contexts;
    //IGroup<InputEntity> _group;

    public AddReactiveUISystem(Contexts contexts) : base(contexts.input)
    {
        this._contexts = contexts;
        //this._group = this._contexts.input.GetGroup(InputMatcher.AddReactiveUI);
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AddReactiveUI.Added());
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.isAddReactiveUI;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        GameObject pfb = Resources.Load<GameObject>("ReactiveUI");
        GameObject elixirUIGo = GameObject.Instantiate(pfb, Vector3.zero, Quaternion.identity);

        this._contexts.game.ReplaceReactiveUI(elixirUIGo);
        
    }

    public void Cleanup()
    {
        //IGroup<InputEntity> group = this._contexts.input.GetGroup(InputMatcher.AnyOf(InputMatcher.AddReactiveUI));
        //foreach (var entity in _group.GetEntities())
        //{
        //    entity.Destroy();
        //}
    }
}
using System;
using System.Collections.Generic;
using Entitas;

public class ElixirConsumeSystem : ReactiveSystem<InputEntity>, ICleanupSystem, ITearDownSystem
{

    IGroup<InputEntity> consumeGroups;

    public ElixirConsumeSystem(Contexts contexts) : base(contexts.input)
    {
        this.consumeGroups = contexts.input.GetGroup(InputMatcher.Consume);
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Consume.Added());
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
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

    public void Cleanup()
    {
        foreach (var entity in this.consumeGroups.GetEntities())
        {
            //Contexts.sharedInstance.game
            entity.Destroy();
            //_pool.DestroyEntity(entity);
        }
    }

    public void TearDown()
    {
    }
}

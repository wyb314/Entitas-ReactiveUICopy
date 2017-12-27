using System;
using System.Collections.Generic;
using Entitas;


public class ElixirConsumeCleanupSystem : ReactiveSystem<InputEntity>,ICleanupSystem, ITearDownSystem
{
    public ElixirConsumeCleanupSystem(Contexts contexts) : base(contexts.input)
    {
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
            //Contexts.sharedInstance.game
            entity.Destroy();
            //_pool.DestroyEntity(entity);
        }
    }

    public void Cleanup()
    {
    }

    public void TearDown()
    {
    }
}

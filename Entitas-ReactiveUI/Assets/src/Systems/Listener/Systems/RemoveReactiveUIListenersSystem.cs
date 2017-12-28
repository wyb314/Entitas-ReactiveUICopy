

using System.Collections.Generic;
using Entitas;
using UnityEngine.Playables;

public class RemoveReactiveUIListenersSystem : ReactiveSystem<GameEntity>,ICleanupSystem
{
    private Contexts _contexts;
    public RemoveReactiveUIListenersSystem(Contexts contexts) : base(contexts.game)
    {
        this._contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ReactiveUI.Removed());
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.hasReactiveUI;
    }

    protected override void Execute(List<GameEntity> entities)
    {
       
        IGroup<GameEntity> gameEntityGroup = this._contexts.game.GetGroup(GameMatcher.AnyOf(GameMatcher.ElixirListener
            , GameMatcher.TickListener));
        foreach (var entity in gameEntityGroup.GetEntities())
        {
            entity.Destroy();
        }

        IGroup<InputEntity> inputEntityGroup =
            this._contexts.input.GetGroup(InputMatcher.AllOf(InputMatcher.PauseListener));
        foreach (var entity in inputEntityGroup.GetEntities())
        {
            entity.Destroy();
        }

    }

    public void Cleanup()
    {
    }

}


using System.Collections.Generic;
using Entitas;
using UnityEngine.Playables;

public class RemoveReactiveUIListenersSystem : ReactiveSystem<GameEntity>,ICleanupSystem
{
    private Contexts _contexts;
    private IGroup<GameEntity> gameEntityGroup;
    private IGroup<InputEntity> inputEntityGroup;
    public RemoveReactiveUIListenersSystem(Contexts contexts) : base(contexts.game)
    {
        this._contexts = contexts;
        gameEntityGroup = this._contexts.game.GetGroup(GameMatcher.AnyOf(GameMatcher.ElixirListener
            , GameMatcher.TickListener));
        inputEntityGroup =
            this._contexts.input.GetGroup(InputMatcher.AllOf(InputMatcher.PauseListener));

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
        foreach (var entity in gameEntityGroup.GetEntities())
        {
            entity.Destroy();
        }
        foreach (var entity in inputEntityGroup.GetEntities())
        {
            entity.Destroy();
        }

    }

    public void Cleanup()
    {
    }

}
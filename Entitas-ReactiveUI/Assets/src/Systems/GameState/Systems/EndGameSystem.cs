using System.Collections.Generic;
using System.Net.NetworkInformation;
using Entitas;

public class EndGameSystem : ReactiveSystem<InputEntity>, ICleanupSystem , ITearDownSystem
{
    private Systems _systems;
    private Contexts _contexts;
    private IGroup<InputEntity> group;
    public EndGameSystem(Contexts contexts, Systems systems) : base(contexts.input)
    {
        this._contexts = contexts;
        this._systems = systems;
        group = this._contexts.input.GetGroup(InputMatcher.AnyOf(InputMatcher.EndGame, InputMatcher.DestroyReactiveUI));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.EndGame);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.isEndGame;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        this._contexts.input.isStartProduceElixir = false;
        
        this._contexts.input.CreateEntity().isDestroyReactiveUI = true;
        
        this._contexts.game.RemoveTick();
        this._contexts.game.RemoveElixir();
        this._contexts.game.RemoveJumpInTime();
        this._contexts.game.RemoveLogicSystems();
    }

    public void Cleanup()
    {
        foreach (var entity in group.GetEntities())
        {
            entity.Destroy();
        }
    }

    public void TearDown()
    {
        this._contexts.input.isStartProduceElixir = false;

        if (this._contexts.game.hasReactiveUI)
        {
            this._contexts.game.RemoveReactiveUI();
        }

        if (this._contexts.game.hasTick)
        {
            this._contexts.game.RemoveTick();
        }

        if (this._contexts.game.hasElixir)
        {
            this._contexts.game.RemoveElixir();
        }

        if (this._contexts.game.hasJumpInTime)
        {
            this._contexts.game.RemoveJumpInTime();
        }

        if (this._contexts.game.hasLogicSystems)
        {
            this._contexts.game.RemoveLogicSystems();
        }

        
    }
}
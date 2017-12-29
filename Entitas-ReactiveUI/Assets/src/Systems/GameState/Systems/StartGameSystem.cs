
using System.Collections.Generic;
using Entitas;
using JetBrains.Annotations;

public class StartGameSystem : ReactiveSystem<InputEntity>, ICleanupSystem
{

    private Systems _systems;
    private Contexts _contexts;
    private IGroup<InputEntity> group;
    public StartGameSystem(Contexts contexts , Systems systems) : base(contexts.input)
    {
        this._contexts = contexts;
        this._systems = systems;
        group = this._contexts.input.GetGroup(InputMatcher.AnyOf(InputMatcher.AddReactiveUI, InputMatcher.StartGame));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.StartGame);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.isStartGame;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        this.StartGame();
    }


    private void StartGame()
    {
        this._contexts.game.ReplaceTick(0);
        this._contexts.game.ReplaceElixir(0);
        this._contexts.game.ReplaceLogicSystems(this._systems);
        
        this._contexts.input.CreateEntity().isAddReactiveUI = true;
        this._contexts.input.isStartProduceElixir = true;
        this._contexts.input.isPause = false;

        var logicSystems = Contexts.sharedInstance.game.logicSystems.systems;
        logicSystems.Initialize();


    }

    public void Cleanup()
    {
        foreach (var entity in group.GetEntities())
        {
            entity.Destroy();
        }
        
    }
}
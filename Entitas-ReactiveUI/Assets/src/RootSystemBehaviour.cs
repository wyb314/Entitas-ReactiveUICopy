using System.Threading;
using UnityEngine;
using Entitas;

public class RootSystemBehaviour : MonoBehaviour
{
    Systems _systems;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        var contexts = Contexts.sharedInstance;
        
        this._systems = new Feature("Systems")
            .Add(new InitSystem(contexts))
            .Add(new ReplaySystems(contexts))
            .Add(new GameStateSystems(contexts))
            .Add(new ListenerSystems(contexts))
            ;
        
        this._systems.Initialize();
    }
    

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    void OnDestroy()
    {
        this._systems.TearDown();
        _systems.ClearReactiveSystems();
        Contexts.sharedInstance.game.DestroyAllEntities();
        Contexts.sharedInstance.input.DestroyAllEntities();

       
    }
}

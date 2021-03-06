//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity consumtionHistoryEntity { get { return GetGroup(GameMatcher.ConsumtionHistory).GetSingleEntity(); } }
    public ConsumtionHistoryComponent consumtionHistory { get { return consumtionHistoryEntity.consumtionHistory; } }
    public bool hasConsumtionHistory { get { return consumtionHistoryEntity != null; } }

    public GameEntity SetConsumtionHistory(System.Collections.Generic.List<ConsumptionEntry> newEntires) {
        if (hasConsumtionHistory) {
            throw new Entitas.EntitasException("Could not set ConsumtionHistory!\n" + this + " already has an entity with ConsumtionHistoryComponent!",
                "You should check if the context already has a consumtionHistoryEntity before setting it or use context.ReplaceConsumtionHistory().");
        }
        var entity = CreateEntity();
        entity.AddConsumtionHistory(newEntires);
        return entity;
    }

    public void ReplaceConsumtionHistory(System.Collections.Generic.List<ConsumptionEntry> newEntires) {
        var entity = consumtionHistoryEntity;
        if (entity == null) {
            entity = SetConsumtionHistory(newEntires);
        } else {
            entity.ReplaceConsumtionHistory(newEntires);
        }
    }

    public void RemoveConsumtionHistory() {
        consumtionHistoryEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ConsumtionHistoryComponent consumtionHistory { get { return (ConsumtionHistoryComponent)GetComponent(GameComponentsLookup.ConsumtionHistory); } }
    public bool hasConsumtionHistory { get { return HasComponent(GameComponentsLookup.ConsumtionHistory); } }

    public void AddConsumtionHistory(System.Collections.Generic.List<ConsumptionEntry> newEntires) {
        var index = GameComponentsLookup.ConsumtionHistory;
        var component = CreateComponent<ConsumtionHistoryComponent>(index);
        component.entires = newEntires;
        AddComponent(index, component);
    }

    public void ReplaceConsumtionHistory(System.Collections.Generic.List<ConsumptionEntry> newEntires) {
        var index = GameComponentsLookup.ConsumtionHistory;
        var component = CreateComponent<ConsumtionHistoryComponent>(index);
        component.entires = newEntires;
        ReplaceComponent(index, component);
    }

    public void RemoveConsumtionHistory() {
        RemoveComponent(GameComponentsLookup.ConsumtionHistory);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherConsumtionHistory;

    public static Entitas.IMatcher<GameEntity> ConsumtionHistory {
        get {
            if (_matcherConsumtionHistory == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ConsumtionHistory);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherConsumtionHistory = matcher;
            }

            return _matcherConsumtionHistory;
        }
    }
}

using Entitas;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;

[Unique]
[Input]
public class PauseComponent : IComponent { }

[Input]
public class ConsumeComponent : IComponent
{
    public int amount;
}

public interface IPauseListener
{
    void PauseStateChanged();
}

public delegate void PauseStateChangedDelegate();

[Input]
public class PauseListenerComponent : IComponent
{
    public PauseStateChangedDelegate listener;
}

[Input]
public class AddReactiveUIComponent : IComponent
{
}

[Input]
public class StartGame : IComponent
{
    public bool isStartGame;
}

[Unique]
[Input]
public class StartProduceElixir : IComponent
{

}

[Input]
public class DestroyReactiveUIComponent : IComponent
{
}

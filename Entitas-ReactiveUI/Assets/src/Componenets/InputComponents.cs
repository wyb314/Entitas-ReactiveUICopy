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

[Input]
public class PauseListenerComponent : IComponent
{
    public IPauseListener listener;
}
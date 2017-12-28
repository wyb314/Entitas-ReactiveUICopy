using Entitas;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Unique]
[Game]
public class TickComponent : IComponent
{
  public long currentTick;
}

[Unique]
[Game]
public class JumpInTimeComponent : IComponent
{
	public long targetTick;
}


[Unique]
[Game]
public class ElixirComponent : IComponent
{
  public float amount;
}




public class ConsumptionEntry
{
	public ConsumptionEntry(long tick, int amount)
	{
		this.tick = tick;
		this.amount = amount;
	}
	public readonly long tick;
	public readonly int amount;
}

[Unique]
[Game]
public class ConsumtionHistoryComponent : IComponent
{
	public List<ConsumptionEntry> entires;
}

[Unique]
[Game]
public class LogicSystemsComponent : IComponent
{
	public Systems systems;
}

[Unique]
[Game]
public class ReactiveUIComponent : IComponent
{
    public GameObject uiContent;
}


public delegate void TickChangedDelegate();

[Game]
public class TickListenerComponent : IComponent {
	public TickChangedDelegate listener;
}

public delegate void ElixirAmountChangedDelegate();
[Game]
public class ElixirListenerComponent : IComponent {
	public ElixirAmountChangedDelegate listener;
}

public interface ITickListener
{
    void TickChanged();
}

public interface IElixirListener
{
    void ElixirAmountChanged();
}
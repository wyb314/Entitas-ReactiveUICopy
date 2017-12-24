using Entitas;
using System.Collections.Generic;
using Entitas.CodeGeneration.Attributes;

[Unique]
public class TickComponent : IComponent
{
  public long currentTick;
}

[Unique]
public class JumpInTimeComponent : IComponent
{
	public long targetTick;
}


[Unique]
public class ElixirComponent : IComponent
{
  public float amount;
}

[Unique]
public class PauseComponent : IComponent {}

public class ConsumeComponent : IComponent
{
  public int amount;
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
public class ConsumtionHistoryComponent : IComponent
{
	public List<ConsumptionEntry> entires;
}

[Unique]
public class LogicSystemsComponent : IComponent
{
	public Systems systems;
}


public interface ITickListener {
	void TickChanged();
}

public class TickListenerComponent : IComponent {
	public ITickListener listener;
}

public interface IPauseListener {
	void PauseStateChanged();
}

public class PauseListenerComponent : IComponent {
	public IPauseListener listener;
}

public interface IElixirListener {
	void ElixirAmountChanged();
}

public class ElixirListenerComponent : IComponent {
	public IElixirListener listener;
}

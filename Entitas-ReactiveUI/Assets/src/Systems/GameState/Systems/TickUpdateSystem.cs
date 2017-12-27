using System;
using System.Collections.Generic;
using Entitas;

public class TickUpdateSystem : IInitializeSystem, IExecuteSystem,ICleanupSystem, ITearDownSystem
{
    public void Initialize()
    {
        Contexts.sharedInstance.game.ReplaceTick(0);
        //_pool.ReplaceTick(0);
    }

    public void Execute()
    {
        if (!Contexts.sharedInstance.input.isPause)
        {
            long currentTick = Contexts.sharedInstance.game.tick.currentTick;
            Contexts.sharedInstance.game.ReplaceTick(currentTick + 1);
            //_pool.ReplaceTick(_pool.tick.currentTick + 1);
        }
    }

    public void Cleanup()
    {
    }

    public void TearDown()
    {
    }

}

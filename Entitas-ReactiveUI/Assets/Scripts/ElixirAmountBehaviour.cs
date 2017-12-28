using System;
using UnityEngine;
using UnityEngine.UI;

public class ElixirAmountBehaviour : MonoBehaviour, IElixirListener {

	void Start () {
        //Contexts.sharedInstance.game.CreateEntity().AddElixirListener(this);
        //Pools.pool.CreateEntity().AddElixirListener(this);
	}

	public void ElixirAmountChanged ()
	{
        var label = GetComponent<Text>();
        label.text = ((int)Contexts.sharedInstance.game.elixir.amount).ToString();
        label.color = System.Math.Abs(Contexts.sharedInstance.game.elixir.amount - ElixirProduceSystem.ElixirCapacity) < Mathf.Epsilon ? Color.red : Color.black;
    }

}

using UnityEngine;

public class ElixirBarBehaviour : MonoBehaviour, IElixirListener {

	void Start () {
        Contexts.sharedInstance.game.CreateEntity().AddElixirListener(this);
        //Pools.pool.CreateEntity().AddElixirListener(this);
	}

	public void ElixirAmountChanged ()
	{
        var ratio = Contexts.sharedInstance.game.elixir.amount / ElixirProduceSystem.ElixirCapacity;
        GetComponent<RectTransform>().localScale = new Vector3(ratio, 1f, 1f);
    }

}

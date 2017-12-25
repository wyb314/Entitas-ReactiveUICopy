using UnityEngine;
using UnityEngine.UI;

public class ConsumeButtonBehaviour : MonoBehaviour, IPauseListener, IElixirListener {

	public Text label;
	public RectTransform progressBox;
	public int consumptionAmmount;

	float maxHeight;

	void Awake()
	{
		maxHeight = progressBox.rect.height;
		label.text = consumptionAmmount.ToString();

	    GameEntity entity = Contexts.sharedInstance.game.CreateEntity();
        entity.AddPauseListener(this);
        entity.AddElixirListener(this);
        
        
        //Contexts.sharedInstance.game.DestroyAllEntities();
        //entity = Contexts.sharedInstance.game.CreateEntity();
        //entity.AddPauseListener(this);
        //Pools.pool.CreateEntity().AddPauseListener(this).AddElixirListener(this);
    }

	public void PauseStateChanged ()
	{
        //UnityEngine.Debug.LogError("PauseStateChanged is invoke! hashCode->"+this.GetHashCode());
		GetComponent<Button>().enabled = !Contexts.sharedInstance.game.isPause;
	}

	public void ElixirAmountChanged ()
	{
        var ratio = 1 - Mathf.Min(1f, (Contexts.sharedInstance.game.elixir.amount / (float)consumptionAmmount));
        progressBox.sizeDelta = new Vector2(progressBox.rect.width, maxHeight * ratio);
        GetComponent<Button>().enabled = (System.Math.Abs(ratio - 0) < Mathf.Epsilon);
    }

    public void ButtonPressed()
    {
        if (Contexts.sharedInstance.game.isPause) return;
        Contexts.sharedInstance.game.CreateEntity().AddConsume(consumptionAmmount);
    }
}

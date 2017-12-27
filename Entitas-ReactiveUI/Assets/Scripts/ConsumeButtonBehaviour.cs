using UnityEngine;
using UnityEngine.UI;

public class ConsumeButtonBehaviour : MonoBehaviour, IPauseListener, IElixirListener {

	public Text label;
	public RectTransform progressBox;
	public int consumptionAmmount;

	float maxHeight;

    //private InputEntity inputEntity; 

	void Awake()
	{
		maxHeight = progressBox.rect.height;
		label.text = consumptionAmmount.ToString();

	    var inputEntity = Contexts.sharedInstance.input.pauseEntity;

	    if (inputEntity == null)
	    {

	        //Contexts.sharedInstance.input.CreateEntity().isPause = true;

         //   inputEntity = Contexts.sharedInstance.input.pauseEntity;
        }
        //inputEntity.AddPauseListener(this);


	    //this.inputEntity = Contexts.sharedInstance.input.CreateEntity();
     //   this.inputEntity.AddPauseListener(this);
     //   this.inputEntity.Retain(this);

	    var gameEntity = Contexts.sharedInstance.game.elixirEntity;
	    if (gameEntity == null)
	    {
	        gameEntity = Contexts.sharedInstance.game.CreateEntity();
	        gameEntity.AddElixirListener(this);
	    }
	    else
	    {
            gameEntity.AddElixirListener(this);
        }
	    
        //GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
        //gameEntity.AddElixirListener(this);


        //Contexts.sharedInstance.game.DestroyAllEntities();
        //entity = Contexts.sharedInstance.game.CreateEntity();
        //entity.AddPauseListener(this);
        //Pools.pool.CreateEntity().AddPauseListener(this).AddElixirListener(this);
    }

	public void PauseStateChanged ()
	{
        //UnityEngine.Debug.LogError("PauseStateChanged is invoke! hashCode->"+this.GetHashCode());
		GetComponent<Button>().enabled = !Contexts.sharedInstance.input.isPause;
	}

	public void ElixirAmountChanged ()
	{
        var ratio = 1 - Mathf.Min(1f, (Contexts.sharedInstance.game.elixir.amount / (float)consumptionAmmount));
        progressBox.sizeDelta = new Vector2(progressBox.rect.width, maxHeight * ratio);
        GetComponent<Button>().enabled = (System.Math.Abs(ratio - 0) < Mathf.Epsilon);
    }

    public void ButtonPressed()
    {
        if (Contexts.sharedInstance.input.isPause) return;
        Contexts.sharedInstance.input.CreateEntity().ReplaceConsume(consumptionAmmount);
        //Contexts.sharedInstance.input.CreateEntity().AddConsume(consumptionAmmount);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PauseResumeButtonBehaviour : MonoBehaviour {

	public Text label;

	public void ButtonPressed()
	{
	    Contexts.sharedInstance.game.isPause = !Contexts.sharedInstance.game.isPause;
        //Pools.pool.isPause = !Pools.pool.isPause;
        label.text = Contexts.sharedInstance.game.isPause ? "Resume" : "Pause";
    }
}

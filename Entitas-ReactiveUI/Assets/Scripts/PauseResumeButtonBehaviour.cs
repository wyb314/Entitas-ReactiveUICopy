using UnityEngine;
using UnityEngine.UI;

public class PauseResumeButtonBehaviour : MonoBehaviour {

	public Text label;

	public void ButtonPressed()
	{
        //Contexts.sharedInstance.input.is
	    Contexts.sharedInstance.input.isPause = !Contexts.sharedInstance.input.isPause;
        //Pools.pool.isPause = !Pools.pool.isPause;
        label.text = Contexts.sharedInstance.input.isPause ? "Resume" : "Pause";
    }
}

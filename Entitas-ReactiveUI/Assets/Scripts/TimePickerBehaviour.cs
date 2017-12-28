using UnityEngine;
using UnityEngine.UI;

public class TimePickerBehaviour : MonoBehaviour, IPauseListener {

	void Start () 
	{
        //Contexts.sharedInstance.input.CreateEntity().AddPauseListener(this);
        //Pools.pool.CreateEntity().AddPauseListener(this);
        PauseStateChanged();
    }

	public void PauseStateChanged ()
	{
        gameObject.SetActive(Contexts.sharedInstance.input.isPause);
        if (Contexts.sharedInstance.game.hasTick)
        {
            var slider = GetComponent<Slider>();
            slider.maxValue = Contexts.sharedInstance.game.tick.currentTick;
            slider.value = Contexts.sharedInstance.game.tick.currentTick;
        }
    }

	public void ChangedValue()
	{
        Contexts.sharedInstance.game.ReplaceJumpInTime((long)GetComponent<Slider>().value);
		//Pools.pool.ReplaceJumpInTime((long)GetComponent<Slider>().value);
	}
}
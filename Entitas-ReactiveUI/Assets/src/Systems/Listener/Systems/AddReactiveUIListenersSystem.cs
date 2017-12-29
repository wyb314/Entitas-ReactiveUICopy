
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class AddReactiveUIListenersSystem : ReactiveSystem<GameEntity> ,IInitializeSystem, ICleanupSystem, ITearDownSystem
{
    private Contexts _contexts;
    public AddReactiveUIListenersSystem(Contexts contexts) : base(contexts.game)
    {
        this._contexts = contexts;
    }

    public void Initialize()
    {
        
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            this.AddListener(entity.reactiveUI.uiContent.transform);
        }
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ReactiveUI.Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasReactiveUI;
    }


    public void AddListener(Transform uiTran)
    {
        this.AddTimeLabelListener(uiTran);

        this.AddElixirListeners(uiTran);

        this.AddConsumeBtnsListeners(uiTran);

        this.AddPauseBtnListener(uiTran);

        this.AddSliderListener(uiTran);
    }



    // Time label
    public void AddTimeLabelListener(Transform uiTran)
    {
        Text timeLabel = uiTran.Find("TimeLabel").GetComponent<Text>();

        this._contexts.game.CreateEntity().AddTickListener(delegate()
        {
            var tick = Contexts.sharedInstance.game.tick.currentTick;
            var sec = (tick / 60) % 60;
            var min = (tick / 3600);
            //var secText = sec > 9 ? "" + sec : "0" + sec;
            //var minText = min > 9 ? "" + min : "0" + min;

            //timeLabel.text = minText + ":" + secText;
        });
    }

    //Elixir
    public void AddElixirListeners(Transform uiTran)
    {
        Text elixirLabel = uiTran.Find("ElixirAmount").GetComponent<Text>();
        RectTransform elixirCurrentRt = uiTran.Find("ElixirCurrent").GetComponent<RectTransform>();
        elixirCurrentRt.localScale = new Vector3(0, 1f, 1f);
        this._contexts.game.CreateEntity().AddElixirListener(delegate()
        {
            //elixirLabel.text = ((int)Contexts.sharedInstance.game.elixir.amount).ToString();
            elixirLabel.color = 
            System.Math.Abs(Contexts.sharedInstance.game.elixir.amount - ElixirProduceSystem.ElixirCapacity) < Mathf.Epsilon 
            ? Color.red : Color.black;

            var ratio = Contexts.sharedInstance.game.elixir.amount / ElixirProduceSystem.ElixirCapacity;
            elixirCurrentRt.localScale = new Vector3(ratio, 1f, 1f);
        });
    }
    
    //Consume buttons
    private void AddConsumeBtnsListeners(Transform uiTran)
    {
        //Btn1
        int btn1ConsumptionAmmount = 2;
        RectTransform btn1Rt = uiTran.Find("Button1").GetComponent<RectTransform>();
        RectTransform btn1ImgRt = btn1Rt.Find("Image").GetComponent<RectTransform>();
        float btn1ImgRtHeight = btn1ImgRt.rect.height;
        Text btn1Label = btn1Rt.Find("Text").GetComponent<Text>();
        btn1Label.text = btn1ConsumptionAmmount.ToString();
        btn1Rt.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Contexts.sharedInstance.input.isPause) return;
            Contexts.sharedInstance.input.CreateEntity().ReplaceConsume(btn1ConsumptionAmmount);
        });
        
        //Btn2
        int btn2ConsumptionAmmount = 3;
        RectTransform btn2Rt = uiTran.Find("Button2").GetComponent<RectTransform>();
        RectTransform btn2ImgRt = btn2Rt.Find("Image").GetComponent<RectTransform>();
        float btn2ImgRtHeight = btn2ImgRt.rect.height;
        Text btn2Label = btn2Rt.Find("Text").GetComponent<Text>();
        btn2Label.text = btn2ConsumptionAmmount.ToString();
        btn2Rt.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Contexts.sharedInstance.input.isPause) return;
            Contexts.sharedInstance.input.CreateEntity().ReplaceConsume(btn2ConsumptionAmmount);
        });

        //Btn3
        int btn3ConsumptionAmmount = 4;
        RectTransform btn3Rt = uiTran.Find("Button3").GetComponent<RectTransform>();
        RectTransform btn3ImgRt = btn3Rt.Find("Image").GetComponent<RectTransform>();
        float btn3ImgRtHeight = btn3ImgRt.rect.height;
        Text btn3Label = btn3Rt.Find("Text").GetComponent<Text>();
        btn3Label.text = btn3ConsumptionAmmount.ToString();
        btn3Rt.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Contexts.sharedInstance.input.isPause) return;
            Contexts.sharedInstance.input.CreateEntity().ReplaceConsume(btn3ConsumptionAmmount);
        });

        this._contexts.game.CreateEntity().AddElixirListener(()=>
        {
            var ratio = 1 - Mathf.Min(1f, (this._contexts.game.elixir.amount / (float)btn1ConsumptionAmmount));
            btn1ImgRt.sizeDelta = new Vector2(btn1ImgRt.rect.width, btn1ImgRtHeight * ratio);
            btn1Rt.GetComponent<Button>().enabled = (System.Math.Abs(ratio - 0) < Mathf.Epsilon);


            ratio = 1 - Mathf.Min(1f, (this._contexts.game.elixir.amount / (float)btn2ConsumptionAmmount));
            btn2ImgRt.sizeDelta = new Vector2(btn2ImgRt.rect.width, btn2ImgRtHeight * ratio);
            btn2Rt.GetComponent<Button>().enabled = (System.Math.Abs(ratio - 0) < Mathf.Epsilon);

            ratio = 1 - Mathf.Min(1f, (this._contexts.game.elixir.amount / (float)btn3ConsumptionAmmount));
            btn3ImgRt.sizeDelta = new Vector2(btn3ImgRt.rect.width, btn3ImgRtHeight * ratio);
            btn3Rt.GetComponent<Button>().enabled = (System.Math.Abs(ratio - 0) < Mathf.Epsilon);
        });

        this._contexts.input.CreateEntity().AddPauseListener(()=>
        {
            bool isPause = this._contexts.input.isPause;
            btn1Rt.GetComponent<Button>().enabled = !isPause;
            btn2Rt.GetComponent<Button>().enabled = !isPause;
            btn2Rt.GetComponent<Button>().enabled = !isPause;
        });
        
    }

    // Pause button
    private void AddPauseBtnListener(Transform uiTran)
    {
        Button pauseBtn = uiTran.Find("PauseButton").GetComponent<Button>();
        Text pauseLabel = uiTran.Find("PauseButton/Text").GetComponent<Text>();
        pauseBtn.onClick.AddListener(() =>
        {
            this._contexts.input.isPause = !this._contexts.input.isPause;
            pauseLabel.text = Contexts.sharedInstance.input.isPause ? "Resume" : "Pause";

        });
    }

    //Slider
    private void AddSliderListener(Transform uiTran)
    {
        Slider slider = uiTran.Find("Slider").GetComponent<Slider>();
        slider.onValueChanged.AddListener(val =>
        {
            this._contexts.game.ReplaceJumpInTime((long)val);
        });

        PauseStateChangedDelegate pauseStateAction = () =>
        {
            slider.gameObject.SetActive(Contexts.sharedInstance.input.isPause);
            if (this._contexts.game.hasTick)
            {
                slider.maxValue = Contexts.sharedInstance.game.tick.currentTick;
                slider.value = Contexts.sharedInstance.game.tick.currentTick;
            }
        };

        pauseStateAction();//初始化

        this._contexts.input.CreateEntity().AddPauseListener(pauseStateAction);
    }


    public void Cleanup()
    {
    }

    public void TearDown()
    {
    }


}

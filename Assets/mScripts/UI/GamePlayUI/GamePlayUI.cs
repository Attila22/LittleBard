using UnityEngine;
using System.Collections;

public class GamePlayUI : BaseUI {

	public UISlider mTimeSliderBar;
	public GameObject PauseBtn;
	public GameObject RestartBtn;

    public BottleManager mBottleManager;

	bool isTimeLimit = false;
	public float mTime;
	float MaxTime = 60;

	void Awake()
	{
		UIEventListener.Get(PauseBtn).onClick = OPauseBtnClick;
		UIEventListener.Get(RestartBtn).onClick = OnRestartBtnClick;
	}


	public override void Show ()
	{
		isTimeLimit = true;
		mTime = MaxTime;
        IsPlaySound = false;
	}

    bool IsPlaySound = false;
	void LateUpdate()
	{
		if (!isTimeLimit)
			return;
		mTime -= Time.deltaTime;
        if (mTime < 10)
        {
            if (!IsPlaySound)
            {
                SoundManager.Instance.PlayMusic("TimeLimit");
                IsPlaySound = true;
            }
        }
		if (mTime < 0) 
		{
			mTime = 0;
			isTimeLimit = false;
            GamePlaySceneMgr.Instance.OnGameOver(false);
		}
		float sliderValue = mTime/MaxTime;
		sliderValue = sliderValue<0.04f?0.04f:sliderValue;
		mTimeSliderBar.value = sliderValue;
	}

    

	public void OPauseBtnClick(GameObject go)
	{
        isTimeLimit = false;
        IsPlaySound = false;
        SoundManager.Instance.StopMusic("TimeLimit");
        UIManager.Instance.Show(UIType.GamePause);
	}

	public void ContinueGame()
    {
        isTimeLimit = true;
	}

	public void OnRestartBtnClick(GameObject go)
	{
		if (BeginnerGUIdeUI.IsShowNow)
            return;
        SoundManager.Instance.StopMusic("TimeLimit");
        GamePlaySceneMgr.Instance.ReStartGame();
	}

    public override void OnMsg(MsgType msgType, object msg)
	{
        switch (msgType)
        {
            case MsgType.None:
                break;
            case MsgType.SetLevel:
                mBottleManager.SetBottle((int)msg);
                break;
            case MsgType.ContinueGame:
                ContinueGame();
                break;
            case MsgType.PauseGame:
                PauseGame();
                break;
        }

	}

    void PauseGame()
    {
        isTimeLimit = false;
    }

	public override void Close ()
	{
        gameObject.SetActive(false);
	}

}

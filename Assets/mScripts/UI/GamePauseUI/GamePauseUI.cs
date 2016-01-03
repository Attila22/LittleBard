using UnityEngine;
using System.Collections;

public class GamePauseUI : BaseUI{

    public GameObject HomeBtn;
    public GameObject ContinueBtn;
    public GameObject SettingBtn;

    void Awake()
    {
        UIEventListener.Get(HomeBtn).onClick = OnHomeBtnClick;
        UIEventListener.Get(ContinueBtn).onClick = OnContinueBtnClick;
        UIEventListener.Get(SettingBtn).onClick = OnSettingBtnClick;
    }

    public override void Show()
    {
    }

    void OnHomeBtnClick(GameObject obj)
    {
        SoundManager.Instance.PlayMusic("BackGroundMusic");
        GamePlaySceneMgr.Instance.OnGameOver(false,false);
        UIManager.Instance.CloseAllUI();
        UIManager.Instance.Show(UIType.LevelSelet);
        Close();
    }

    void OnContinueBtnClick(GameObject obj)
    {
		if(!BeginnerGUIdeUI.IsShowNow)
        	UIManager.Instance.SendMsg(UIType.GamePlay, MsgType.ContinueGame,null);
        Close();
    }


    void OnSettingBtnClick(GameObject obj)
    {
        UIManager.Instance.Show(UIType.GamePlaySetting);
        //UIManager.Instance.SendMsg(UIType.GamePlaySetting, MsgType.None, GameSetting.SettingType.GamePlaySetting);
    }

    public override void OnMsg(MsgType msgType, object msg)
    {
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOverPane : BaseUI{


    public GameObject NextBtn;
    public GameObject ReplayBtn;
    public GameObject HomeBtn;
    public GameObject SettingBtn;

    public UITexture BackgroundSprite;
    public Texture2D WndBg;
    public Texture2D LosBg;

	public List<GameOverBottle> mBottleGrouup = new List<GameOverBottle>();

    void Awake()
    {
        UIEventListener.Get(NextBtn).onClick = OnNextBtnClick;
        UIEventListener.Get(ReplayBtn).onClick = OnReplayBtnClick;
        UIEventListener.Get(HomeBtn).onClick = OnHomeBtnClick;
        UIEventListener.Get(SettingBtn).onClick = OnSettingBtnClick;
    }

    public override void Show()
    {
        gameObject.SetActive(true);
		
    }

	void ShowBottle(GameOverData data)
	{
		LevelData levelData = GameManager.Instance.mGameDataManager.GetLevelData(GamePlaySceneMgr.Instance.CurrentLevel);
		int smallNum = 0;
		int mediuNum = 0;
		int BigNum = 0;
		levelData.BottleGetDataGroup.ApplyAllItem(C=>{
			switch(C.mFragmentType)
			{
			case FragmentType.Big:
				BigNum+=C.GetFragNum;
				break;
			case FragmentType.Medium:
				mediuNum+=C.GetFragNum;
				break;
			case FragmentType.Small:
				smallNum+=C.GetFragNum;
				break;
			}
		});
		mBottleGrouup[0].SetMyFragments(data.SmallFragmentNum,smallNum,FragmentType.Small);
		mBottleGrouup[1].SetMyFragments(data.MediumFragmentNum,mediuNum,FragmentType.Medium);
		mBottleGrouup[2].SetMyFragments(data.BigFragmentNum,BigNum,FragmentType.Big);

	}

    public override void OnMsg(MsgType msgType, object msg)
    {
        switch (msgType)
        {
            case MsgType.GameOverData:
                GameOverData data = (GameOverData)msg;
                SoundManager.Instance.StopMusic("TimeLimit");
                SoundManager.Instance.PlaySound(data.IsWin ? "GameWin" : "GameLose");
                //BackgroundSprite.spriteName = data.IsWin ? "WinBg" : "LoseUIBg";
                BackgroundSprite.mainTexture = data.IsWin?WndBg:LosBg;
                NextBtn.SetActive(data.IsWin);
                ReplayBtn.SetActive(!data.IsWin);
                ShowBottle(data);
                if (data.IsWin)
                    GameManager.Instance.mGameDataManager.SaveUnLockLevel(GamePlaySceneMgr.Instance.CurrentLevel);
                break;
        }
    }

    void OnNextBtnClick(GameObject obj)
    {
        int nextLevel = GamePlaySceneMgr.Instance.CurrentLevel;
        nextLevel ++;
        nextLevel= nextLevel>3?3:nextLevel;
        //GamePlaySceneMgr.Instance.InitGameScenes(nextLevel);

		UIManager.Instance.Show(UIType.MissUI);
		UIManager.Instance.SendMsg(UIType.MissUI,MsgType.MissionBottleNum,nextLevel);

        Close();
    }

    void OnReplayBtnClick(GameObject obj)
	{
		int Level = GamePlaySceneMgr.Instance.CurrentLevel;
		GameManager.Instance.StartGame(Level);
        Close();
    }

    void OnHomeBtnClick(GameObject obj)
	{
		SoundManager.Instance.PlayMusic("BackGroundMusic");
        UIManager.Instance.CloseAllUI();
        //GameManager.Instance.mSoundManager.StopMusic("");
        UIManager.Instance.Show(UIType.MainUI);
        Close();
    }
    void OnSettingBtnClick(GameObject obj)
    {
        UIManager.Instance.Show(UIType.GamePlaySetting);
        UIManager.Instance.SendMsg(UIType.GamePlaySetting,MsgType.None, GameSetting.SettingType.GameOverSetting);
        //Close();
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }
}

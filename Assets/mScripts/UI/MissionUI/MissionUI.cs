using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionUI : BaseUI{


    public List<MissionBottle> mBottleGrouup = new List<MissionBottle>();

    public GameObject CotinueBtn;

    int mLevel;

    void Awake()
    {
        UIEventListener.Get(CotinueBtn).onClick = OnContinueBtnClick;
    }

    public override void Show()
    {
        SoundManager.Instance.StopMusic("BackGroundMusic");
    }

    void OnContinueBtnClick(GameObject go)
    {
		//        GameManager.Instance.StartGame (mLevel);
		if(mLevel==1)
			UIManager.Instance.Show(UIType.BeginnerGuideUI);
		else
			UIManager.Instance.SendMsg (UIType.GamePlay, MsgType.ContinueGame,mLevel);
        Close();
    }

    public override void OnMsg(MsgType msgType, object msg)
    {
		switch (msgType)
		{
		case MsgType.MissionBottleNum:
			mLevel = (int)msg;
			LevelData mlevelData = GameManager.Instance.mGameDataManager.GetLevelData(mLevel);
			int smallNum = 0;
			int mediuNum = 0;
			int bigNum = 0;
			
			mlevelData.BottleGetDataGroup.ApplyAllItem(C => {
				switch (C.mFragmentType)
				{
				case FragmentType.Big:
					bigNum+= C.GetFragNum;
					break;
				case FragmentType.Medium:
					mediuNum += C.GetFragNum;
					break;
				case FragmentType.Small:
					smallNum += C.GetFragNum;
					break;
				}
			});
			mBottleGrouup[0].SetMyFragments(smallNum,FragmentType.Small);
			mBottleGrouup[1].SetMyFragments(mediuNum,FragmentType.Medium);
			mBottleGrouup[2].SetMyFragments(bigNum,FragmentType.Big);
			
			GameManager.Instance.StartGame (mLevel);
			UIManager.Instance.SendMsg(UIType.GamePlay,MsgType.PauseGame,mLevel);
			break;
		}
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }
}

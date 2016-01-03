using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlaySceneMgr : MonoBehaviour
{

    public static GamePlaySceneMgr Instance;

	public FragmentManager mFragmentManager{get;private set;}
    public FragmentTouchMgr mFragmentTouchMgr { get; private set; }
    public ScreenTouchManager mScreenTouchMgr { get; private set; }

	public FragmentAttractPoint mFragmentAttractPoint;
	public GameCamraRotator mGameCamraRotator{get;private set;}
    public Renderer PicWallRendere;

    public int CurrentLevel { get; private set; }

    public Camera mMainCamera;

	int MaxFragmentNum = 10;

	void Awake () {
        Instance = this;
        mScreenTouchMgr = gameObject.AddComponent<ScreenTouchManager>();
        mFragmentTouchMgr = new FragmentTouchMgr();
        mFragmentManager = new FragmentManager();
		
		mGameCamraRotator = Camera.main.gameObject.GetComponent<GameCamraRotator>();
        
	}

    void OnDestroy()
    {
        Instance = null;
    }

	public void InitGameScenes(int level)
    {
        mMainCamera.enabled = true;
		UIManager.Instance.Show(UIType.FragmentSelectUI);
		UIManager.Instance.Show (UIType.GamePlay);
        UIManager.Instance.SendMsg(UIType.GamePlay,MsgType.SetLevel,level);
		mScreenTouchMgr.enabled = true;
		mScreenTouchMgr.CleanUpDisableTouchType();
		mGameCamraRotator.enabled = true;
        CurrentLevel = level;
        //PicWallRendere.material.mainTexture = GameManager.Instance.mGameDataManager.GetLevelData(level).PicWallPic;
        if (level == 1)
        {
            //StartCoroutine(ShowBeginnerGuideUI());
        }
        else
        {
			mFragmentManager.InitFragment(MaxFragmentNum);
        }
		SetBackGround(level);
	}

	void SetBackGround(int level)
	{
		level = level>2?2:level;
		for(int i = 0;i<10;i++)
		{
			int pathName = i+1;
			string path = string.Format("FragmentShow/BgTex{0}/{1}",level,pathName<10?"0"+pathName:pathName.ToString());
			Texture2D tex  = Resources.Load(path)as Texture2D;
			PicWallRendere.materials[i].mainTexture = tex;
//			Debug.Log(tex.name);
		}
	}

//    public void ShowBeginnerGuideUI()
//    {
//        //yield return null;
//        UIManager.Instance.Show(UIType.BeginnerGuideUI);
//        UIManager.Instance.SendMsg(UIType.GamePlay, MsgType.PauseGame, null);
//    }

    public void OnBeginnerGUIdeFinish()
    {
		mFragmentManager.InitFragment(MaxFragmentNum);
		UIManager.Instance.Close(UIType.BeginnerGuideUI);
		UIManager.Instance.SendMsg(UIType.GamePlay, MsgType.ContinueGame, null);
    }

    public void ReStartGame()
    {
        mMainCamera.enabled = true;
		mFragmentManager.InitFragment(MaxFragmentNum);
        UIManager.Instance.Show(UIType.FragmentSelectUI);
        UIManager.Instance.Show(UIType.GamePlay);
        UIManager.Instance.SendMsg(UIType.GamePlay, MsgType.SetLevel, CurrentLevel);
        mScreenTouchMgr.enabled = true;
        mGameCamraRotator.enabled = true;
    }

    public void OnGameOver(bool isWin,bool showGameOverUI = true)
    {
        mFragmentManager.CleanUpAllFragment();
        mMainCamera.enabled = false;
		GamePlayUI mGamePlayUI =UIManager.Instance.GetUI( UIType.GamePlay)as GamePlayUI;
		GameOverData overData = mGamePlayUI.mBottleManager.GetGameOverData();
		overData.IsWin = isWin;
        UIManager.Instance.CloseAllUI();
        if(showGameOverUI)
            UIManager.Instance.Show(UIType.GameOver);
		UIManager.Instance.SendMsg(UIType.GameOver, MsgType.GameOverData, overData);
        mScreenTouchMgr.enabled = false;
        mGameCamraRotator.enabled = false;
    }

	public Vector3 mPos{ get; set;}
	public float BeAttractDistance{ get; set;}
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(mPos, BeAttractDistance);
    }

}

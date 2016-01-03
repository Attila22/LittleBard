using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectUI : BaseUI{

	public UILabel Level1Label;
	public UILabel Level2Label;
	public UILabel Level3Label;
	public List< GameObject> LevelBtnGroup;
    public List<GameObject> LockIconGroup;
    public int CurrengUnLockLevel;
	public GameObject BackBtn;


	void Awake()
	{
        LevelBtnGroup.ApplyAllItem(C => UIEventListener.Get(C).onClick = OnLevelBtnClick);
		UIEventListener.Get (BackBtn).onClick = OnBackBtnClick;
	}

	void OnLevelBtnClick(GameObject obj)
	{
        int level = LevelBtnGroup.IndexOf(obj)+1;
		UIManager.Instance.Show(UIType.MissUI);
		UIManager.Instance.SendMsg(UIType.MissUI,MsgType.MissionBottleNum,level);
		Close ();
	}
	public override void Show ()
    {
        gameObject.SetActive(true);
        Level1Label.SetText(GetLevelLabel(1));
        Level2Label.SetText(GetLevelLabel(2));
        Level3Label.SetText(GetLevelLabel(3));
        SetUnLockLevel(GameManager.Instance.mGameDataManager.GetUnLockLevel());
	}

    void SetUnLockLevel(int level)
    {
        CurrengUnLockLevel = level;
        for (int i = 0; i < LockIconGroup.Count; i++)
        {
            if (i < level)
            {
                LevelBtnGroup[i].GetComponent<Collider>().enabled = true;
                LockIconGroup[i].SetActive(false);
            }
            else
            {
                LevelBtnGroup[i].GetComponent<Collider>().enabled = false;
                LockIconGroup[i].SetActive(true);
            }
        }
    }

    string GetLevelLabel(int level)
    {
        LevelData levelData = GameManager.Instance.mGameDataManager.GetLevelData(level);
        int haseNum = 0;
        List<int> ownGroup = GameManager.Instance.mGameDataManager.GetOwnBottle();
        levelData.BottleGetDataGroup.ApplyAllItem(C => { 
            ownGroup.ApplyAllItem(T=>{
                if ((int)C.mBottleType == T)
                {
                    haseNum++;
                }
            });
        });
        return string.Format("{0}/{1}",haseNum,levelData.BottleGetDataGroup.Count);
    }

	void OnBackBtnClick(GameObject go)
	{
		Close ();
		UIManager.Instance.Show (UIType.ScenesSelect);
	}

    public override void OnMsg(MsgType msgType,object msg)
	{

	}

	public override void Close ()
	{
		gameObject.SetActive (false);
	}

}

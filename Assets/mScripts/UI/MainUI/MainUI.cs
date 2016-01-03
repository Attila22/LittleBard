using UnityEngine;
using System.Collections;

public class MainUI : BaseUI {

	public GameObject SettingBtn;
	public GameObject CollectionsBtn;
	public GameObject StartBtn;


	void Start()
	{
		UIEventListener.Get(SettingBtn).onClick = OnSettingBtnClick;
		UIEventListener.Get(CollectionsBtn).onClick = OnCollectionsBtnClick;
		UIEventListener.Get(StartBtn).onClick = OnStartBtnClick;
	}

	void OnStartBtnClick(GameObject go)
	{
		if(IsLevelPass())
		{
			UIManager.Instance.Show(UIType.ScenesSelect);
			Close();
		}else
		{
			Close();
			VideoPlay.Instance.Play(()=>{
				UIManager.Instance.Show(UIType.ScenesSelect);
			});
		}
	}

	bool IsLevelPass()
	{
		return GameManager.Instance.mGameDataManager.GetUnLockLevel()>1;
	}

	void OnCollectionsBtnClick(GameObject go)
	{
		UIManager.Instance.Show(UIType.Collections);
		Close();
	}
	void OnSettingBtnClick(GameObject go)
	{
		UIManager.Instance.Show(UIType.MainGameSetting);
        UIManager.Instance.SendMsg(UIType.MainGameSetting,MsgType.None, GameSetting.SettingType.MainSetting);
	}

	public override void Show ()
	{
	}
	public override void Close ()
	{
		gameObject.SetActive (false);
	}
    public override void OnMsg(MsgType msgType,object msg)
	{
	}



}

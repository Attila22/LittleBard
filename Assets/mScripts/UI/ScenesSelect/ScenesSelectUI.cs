using UnityEngine;
using System.Collections;

public class ScenesSelectUI : BaseUI {


	public GameObject HongkongBtn;
	public GameObject KowLoonBtn;
	public GameObject SaiKungBtn;
	public GameObject YuenLungBtn;
	public GameObject LanTauIslandBtn;
    public GameObject BackBtn;

	void Awake()
	{
		UIEventListener.Get (HongkongBtn).onClick = OnBtnClick;
		UIEventListener.Get (KowLoonBtn).onClick = OnBtnClick;
		UIEventListener.Get (SaiKungBtn).onClick = OnBtnClick;
		UIEventListener.Get (YuenLungBtn).onClick = OnBtnClick;
        UIEventListener.Get(LanTauIslandBtn).onClick = OnBtnClick;
        UIEventListener.Get(BackBtn).onClick = OnBackBtnClick;
	}


	void OnBtnClick(object obj)
	{
		Close ();
		UIManager.Instance.Show (UIType.LevelSelet);
	}

    void OnBackBtnClick(object obj)
    {
        Close();
        UIManager.Instance.Show(UIType.MainUI);
    }

	public override void Show ()
	{

	}
   public override void OnMsg(MsgType msgType,object msg)
	{

	}

	public override void Close ()
	{
		gameObject .SetActive (false);
	}
}

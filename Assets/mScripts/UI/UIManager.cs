using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIManager : MonoBehaviour {


	public static UIManager Instance;

	public List<UIPrefabData> mPrefabDataGroup;
	Dictionary<UIType,BaseUI> UIInstsanceGroup = new Dictionary<UIType, BaseUI>();

	void Awake()
	{
		Instance = this;
	}

	public void Show (UIType type)
	{
		BaseUI showUI = null;
		if(!UIInstsanceGroup.TryGetValue(type,out showUI))
		{
			showUI = NGUITools.AddChild(gameObject,mPrefabDataGroup.First(C=>C.mType == type).mPrefab).GetComponent<BaseUI>();
			UIInstsanceGroup[type] = showUI;
		}
		showUI.gameObject.SetActive(true);
		showUI.Show();
	}

	public BaseUI GetUI(UIType type)
	{
		BaseUI showUI = null;
		UIInstsanceGroup.TryGetValue(type,out showUI);
		return showUI;
	}

	public void SendMsg(UIType type,MsgType msgType,object arg)
	{
		BaseUI UI = null;
		if(UIInstsanceGroup.TryGetValue(type,out UI))
		{
			if(UI.gameObject.activeSelf)
            	UI.OnMsg(msgType,arg);
		}
	}

	public void Close(UIType type)
	{
		BaseUI closeUI = null;
		if(UIInstsanceGroup.TryGetValue(type,out closeUI))
		{
			closeUI.Close();
		}
	}

    public void CloseAllUI()
    {
        UIInstsanceGroup.ApplyAllItem(C => {
            if (C.Value.gameObject.activeSelf)
                C.Value.Close();
        });
    }

}

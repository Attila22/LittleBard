using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BeginnerGUIdeUI : BaseUI {

	public static bool IsShowNow = false;

	public List<BeginnerGuideData> GuideDataGroup;

	public GameObject Root;

	public override void Show ()
	{
		IsShowNow = true;
        StartCoroutine( OnShowGuideData(GuideDataGroup[0]));
	}

	public override void Close ()
	{
		IsShowNow = false;
		gameObject.SetActive (false);
	}

	public override void OnMsg (MsgType msgType, object msg)
	{
	}

	IEnumerator OnShowGuideData(BeginnerGuideData showData)
	{
		yield return null;
		Root.transform.ClearChild ();
		SingleBeginnerGUide singleGruid = NGUITools.AddChild (Root, showData.AnimPrefab).GetComponent<SingleBeginnerGUide>();
		singleGruid.Init (this,showData);
	}

	public IEnumerator ShowNextGuide(BeginnerGuideData currentData)
	{
		yield return null;
		int index = GuideDataGroup.IndexOf (currentData);
		index++;
        if (index < GuideDataGroup.Count)
        {
			StartCoroutine(OnShowGuideData(GuideDataGroup[index]));
        }
        else
        {
            GamePlaySceneMgr.Instance.mScreenTouchMgr.CleanUpDisableTouchType();
            GamePlaySceneMgr.Instance.OnBeginnerGUIdeFinish();
        }
	}

}

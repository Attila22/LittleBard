using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FragmentAttractPoint : MonoBehaviour {


	bool mActive = false;

	
	List<SingleFragment> SmallFragmentGroup = new List<SingleFragment>();
	List<SingleFragment> MediumFragmentGroup = new List<SingleFragment>();

	float MinDis = 1f;


	public void SetActive(bool flag)
	{
		mActive = flag;
		if (mActive) 
		{
			GetFragmentGroup();
		}
		else
		{
			CleanUpGroup(SmallFragmentGroup);
			CleanUpGroup(MediumFragmentGroup);
//			Debug.Log("CleanUpGroup");
		}
	}

	void GetFragmentGroup()
	{
		List<SingleFragment> fragmentGroup = GamePlaySceneMgr.Instance.mFragmentManager.mFragmentGroup;
		for (int i = 0; i<fragmentGroup.Count; i++) 
		{
			SingleFragment frag = fragmentGroup[i];
			float dis = Vector3.Distance(frag.transform.position,transform.position);
			if(dis<20)
			{
				switch(frag.mFragmentType)
				{
				case FragmentType.Small:
					if(SmallFragmentGroup.Count<3)
						SmallFragmentGroup.Add(frag);
					break;
				case FragmentType.Medium:
					if(MediumFragmentGroup.Count<3)
						MediumFragmentGroup.Add(frag);
					break;
				default:
					break;
				}
			}
//			Debug.Log(dis);
		}
	}

	void CleanUpGroup(List<SingleFragment> group)
	{
		for (int i = 0; i<group.Count; i++) {
			group[i].mFragmentStatus = SingleFragment.FragmentStatus.Stay;
		}
		group.Clear ();
	}

	void Update()
	{
		if (!mActive)
			return;
		if (SmallFragmentGroup.Count > 0) 
		{
			FlyFragTogetor(SmallFragmentGroup);
		}
		if (MediumFragmentGroup.Count > 0) 
		{
			FlyFragTogetor(MediumFragmentGroup);
		}
	}

	
	List<SingleFragment> TranslateFragmentGroup = new List<SingleFragment>();
	void FlyFragTogetor(List<SingleFragment> fragGroup)
	{
		int moveNum = 0;
//		Debug.Log ("New");
		TranslateFragmentGroup.Clear ();
		for (int i = fragGroup.Count-1; i>=0; i--) 
		{
			SingleFragment child = fragGroup[i];
			if(child==null||child.gameObject == null)
			{
				fragGroup.RemoveAt(i);
				continue;
			}
			float dis = Vector3.Distance(child.transform.position,transform.position);
//			Debug.Log("dis"+dis);
			if(dis>MinDis)
			{
				if(moveNum<3)
				{
					moveNum++;
					Vector3 dir = Vector3.Normalize( transform.position - child.transform.position)*0.2f;
					child.transform.position = child.transform.position+dir;
				}
			}else
			{
				if(TranslateFragmentGroup.Count<3){
					TranslateFragmentGroup.Add(child);
					EventManager.Instance.TriggerEvent(GameEventType.BeginnerGuideEvent,BeginnerGuideEventType.AttractFragmentComplete);
				}
			}
		}
		if (TranslateFragmentGroup.Count == 3) 
		{
			for(int i = 0;i<TranslateFragmentGroup.Count;i++)
			{
				if(fragGroup.Contains(TranslateFragmentGroup[i]))
					fragGroup.Remove(TranslateFragmentGroup[i]);
			}
			GamePlaySceneMgr.Instance.mFragmentTouchMgr.TranslateFragmentToBinger(TranslateFragmentGroup,transform.position);
			SetActive( false);
		}
	}


}

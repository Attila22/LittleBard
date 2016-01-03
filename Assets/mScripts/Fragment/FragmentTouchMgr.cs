using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FragmentTouchMgr
{
    float BeAttractDistance = 0.12f;

    List<SingleFragment> SmallBeAttractFragmentGroup = new List<SingleFragment>();
    List<SingleFragment> MediumBeAttractFragmentGroup = new List<SingleFragment>();

    Vector3 TempPosition = Vector3.zero;

    public void BeginTouchScreen(Vector3 pos)
    {
        pos = GamePlaySceneMgr.Instance.mFragmentManager.GetPosInRange(pos);

        GamePlaySceneMgr.Instance.mPos = pos;
        GamePlaySceneMgr.Instance.BeAttractDistance = BeAttractDistance;

		GamePlaySceneMgr.Instance.mFragmentAttractPoint.transform.position = pos;
		GamePlaySceneMgr.Instance.mFragmentAttractPoint.SetActive (true);

//        if (TempPosition != pos)
//        {
//            TempPosition = pos;
//            SmallBeAttractFragmentGroup.Clear();
//            MediumBeAttractFragmentGroup.Clear();
//
//            List<SingleFragment> fragmentGroup = GamePlaySceneMgr.Instance.mFragmentManager.mFragmentGroup;
//            for (int i = 0; i < fragmentGroup.Count; i++)
//            {
//                SingleFragment fragment = fragmentGroup[i];
//				if (fragment.IsInCameraView&&fragment.mFragmentStatus == SingleFragment.FragmentStatus.Stay&&Vector3.Distance(fragment.transform.position, pos) < BeAttractDistance)
//                {
//                    switch (fragment.mFragmentType)
//                    {
//                        case FragmentType.Small:
//                            SmallBeAttractFragmentGroup.Add(fragment);
//                            break;
//                        case FragmentType.Medium:
//                            MediumBeAttractFragmentGroup.Add(fragment);
//                            break;
//                    }
//                }
//            }


            //Debug.Log("SmallBeAttractFragmentGroup.Count:" + SmallBeAttractFragmentGroup.Count);
            //Debug.Log("MediumBeAttractFragmentGroup.Count:" + MediumBeAttractFragmentGroup.Count);
//            if (SmallBeAttractFragmentGroup.Count >= 3)
//            {
//                TranslateFragmentToBinger(SmallBeAttractFragmentGroup,pos);
//            }
//            else if (MediumBeAttractFragmentGroup.Count >= 3)
//            {
//                TranslateFragmentToBinger(MediumBeAttractFragmentGroup, pos);
//            }
//        }
    }

	public void EndTouchScreen()
	{
		GamePlaySceneMgr.Instance.mFragmentAttractPoint.SetActive (false);
	}

    public void TranslateFragmentToBinger(List<SingleFragment> fragmentGroup,Vector3 pos)
    {
        int flyNum = 0;
        for (int i = 0; i < 3; i++)
		{
			SingleFragment fragment = fragmentGroup[i];
			GamePlaySceneMgr.Instance.mFragmentManager.DestroyFragment(fragment);
			flyNum++;
			if (flyNum >= 3)
			{
				GamePlaySceneMgr.Instance.mFragmentManager.AddNewFragment(pos, (FragmentType)(((int)fragment.mFragmentType) + 1),true);
				GamePlaySceneMgr.Instance.mFragmentManager.AddFragmentToMax();
				EventManager.Instance.TriggerEvent(GameEventType.BeginnerGuideEvent,BeginnerGuideEventType.TranslateBinggerFragment);
			}
		}
	}

	
}

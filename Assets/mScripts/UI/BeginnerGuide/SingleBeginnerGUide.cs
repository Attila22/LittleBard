using UnityEngine;
using System.Collections;

public class SingleBeginnerGUide : MonoBehaviour {

	BeginnerGUIdeUI mParent;
	BeginnerGuideData mData;

	public GameObject mRoundArrow;

	public BeginnerGuideEventType mCompareDataType = BeginnerGuideEventType.None;

	bool IsTrigger = false;
	void Awake()
	{
		EventManager.Instance.AddEvent (GameEventType.BeginnerGuideEvent, OnEvent);
	}

	void OnDestroy()
	{
		EventManager.Instance.DeleteEvent(GameEventType.BeginnerGuideEvent, OnEvent);
	}

	void OnEvent(object obj)
	{
		if(IsTrigger)
			return ;
		BeginnerGuideEventType msgType = (BeginnerGuideEventType)obj;
		if (mCompareDataType == BeginnerGuideEventType.None||mCompareDataType == msgType) {
//			Debug.Log("触发引导事件："+msgType);
			IsTrigger = true;
			StartCoroutine( mParent.ShowNextGuide (mData));
		}
	}

	void Update()
	{
//		if(Input.GetMouseButtonDown(0))
//			Debug.Log(UICamera.mainCamera.ScreenToViewportPoint(Input.mousePosition));
	}

    public void Init(BeginnerGUIdeUI parent, BeginnerGuideData mdata)
    {
        mParent = parent;
        mData = mdata;
        switch (mdata.mGuideType)
        {
            case BeginnerGuideType.Step1:
                GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeDisable(ScreenTouchManager.TouchType.DrawGrid);
                GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeDisable(ScreenTouchManager.TouchType.GetFragment);
                GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeDisable(ScreenTouchManager.TouchType.MoveScreen);
                break;
            case BeginnerGuideType.Step2:
                GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeActive(ScreenTouchManager.TouchType.DrawGrid);
                SingleFragment mfragment = GamePlaySceneMgr.Instance.mFragmentManager.AddFragmentAtScreenPos(new Vector3(0.7f, 0.5f, 0), FragmentType.Small);
                mRoundArrow.transform.position = fragmentToRoundArrowPos(mfragment);
                break;
		case BeginnerGuideType.Step3:
//                SoundManager.Instance.PlaySound("RoundArrow");
			GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeActive(ScreenTouchManager.TouchType.GetFragment);
			GamePlaySceneMgr.Instance.mFragmentManager.AddFragmentAtScreenPos(new Vector3(0.6f, 0.5f, 0), FragmentType.Small);
			break;
            case BeginnerGuideType.Step4:
                GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeDisable(ScreenTouchManager.TouchType.DrawGrid);
                GamePlaySceneMgr.Instance.mFragmentManager.AddFragmentAtScreenPos(new Vector3(0.7f, 0.4f, 0), FragmentType.Small);
                GamePlaySceneMgr.Instance.mFragmentManager.AddFragmentAtScreenPos(new Vector3(0.8f, 0.5f, 0), FragmentType.Small);
                break;
            case BeginnerGuideType.Step5:
                GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeActive(ScreenTouchManager.TouchType.DrawGrid);
                SingleFragment fragment = GamePlaySceneMgr.Instance.mFragmentManager.mFragmentGroup[0];
                fragment.SetRandomMoveActive(false);
                mRoundArrow.transform.position = fragmentToRoundArrowPos(fragment);

                break;
            case BeginnerGuideType.Step6:
                GamePlaySceneMgr.Instance.mGameCamraRotator.SetTweenStopActive(false);
                GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeActive(ScreenTouchManager.TouchType.MoveScreen);
                break;
            case BeginnerGuideType.Step7:
                GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeDisable(ScreenTouchManager.TouchType.DrawGrid);
                GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeDisable(ScreenTouchManager.TouchType.MoveScreen);
                GamePlaySceneMgr.Instance.mScreenTouchMgr.SetTouchTypeActive(ScreenTouchManager.TouchType.GetFragment);
                GamePlaySceneMgr.Instance.mFragmentManager.AddFragmentAtScreenPos(new Vector3(0.6f, 0.5f, 0), FragmentType.Medium);
                GamePlaySceneMgr.Instance.mFragmentManager.AddFragmentAtScreenPos(new Vector3(0.7f, 0.4f, 0), FragmentType.Medium);
                GamePlaySceneMgr.Instance.mFragmentManager.AddFragmentAtScreenPos(new Vector3(0.8f, 0.5f, 0), FragmentType.Medium);
                break;
            case BeginnerGuideType.Step8:
                GamePlaySceneMgr.Instance.mGameCamraRotator.SetTweenStopActive(true);
                break;
        }
    }

    Vector3 fragmentToRoundArrowPos(SingleFragment fragment)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(fragment.transform.position);
        screenPos.z = 0;
        Vector3 mpos = UICamera.mainCamera.ScreenToWorldPoint(screenPos);
        return mpos;
    }
}

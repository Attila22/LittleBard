using UnityEngine;
using System.Collections;

public class SingleFragment : MonoBehaviour {
    public delegate void FlyCompleteDelegate(SingleFragment fragment);
	public enum FragmentStatus
	{
		FlyToPos,
		FlyOut,
		Stay,
	}

    FlyCompleteDelegate FlyCompleteCallBack;

    public FragmentType mFragmentType { get; private set; }
	public FragmentStatus mFragmentStatus;
    public bool IsInCameraView { get; private set; }
	public RandomMove mRandomMove ;
    public FragmentLookatCam mFragmentLookatCam;

    GameObject EffectPrefab { get { return Resources.Load("") as GameObject; } }
    GameObject EffectObj;
    GameObject TweenFloatObj;

    public void Init(FragmentType type,bool isShowEffect = false)
    {
        mFragmentType = type;

        transform.localScale *= (int)type;

		mFragmentStatus = FragmentStatus.Stay;
		
		SetRandomMoveActive(true);
    }

	void OnDestroy()
	{
		if (TweenFloatObj != null)
			Destroy(TweenFloatObj);
	}

    IEnumerator DestroyEffectForTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(EffectObj);
        EffectObj = null;
    }

    void OnBecameInvisible()
    {
        IsInCameraView = false;//不渲染;
        mFragmentLookatCam.SetActive(false);
    }

    void OnBecameVisible()
    {
        IsInCameraView = true;//渲染
        mFragmentLookatCam.SetActive(true);
    }

    public void FlyToPos(Vector3 pos,FlyCompleteDelegate completeCallBack)
    {
		SetRandomMoveActive(false);
        if (TweenFloatObj != null)
            Destroy(TweenFloatObj);
		mFragmentStatus = FragmentStatus.FlyToPos;
        TweenFloatObj = TweenFloat.Begin(0.5f, 0, 1, SetPosition, OnFlyComplete);
        fromPos = transform.position;
        toPos = pos;
        FlyCompleteCallBack = completeCallBack;
//        mTweenPosition.enabled = false;
    }
    Vector3 fromPos;
    Vector3 toPos;
    void SetPosition(float value)
    {
        transform.position = Vector3.Slerp(fromPos, toPos, value);
    }
    void OnFlyComplete()
    {
        if (FlyCompleteCallBack != null)
            FlyCompleteCallBack(this);
		SetRandomMoveActive(true);
    }

    public void FlyOut()
    {
        if (mFragmentType == FragmentType.Small)
            GamePlaySceneMgr.Instance.mFragmentManager.FlyToRandomPos(this);
        else
            GamePlaySceneMgr.Instance.mFragmentManager.RemoveBigTypeFragment(this);
    }

	public void SetRandomMoveActive(bool flag)
	{
		mRandomMove.SetActive(flag);
	}


	public bool IsBeginnerGuideFragment = false;
    float StayTimeLimit = 10;
    float mStayTime = 0;
    void Update()
    {
		if (!IsBeginnerGuideFragment&&mFragmentStatus == FragmentStatus.Stay && mFragmentType != FragmentType.Small)
        {
            mStayTime += Time.deltaTime;
            if (mStayTime > StayTimeLimit)
            {
				GamePlaySceneMgr.Instance.mFragmentManager.RemoveBigTypeFragment(this);
            }
        }
    }


	float getRandomNum()
	{
		return Random.Range (-1f,1f);
	}
}

using UnityEngine;
using System.Collections;

public class FragmentFlyToBottleEffect : MonoBehaviour {
    public delegate void FinishCallBackDelegate(object obj);
	
    FragmentType mType;

    public UISprite mSPrite;

    public BottleFragRandomFly mBottleFragRandomFly;

    FinishCallBackDelegate FinishCallBack;
    object callBackValue;

    void Awake()
    {
        mBottleFragRandomFly = gameObject.AddComponent<BottleFragRandomFly>();
        mBottleFragRandomFly.SetActive(false);
    }

    //IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(1);
    //    SetRandomFly();
    //}

    public void Init(FragmentType type)
    {
        mType = type;
        switch (type)
        {
            case FragmentType.Big:
                mSPrite.spriteName = "Fragment_Big";
                break;
            case FragmentType.Medium:
                mSPrite.spriteName = "Fragment_Medium";
                break;
            case FragmentType.Small:
                mSPrite.spriteName = "Fragment_Small";
                break;
        }
		//mSPrite.transform.localScale *= (int)type;
		float scale = 0.5f+((int)type)*0.5f;
		mSPrite.transform.localScale*= scale;
	}

    public void FlyToPos(Vector3 pos,FinishCallBackDelegate finishCallBack,object obj)
	{
		this.FinishCallBack = finishCallBack;
		this.callBackValue = obj;

		transform.localPosition = pos;
		OnFlyToTargetComplete ();
		return;
		isFly = true;
		flyTime = 0;
        fromPos = transform.localPosition;
        toPos = pos;
    }

    bool isFly = false;
    float flyTime = 0;
	float FlyTimeDis = 1;
    Vector3 fromPos;
    Vector3 toPos;
    void Update()
    {
        if (!isFly)
            return;
        flyTime += Time.deltaTime;
        if (flyTime >1)
        {
            isFly = false;
            flyTime = 1;
			OnFlyToTargetComplete();
        }
        transform.localPosition = Vector3.Lerp(fromPos,toPos,flyTime);
    }

	void OnFlyToTargetComplete()
	{
		if (FinishCallBack != null)
			FinishCallBack(callBackValue);
		SetRandomFly();
	}

    void SetRandomFly()
    {
        mBottleFragRandomFly.SetActive(true);
		mBottleFragRandomFly.mSPrite.depth = 3;
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SingleBottle : MonoBehaviour {

    public LevelBottleData mLevelBottledata { get; private set; }
	public UILabel mLabel;
    public int mFragmentNum = 0;

    List<GameObject> mFragmentGroup = new List<GameObject>();

    public GameObject BottleTop;
    public UISprite BottleBody;
    public List<UITweener> BottleTopTweenGroup;
	
	public UISprite FragEffectSprite;

    void Awake()
    {
        BottleTopTweenGroup.Add(BottleTop.GetComponent<TweenPosition>());
        BottleTopTweenGroup.Add(BottleTop.GetComponent<TweenRotation>());
        BottleTopTweenGroup.Add(BottleTop.GetComponent<TweenAlpha>());
        BottleTop.GetComponent<UISprite>().alpha = 0;
		
		BottleBody.alpha = 0;
    }

	public void Init(LevelBottleData data)
	{
        mLevelBottledata = data;
		BottleBody.spriteName = data.mBottleType.ToString();
	}

	public bool FlyFragmentToBottle(SingleFragment fragment)
	{
        bool flag = false;
        if (fragment.mFragmentType==mLevelBottledata.mFragmentType&&mFragmentNum < mLevelBottledata.GetFragNum)
        {
            mFragmentNum++;
            flag = true;
        }
		return flag;
	}

    void OnBottleFull()
    {
        GameManager.Instance.mGameDataManager.SaveOwnBottle(mLevelBottledata.mBottleType);
        BottleTopTweenGroup.ApplyAllItem(C=>C.enabled = true);
    }

	public void OnFragmentFlyComplete(object obj)
	{
//        Debug.Log("Add"+obj);
        mFragmentGroup.Add(obj as GameObject);
        if (mFragmentNum == mLevelBottledata.GetFragNum)
        {
            OnBottleFull();
        }
		PlayOnFragmentInAnim ();
	}
	
	public Animator manim;
	[ContextMenu("Do")]
	public void PlayOnFragmentInAnim()
	{
		if (BottleBody.alpha == 0) {
			TweenAlpha.Begin (BottleBody.gameObject, 1f, 1);
		}
		manim.SetBool ("BeginShow",true);
		ShowFlagEffect ();
	}

	void ShowFlagEffect()
	{
		TweenAlpha.Begin (FragEffectSprite.gameObject, 0.5f, 0, 0.5f, (obj) => {
			TweenAlpha.Begin(FragEffectSprite.gameObject,0.5f,0.5f,0,null);
		});
	}

	void LateUpdate()
	{
		manim.SetBool ("BeginShow",false);
	}

    public void DestroySelf()
    {
        mFragmentGroup.ApplyAllItem(C=>{
            GameObject.Destroy(C);
//            Debug.Log("Destroy:"+C);
        });
        
        GameObject.Destroy(gameObject);
    }

    public bool IsFull()
    {
        return mFragmentNum >= mLevelBottledata.GetFragNum;
    }
}

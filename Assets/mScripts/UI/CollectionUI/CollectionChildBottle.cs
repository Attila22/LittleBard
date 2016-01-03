using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionChildBottle : MonoBehaviour {

	public DragChild mDragChild;
    CollectionUI mParent;

	public BottleType mType;
	public UISprite mSprite;

	GameObject BottleFlyEffectPrefab { get { return Resources.Load("UI/GamePlayUI/Bottle/FragmentFlyEffect") as GameObject; } }
	
	List<GameObject> mBottleObjGroup = new List<GameObject>();

	public void Init(BottleType type,CollectionUI parent)
	{
        string key = "";
        mParent = parent;
		mType = type;
		switch (type)
		{
            case BottleType.OIL_STREET:
                key = "OIL STREET";
                break;
            case BottleType.OIL_STREET_EVENTS:
                key = "OIL STREET EVENTS";
                break;
            case BottleType.PMQ:
                key = "PMQ";
                break;
            case BottleType.PMQ_EVENTS:
                key = "PMQ EVENTS";
                break;
            case BottleType.PMQ_GALLERY:
                key = "PMQ GALLERY";
                break;
            case BottleType.PMQ_RESTAURANT:
                key = "PMQ RESTAURANT";
                break;
            case BottleType.VA:
                key = "VA!";
                break;
        }
        mSprite.spriteName = key;
        mDragChild.Init(this);
		SetMyFragments(Random.Range(1,4),(FragmentType)Random.Range(1,4));
	}

    public void OnClickSelf()
    {
        mParent.OnBottleClick(this);
    }
	
	
	public void SetMyFragments(int num,FragmentType type)
	{
		mBottleObjGroup.ApplyAllItem(C=>GameObject.Destroy(C.gameObject));
		mBottleObjGroup.Clear();
		for(int i = 0;i<num;i++)
		{
			GameObject newFrag = NGUITools.AddChild(gameObject, BottleFlyEffectPrefab);
			BottleFragRandomFly randomFly = newFrag.AddComponent<BottleFragRandomFly>();
			float scale = 0.5f+((int)type)*0.5f;
			newFrag.transform.localScale*= scale/2;
			
			int randomX = Random.Range(20, 30);
			int randomY = Random.Range(20, 30);
			Vector3 targetPos = new Vector3(Random.Range(-1f, 1f) > 0 ? randomX : -randomX, Random.Range(-1f, 1f) > 0 ? randomY : -randomY, 0);
			randomFly.transform.localPosition = targetPos;
			randomFly.SetFlyPosRange(20,30);
			randomFly.mSPrite.depth = 3;
			randomFly.SetActive(true);
			mBottleObjGroup.Add(newFrag);
		}
	}
}

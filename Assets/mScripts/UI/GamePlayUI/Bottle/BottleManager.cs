using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BottleManager : MonoBehaviour  {


	public GameObject Root;
    GameObject BottlePrefab { get { return Resources.Load("UI/GamePlayUI/Bottle/SinlgeBottle") as GameObject; } }
    GameObject BottleFlyEffectPrefab { get { return Resources.Load("UI/GamePlayUI/Bottle/FragmentFlyEffect") as GameObject; } }
	float ItemDistance = 300f;

	Camera mUICamera;
	Camera mGameCamera;

    List<SingleBottle> mBottleGroup = new List<SingleBottle>();

	void Awake()
	{
		mUICamera = UICamera.mainCamera;
		mGameCamera = Camera.main;
	}

	public void SetBottle(int level)
	{
        LevelData mlevelData = GameManager.Instance.mGameDataManager.GetLevelData(level);
        CleanUpMBottle();
        InitBottle(mlevelData.BottleGetDataGroup);
	}

	void InitBottle(List<LevelBottleData> bottleData)
	{
        for (int i = 0; i < bottleData.Count; i++)
		{
			SingleBottle bottle = NGUITools.AddChild(Root,BottlePrefab).GetComponent<SingleBottle>();
            bottle.Init(bottleData[i]);
            mBottleGroup.Add(bottle);
			bottle.transform.localPosition = new Vector3(ItemDistance*i,20,0);
		}
        Root.transform.localPosition = new Vector3(-ItemDistance / 2 * (bottleData.Count - 1), -450, 0);

	}

    public void CleanUpMBottle()
    {
        mBottleGroup.ApplyAllItem(C=>C.DestroySelf());
        mBottleGroup.Clear();
        foreach (Transform child in Root.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public bool FlyToMyBottle(SingleFragment fragment)
    {
        bool flag = false;
        for (int i = 0; i < mBottleGroup.Count; i++)
        {
            SingleBottle bottle = mBottleGroup[i];
            {
                if (bottle.FlyFragmentToBottle(fragment))
                {
                    flag = true;
                    FlyFragmentToBottle(fragment, bottle);
                    break;
                }
            }
        }
        return flag;
    }

    void FlyFragmentToBottle(SingleFragment fragment, SingleBottle bottle)
    {
//        Debug.Log("FlyFragment");
        Vector2 screenPos = mGameCamera.WorldToScreenPoint(fragment.transform.position);
        Vector2 targetPos = mUICamera.ScreenToWorldPoint(screenPos);
        GameObject bottleEffect = NGUITools.AddChild(Root, BottleFlyEffectPrefab);
        bottleEffect.transform.position = targetPos;
        bottleEffect.transform.parent = bottle.transform;
        FragmentFlyToBottleEffect flyEffect =  bottleEffect.GetComponent<FragmentFlyToBottleEffect>();
        flyEffect.Init(fragment.mFragmentType);
        flyEffect.FlyToPos(Vector3.zero, (obj) => {
            bottle.OnFragmentFlyComplete(obj);
            StartCoroutine(CheckIsWin());
            EventManager.Instance.TriggerEvent(GameEventType.BeginnerGuideEvent, BeginnerGuideEventType.OnFragmentEnterBottle);
        },flyEffect.gameObject);
        
    }

	public GameOverData GetGameOverData()
	{
		GameOverData getdata = new GameOverData();
		mBottleGroup.ApplyAllItem(C=>{
			switch(C.mLevelBottledata.mFragmentType)
			{
			case FragmentType.Big:
				getdata.BigFragmentNum+=C.mFragmentNum;
				break;
			case FragmentType.Medium:
				getdata.MediumFragmentNum+=C.mFragmentNum;
				break;
			case FragmentType.Small:
				getdata.SmallFragmentNum+=C.mFragmentNum;
				break;
			}
		});
		return getdata;
	}
    
    IEnumerator CheckIsWin()
    {
		yield return new WaitForSeconds (1);
        bool isWin = true;
        mBottleGroup.ApplyAllItem(C => {
            if (!C.IsFull())
                isWin = false;
        });
        if (isWin)
        {
            GamePlaySceneMgr.Instance.OnGameOver(true);
        }
    }

}

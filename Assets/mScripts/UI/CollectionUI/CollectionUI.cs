using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class CollectionUI : BaseUI {

	public DragParent mDragParent;
	GameObject BottlePrefab{get{return Resources.Load("UI/CollectionUI/Bottle")as GameObject;}}
    public GameObject mBackButton;
    public GameObject InfoButton;
    public BottleInfoShow mBottleInfoShow;

    public string InfoStr = "";

	void Start()
	{
//        List<BottleType> typeGroup = new List<BottleType>();
//        typeGroup.Add(BottleType.OIL_STREET);
//        typeGroup.Add(BottleType.VA);
//        typeGroup.Add(BottleType.PMQ_RESTAURANT);
//        typeGroup.Add(BottleType.PMQ_GALLERY);
//        typeGroup.Add(BottleType.PMQ_EVENTS);
//        UpdateMBottles(typeGroup);

        UIEventListener.Get(mBackButton).onClick = OnBackBtnClick;
        UIEventListener.Get(InfoButton).onClick = OnInfoBtnClick;
	}

    void OnInfoBtnClick(GameObject go)
    {
        mBottleInfoShow.ShowRoleInfo(InfoStr);
    }

	public override void Show ()
	{
        List<int> mOwnBottles = GameManager.Instance.mGameDataManager.GetOwnBottle();
        List<BottleType> typeGroup = new List<BottleType>();
        mOwnBottles.ApplyAllItem(C => typeGroup.Add((BottleType)C));
        UpdateMBottles(typeGroup);
	}

	void UpdateMBottles(List<BottleType> types)
	{
		mDragParent.DragSlotGroup.ApplyAllItem(C=>C.CleanUpmChild());
		for(int i = 0;i<types.Count;i++)
		{
			CollectionChildBottle newBottle = NGUITools.AddChild(mDragParent.gameObject,BottlePrefab).GetComponent<CollectionChildBottle>();
            newBottle.Init(types[i], this);
			newBottle.transform.position = mDragParent.DragSlotGroup[i].transform.position;
			mDragParent.DragSlotGroup[i].AddDragChildToSlot(newBottle.mDragChild);
		}
	}

    void OnBackBtnClick(GameObject go)
    {
        Close();
        UIManager.Instance.Show(UIType.MainUI);
    }

    public void OnBottleClick(CollectionChildBottle bottle)
    {
        mBottleInfoShow.Show(bottle);
    }

	public override void Close ()
	{
		gameObject.SetActive(false);
	}
	public override void OnMsg (MsgType msgType, object msg)
	{

	}
}

using UnityEngine;
using System.Collections;

public class DragChild : MonoBehaviour {

	Camera mUICamera ;
	DragParent mDragParent;
	public DragSlot mSlot;
    CollectionChildBottle mBottle;

    bool isMove = false;

	void Start()
	{
		mUICamera = UICamera.mainCamera;
		GetDragParent(transform);
		fromPos = transform.position;
	}

    public void Init(CollectionChildBottle bottle)
	{
        mBottle = bottle;
	}

	void GetDragParent(Transform child)
	{
		mDragParent = child.GetComponent<DragParent>();
		if(mDragParent == null)
		{
			GetDragParent(child.parent);
		}
	}


	void OnPress(bool pressed)
	{
        if (pressed)
        {
            isMove = false;
		}else
		{
			if(isMove)
			{
				DragSlot slot = mDragParent.GetInSlot(this);
				if(slot!=null)
				{
					FlyToSlot(slot);
				}else
				{
					FlyBack();
				}
			}
            if (!isMove)
            {
                mBottle.OnClickSelf();
            }
		}
	}

	void OnDrag(Vector2 dir)
	{
        isMove = true;
		Vector2 pos = mUICamera.ScreenToWorldPoint(Input.mousePosition);
		transform.position = pos;
		//mDragParent.GetInSlot(this);
	}

	public void FlyToSlot(DragSlot slot)
	{
		mSlot.mDragChild = null;
		if(slot.mDragChild!=null)
		{
//			DragSlot mslot = mSlot;
			mSlot.mDragChild = null;
			slot.mDragChild.FlyToSlot(mSlot);
		}
		mSlot = slot;
		slot.AddDragChildToSlot(this);
		flyTime = 0;
		isFly = true;
		fromPos = transform.position;
		toPos = slot.transform.position;
	}

	public void FlyBack()
	{
		flyTime = 0;
		isFly = true;
		fromPos = transform.position;
		toPos = mSlot.transform.position;
	}

	bool isFly = false;
	float flyTime = 0;
	Vector3 fromPos;
	Vector3 toPos;
	float flyTimeDis = 0.5f;
	void Update()
	{
		if(isFly)
		{
			flyTime+=Time.deltaTime;
			if(flyTime>flyTimeDis)
			{
				flyTime = flyTimeDis;
				isFly = false;
			}
			transform.position = Vector3.Lerp(fromPos,toPos,flyTime/flyTimeDis);

		}
	}

}

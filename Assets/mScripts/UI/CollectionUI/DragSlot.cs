using UnityEngine;
using System.Collections;

public class DragSlot : MonoBehaviour {


	public DragParent mDragParent;
	public DragChild mDragChild;
	
//	void Awake()
//	{
//		GetDragParent(transform);
//		mDragParent.DragSlotGroup.Add(this);
//	}
//	
//	void GetDragParent(Transform child)
//	{
//		mDragParent = child.GetComponent<DragParent>();
//		if(mDragParent == null)
//		{
//			GetDragParent(child.parent);
//		}
//	}

	public void AddDragChildToSlot(DragChild child)
	{
		mDragChild = child;
		child.mSlot = this;
	}

	public void CleanUpmChild()
	{
		if(mDragChild!=null)
			GameObject.Destroy(mDragChild.gameObject);
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragParent : MonoBehaviour {


	public List<DragSlot> DragSlotGroup = new List<DragSlot>();

	public DragSlot GetInSlot(DragChild dragChild)
	{
		DragSlot getSlot  = null;
		for(int i = 0;i<DragSlotGroup.Count;i++)
		{
			DragSlot C = DragSlotGroup[i];
			float dis = Vector3.Distance(dragChild.transform.position,C.transform.position);
//			Debug.Log(dis);
			if(dis<0.1f)
			{
				getSlot = C;
				break;
			}
		}
		return getSlot;
	}


	[ContextMenu("GetSlot")]
	void GetSlot()
	{
		foreach(Transform child in transform )
		{
			DragSlotGroup.Add(child.GetComponent<DragSlot>());
		}
	}

}

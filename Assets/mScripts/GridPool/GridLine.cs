using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridLine {


    GameObject SingleGridPrefab { get { return Resources.Load("UI/FragmentSelect/SingleGrid") as GameObject; } }
	GridPoolManager mParent;

	public Dictionary<int,SingleGrid> GridGroup =new Dictionary<int, SingleGrid>();


	public void Init(GridPoolManager parent,int mIndex)
	{
		mParent = parent;
		for(int i = 0;i<GridPoolManager.RowNum;i++)
		{
			SingleGrid newRow = GetSingleGrid();
			newRow.Init(i,mIndex);
			GridGroup[i]=newRow;
		}
	}

	SingleGrid GetSingleGrid()
	{
		GameObject newObj = NGUITools.AddChild (mParent.Root.gameObject,SingleGridPrefab);
		return newObj .GetComponent<SingleGrid>();
	}


}

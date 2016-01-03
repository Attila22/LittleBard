using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// </summary>
public class GridSelectMgr{


	List<SingleGrid> DrawGridGroup = new List<SingleGrid>();
	FragmentSelectUI mParent;

	public GridSelectMgr(FragmentSelectUI parent)
	{
		mParent = parent;
	}

	public void OnSelectGridGroup(List<SingleGrid> gridGroup)
	{
		selectGroupData.Clear ();
		
		DrawGridGroup = gridGroup;
		for (int i = 0; i<DrawGridGroup.Count; i++) 
		{
			GetRoundGridGroup(DrawGridGroup[i]);
			for(int j=0;j<RoundGridGroup.Length;j++)
			{
				if(RoundGridGroup[j]!=null&&!DrawGridGroup.Contains(RoundGridGroup[j]))
				{
					GridSelectGroupData mdata = GetSelectGroupData(RoundGridGroup[j]);
					if(!mdata.SelectGridGroup.Contains(RoundGridGroup[j]))
					{
						GetNearGridGroup(RoundGridGroup[j],mdata);
					}
				}
			}
		}

		for(int c = 0;c<selectGroupData.Count;c++)
		{
			GridSelectGroupData child  = selectGroupData[c];
			if(child.isSlect)
			{
				for(int i = 0;i<DrawGridGroup.Count;i++)
				{
					for (int s=0; s<4;s++) 
					{
						SingleGrid mGrid = GridPoolManager.Instance.GetGrid (DrawGridGroup[i], (Side)s);
						if(child.SelectGridGroup.Contains(mGrid)&&!child.SelectGridGroup.Contains(DrawGridGroup[i]))
						{
							child.SelectGridGroup.Add(DrawGridGroup[i]);
						}
					}
				}
			}
		}

//		Debug.Log("CheckNum:"+CheckNum);
		if(selectGroupData.Count>0)
		{
			mParent.OnSelectGridGroup(selectGroupData);
		}
	}

	void AddRounDrawToSelectGroup(GridSelectGroupData mData)
	{
		for (int i = mData.SelectGridGroup.Count-1;i>=0; i--) {
			for (int s=0; s<4; s++) 
			{
				SingleGrid mGrid = GridPoolManager.Instance.GetGrid (mData.SelectGridGroup[i], (Side)s);
				if(DrawGridGroup.Contains(mGrid)&&!mData.SelectGridGroup.Contains(mGrid))
					mData.SelectGridGroup.Add(mGrid);
			}
		}
	}

	SingleGrid[] RoundGridGroup = new SingleGrid[4];
	void GetRoundGridGroup(SingleGrid grid)
	{
		for (int i=0; i<4; i++) 
		{
			SingleGrid mGrid = GridPoolManager.Instance.GetGrid (grid, (Side)i);
			RoundGridGroup[i] = mGrid;
		}
	}
	
	List<GridSelectGroupData> selectGroupData = new List<GridSelectGroupData> ();
	GridSelectGroupData GetSelectGroupData(SingleGrid grid)
	{
		for (int s = 0; s<selectGroupData.Count; s++) {
			GridSelectGroupData data = selectGroupData[s];
			if(data.SelectGridGroup.Contains(grid))
				return data;
		}
		GridSelectGroupData newData = new GridSelectGroupData ();
		selectGroupData.Add (newData);
		return newData;
	}

//	int CheckNum = 0;
	void GetNearGridGroup(SingleGrid grid,GridSelectGroupData gridSelectGroupData)
	{
		for (int i=0; i<4; i++) 
		{
			SingleGrid mGrid = GridPoolManager.Instance.GetGrid (grid, (Side)i);
			if(mGrid!=null)
			{
				if(!DrawGridGroup.Contains(mGrid)&&!gridSelectGroupData.SelectGridGroup.Contains(mGrid))
				{
					gridSelectGroupData.SelectGridGroup.Add(mGrid);
					GetNearGridGroup(mGrid,gridSelectGroupData);
//					CheckNum++;
				}
			}else
				gridSelectGroupData.isSlect = false;
		}
	}
}


public class GridSelectGroupData
{
	public bool isSlect = true;
	public List<SingleGrid> SelectGridGroup = new List<SingleGrid>();
}
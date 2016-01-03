using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// </summary>
public class GridDrawMgr {

	List<SingleGrid> mGridGroup = new List<SingleGrid>();

	SingleGrid LastTempDrawGrid;

    FragmentSelectUI mParent;

	public GridDrawMgr(FragmentSelectUI parent)
    {
        mParent = parent;
    }

	public void BeginDrawGrid()
	{
		mGridGroup.Clear();
	}

	public void EndDrawGrid(SingleGrid grid)
	{
		if (mGridGroup.Count == 0)
				return;
		if (grid!=null&&grid != mGridGroup [0]) 
		{
			float dis = Vector3.Distance(mGridGroup [0].transform.position,grid.transform.position);
			if(dis<0.5f)
			{
				AddNearGridGroupToMyGroup (mGridGroup [0]);
			}
		}
		for (int i = 0; i<mGridGroup.Count; i++) 
		{
			mGridGroup[i].SetLable("");
		}
//		NGUIDebug.Log("SleectGrid:"+mGridGroup.Count);
        mParent.mGridSelectMgr.OnSelectGridGroup(mGridGroup);
		LastTempDrawGrid = null;
	}

	public void OnDrawGrid(SingleGrid grid)
	{
		if (grid==null||(LastTempDrawGrid != null && LastTempDrawGrid == grid)) 
		{
			return;
		}
		if(IsNearGrid(grid))
		{
			AddGridToMyGroup(grid);
		}
		else
		{
			AddNearGridGroupToMyGroup(grid);
		}
	}

	void AddGridToMyGroup(SingleGrid grid)
	{
		LastTempDrawGrid = grid;
		if (!mGridGroup.Contains (grid)) 
		{
			mGridGroup.Add (grid);
			grid.SetLable(mGridGroup.IndexOf(grid).ToString());
		}

	}

	bool IsNearGrid(SingleGrid grid)
	{
		bool flag = true;
		if(LastTempDrawGrid!=null)
			flag = Mathf.Abs(grid.mRowIndex-LastTempDrawGrid.mRowIndex)<=1&&Mathf.Abs(grid.mLineIndex-LastTempDrawGrid.mLineIndex)<=1;
		return flag;
	}

	void AddNearGridGroupToMyGroup(SingleGrid grid)
	{
		SingleGrid getGrid = LastTempDrawGrid;
		while (getGrid!=grid) 
		{
			getGrid = GetNextGrid(getGrid,grid);
			AddGridToMyGroup(getGrid);
		}

	}

	Dictionary<SingleGrid,float> GridDisGroup = new Dictionary<SingleGrid,float>();
	SingleGrid GetNextGrid(SingleGrid fromGrid,SingleGrid targetGrid)
	{
		GridDisGroup.Clear ();
		AddGridDisToGroup (fromGrid,targetGrid,Side.Down);
		AddGridDisToGroup (fromGrid,targetGrid,Side.Up);
		AddGridDisToGroup (fromGrid,targetGrid,Side.Left);
		AddGridDisToGroup (fromGrid,targetGrid,Side.Right);
		SingleGrid minDisGrid = null;
		float minDis = 10000f;
		foreach (var child in GridDisGroup) 
		{
			if(child.Value<minDis)
			{
				minDis = child.Value;
				minDisGrid = child.Key;
			}
		}
		return minDisGrid;
	}

	void AddGridDisToGroup(SingleGrid fromGrid,SingleGrid targetGrid,Side side)
	{
		SingleGrid mSideGrid = GridPoolManager.Instance.GetGrid (fromGrid, side);
		if (mSideGrid != null)
			GridDisGroup [mSideGrid] = Vector2.Distance (new Vector2 (mSideGrid.mRowIndex, mSideGrid.mLineIndex), new Vector2 (targetGrid.mRowIndex, targetGrid.mLineIndex));
	}
}

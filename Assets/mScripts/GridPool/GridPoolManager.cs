using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GridPoolManager:MonoBehaviour {

    public GameObject Root { get; private set; }
	static GridPoolManager instance;
	public static GridPoolManager Instance{
		get
		{
//			if(instance==null)
//				instance = new GridPoolManager();
			return instance;
		}
	}

	public const int LineNum = 17;
	public const int RowNum = 22;

	Dictionary <int,GridLine> mGridLineGroup = new Dictionary<int, GridLine>();

	void Awake()
	{
		instance = this;
	}

	public void Init(GameObject root)
	{
        this.Root = root;
		for(int i = 0;i<LineNum;i++)
		{
			GridLine newLine = new GridLine();
			newLine.Init(this,i);
			mGridLineGroup[i]=newLine;
		}
	}

	public SingleGrid GetGrid(SingleGrid grid,Side side)
	{
		int rowIndex = grid.mRowIndex;
		int lineIndex = grid.mLineIndex;
		switch (side)
		{
		case Side.Down:
			rowIndex--;
			break;
		case Side.Left:
			lineIndex--;
			break;
		case Side.Right:
			lineIndex++;
			break;
		case Side.Up:
			rowIndex++;
			break;
		}
		return GetGrid(rowIndex,lineIndex);
	}

	public SingleGrid GetGrid(int rowIndex,int lineIndex)
	{
		SingleGrid getGrid = null;

		GridLine mline = null;
		if(mGridLineGroup.TryGetValue(lineIndex,out mline))
		{
			mline.GridGroup.TryGetValue(rowIndex,out getGrid);
		}
		return getGrid;
	}

}

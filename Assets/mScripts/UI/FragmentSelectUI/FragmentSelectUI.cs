using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FragmentSelectUI : BaseUI{

    public GameObject Root;
    public GridPoolManager mGridPoolManager { get; private set; }
    public GridDrawMgr mGridDrawMgr { get; private set; }
    public GridSelectMgr mGridSelectMgr { get; private set; }
	public MouseTrail mMouseTrail {get;private set;}

	GameObject MouseTrailPrefab{get{return Resources.Load("Trail/MouseTrail")as GameObject;}}

	BottleManager bottleManager;
	public BottleManager mBottleManager
	{
		get{
            if (bottleManager == null)
            {
                GamePlayUI gamePlayUI = UIManager.Instance.GetUI(UIType.GamePlay) as GamePlayUI;
                bottleManager = gamePlayUI.mBottleManager;
            }
			return bottleManager;
		}
	}

	Camera mUICamera;
	Camera mGameCamera;

	void Awake()
    {
		mUICamera = UICamera.mainCamera;
		mGameCamera = Camera.main;
        mGridPoolManager = gameObject.AddComponent <GridPoolManager>();
		mGridDrawMgr = new GridDrawMgr(this);
		mGridSelectMgr = new GridSelectMgr(this);
		mMouseTrail = (GameObject.Instantiate(MouseTrailPrefab)as GameObject).GetComponent<MouseTrail>();
		mGridPoolManager.Init(Root);
	}

	public override void Show ()
	{
        gameObject.SetActive(true);
	}

    public override void OnMsg(MsgType msgType,object msg)
	{
	}

	public override void Close ()
	{
        gameObject.SetActive(false);
	}

	
	Ray ray;
	RaycastHit hit;
	List<SingleFragment> selectFragment = new List<SingleFragment>();
	public void OnSelectGridGroup(List<GridSelectGroupData> gridGroup)
	{
		for(int i = 0;i< gridGroup.Count;i++)
		{
			if(gridGroup[i].isSlect)
			{
				selectFragment.Clear();
				for(int child =0;child< gridGroup[i].SelectGridGroup.Count;child++)
				{
					for(int j = 0;j<gridGroup[i].SelectGridGroup[child].rayPosGroup.Length;j++)
					{
						Vector2 screenPos = mUICamera.WorldToScreenPoint(gridGroup[i].SelectGridGroup[child].rayPosGroup[j].position);
//						Debug.Log(screenPos);
						ray = mGameCamera.ScreenPointToRay(screenPos);
						if (Physics.Raycast (ray, out hit, 100)&&hit.collider.name=="Fragment(Clone)") 
						{
							SingleFragment frag= hit.collider.GetComponent<SingleFragment>(); 
							if(frag!=null&&!selectFragment.Contains(frag))
							{
								frag.gameObject.GetComponent<Renderer>().material.color = Color.green;
								selectFragment.Add(frag);
								if(frag!= selectFragment[0]||!mBottleManager.FlyToMyBottle(frag))
								{
									frag.FlyOut();
								}else
								{
									frag.SetRandomMoveActive(false);
									TweenScale.Begin(frag.gameObject,0.5f,frag.transform.localScale,Vector3.zero,(obj)=>{
										GamePlaySceneMgr.Instance.mFragmentManager.DestroyFragment(frag);}
									);
								}
							}
						}
					}
				}
			}
		}
	}

}

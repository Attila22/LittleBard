using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenTouchManager : MonoBehaviour {
	public enum TouchType
	{
		None,
		GetFragment,
		DrawGrid,
		MoveScreen,
	}

	float mTouchTime = 0;

	TouchType mTouchType = TouchType.None;

    List<TouchType> DisableTouchTypeGroup = new List<TouchType>();
	
	Ray ray;
	RaycastHit hit;
	Camera mUICamera;
	Camera gameCam;
    Camera mGameCamera
    {
        get
        {
            if (gameCam == null)
                gameCam = Camera.main;
            return gameCam;
        }
    }
	
	FragmentSelectUI fragmentSelectUI;
	FragmentSelectUI mFragmentSelectUI{
		get
		{
			if(fragmentSelectUI == null)
				fragmentSelectUI = UIManager.Instance.GetUI (UIType.FragmentSelectUI)as FragmentSelectUI;
			return fragmentSelectUI;
		}
		
	}
	
	void Start()
	{
		mUICamera = UICamera.mainCamera;
	}

	Vector3 tempPos ;
	float BeginDrawGridTime = 0;
	void Update () {
		int touchCount = Input.touchCount;
		if (touchCount == 0)
			return;
        else if (touchCount == 1)
        {
            Touch mTouch = Input.GetTouch(0);
            switch (mTouch.phase)
            {
                case TouchPhase.Began:
                    mTouchType = TouchType.None;
                    tempPos = mTouch.position;
                    break;
                case TouchPhase.Stationary://静止
                case TouchPhase.Moved:
                    if (mTouchType == TouchType.None)
                    {
                        if (IsMove(tempPos, mTouch.position))
                        {
                            mTouchType = TouchType.DrawGrid;
                        }
                        else
                        {
                            mTouchTime += Time.deltaTime;
                            if (mTouchTime > 0.5f)
                            {
                                mTouchType = TouchType.GetFragment;
                            }
                        }

                    }
                    break;
                case TouchPhase.Ended:
                    break;
            }
        }
        else
        {
            if (mTouchType == TouchType.None)
                mTouchType = TouchType.MoveScreen;
        }


        switch (mTouchType)
        {
            case TouchType.DrawGrid:
                if (!DisableTouchTypeGroup.Contains(mTouchType))
                    OnDrawGrid();
                break;
            case TouchType.GetFragment:
                if (!DisableTouchTypeGroup.Contains(mTouchType))
                    OnGetFragment();
                break;
            case TouchType.MoveScreen:
                if (!DisableTouchTypeGroup.Contains(mTouchType))
                    OnMoveScreen();
                break;
            case TouchType.None:
                break;
        }
	}

	bool IsBeginDrawGrid = false;
	void OnDrawGrid()
	{
		if(!IsBeginDrawGrid)
		{
			IsBeginDrawGrid = true;
			mFragmentSelectUI.mGridDrawMgr.BeginDrawGrid();
			mFragmentSelectUI.mMouseTrail.OnFingerDown();
		}
		Touch mTouch = Input.GetTouch (0);
		switch (mTouch.phase) 
		{
		case TouchPhase.Ended:
			mTouchType = TouchType.None;
			BeginDrawGridTime = 0;
			IsBeginDrawGrid = false;
			mFragmentSelectUI.mGridDrawMgr.EndDrawGrid(GetUICameraRayHit(Input.mousePosition));
			mFragmentSelectUI.mMouseTrail.OnFingerUp();
			break;
		case TouchPhase.Moved:
			mFragmentSelectUI.mGridDrawMgr.OnDrawGrid(GetUICameraRayHit(Input.mousePosition));
			mFragmentSelectUI.mMouseTrail.OnFingerTouch(mTouch.position);
			break;
		default:
			break;
		}
	}

	Vector2 FragTempPos;
	bool isBeginGetFragment = false;
	void OnGetFragment()
	{
		Touch mTouch = Input.GetTouch (0);
		if (!isBeginGetFragment) {
			isBeginGetFragment = true;
//			Debug.Log ("BeginTouchFragment");
			GamePlaySceneMgr.Instance.mFragmentTouchMgr.BeginTouchScreen (GetGameCameraRayHitPos (mTouch.position));
			//mFragmentSelectUI.mMouseTrail.OnFingerDown();
			FragTempPos = mTouch.position;
		}
		switch (mTouch.phase) 
		{
		case TouchPhase.Began:
			break;
		case TouchPhase.Canceled:
			break;
		case TouchPhase.Ended:
//			Debug.Log("EndTouchFragment");
			mTouchType = TouchType.None;
			isBeginGetFragment = false;
			mTouchTime = 0;
			GamePlaySceneMgr.Instance.mFragmentTouchMgr.EndTouchScreen();
			//mFragmentSelectUI.mMouseTrail.OnFingerUp();
			break;
		case TouchPhase.Moved:
		case TouchPhase.Stationary:
			//mFragmentSelectUI.mMouseTrail.OnFingerTouch(FragTempPos);
			break;
		}
	}

	
	bool isBeginMoveScreen = false;
	void OnMoveScreen()
	{
		Touch mTouch = Input.GetTouch (0);
		switch (mTouch.phase) 
		{
		case TouchPhase.Ended:
			isBeginMoveScreen = false;
//			Debug.Log("EndMoveScreen");
			mTouchType = TouchType.None;
			GamePlaySceneMgr.Instance.mGameCamraRotator.OnMouseUp();
			break;
		case TouchPhase.Moved:
		case TouchPhase.Stationary:
			if(!isBeginMoveScreen)
			{
				isBeginMoveScreen = true;
//				Debug.Log("BeginMoveScreen");
				GamePlaySceneMgr.Instance.mGameCamraRotator.OnMouseDown();
			}else
			{
				GamePlaySceneMgr.Instance.mGameCamraRotator.OnMouse();
			}
			break;
		default:
			break;
		}
	}

    public void SetTouchTypeDisable(TouchType type)
    {
        if (!DisableTouchTypeGroup.Contains(type))
        {
            DisableTouchTypeGroup.Add(type);
        }
    }
    public void SetTouchTypeActive(TouchType type)
    {
        if (DisableTouchTypeGroup.Contains(type))
        {
            DisableTouchTypeGroup.Remove(type);
        }
    }
    public void CleanUpDisableTouchType()
    {
        DisableTouchTypeGroup.Clear();
    }

	
	SingleGrid GetUICameraRayHit(Vector3 pos)
	{
		SingleGrid getObj = null;
		ray = mUICamera.ScreenPointToRay (pos);
		if (Physics.Raycast (ray, out hit, 100)) 
		{
			getObj = hit.collider.gameObject.GetComponent<SingleGrid>();
		}
		return getObj;
	}
	
	Vector3 GetGameCameraRayHitPos(Vector3 pos)
	{
		Vector3 getPos = Vector3.zero;
		ray = mGameCamera.ScreenPointToRay(pos);
		if (Physics.Raycast(ray, out hit, 1000))
		{
			getPos = hit.point;
		}
		return getPos;
	}
	
	bool IsMove(Vector2 pos0,Vector2 pos1)
	{
		float distance = Vector3.Distance(pos0,pos1);
		return distance>5;
	}

}

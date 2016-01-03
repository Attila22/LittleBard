using UnityEngine;
using System.Collections;

public class CameraRayHit : MonoBehaviour {
	public enum TouchType
	{
		None,
		FragmentTouch,
		DrawGrid,
	}


	Ray ray;
	RaycastHit hit;
	Camera mUICamera;
    Camera mGameCamera;

	Vector2 BeginTouchFingerPos;
    Vector2 LastFingerPos;
    Vector2 TempFingerPos;

	bool IsTouchActive = true;
	TouchType mTouchType = TouchType.None;
	float TouchTime = 0f;

	FragmentSelectUI fragmentSelectUI;
	FragmentSelectUI mFragmentSelectUI{
		get
		{
			if(fragmentSelectUI == null)
				fragmentSelectUI = UIManager.Instance.GetUI (UIType.FragmentSelectUI)as FragmentSelectUI;
			return fragmentSelectUI;
		}

	}

	void Awake()
	{
		mUICamera = UICamera.mainCamera;
        mGameCamera = Camera.main;
	}

	
	bool IsActive = true;
	void Update () {

//		if (Input.GetMouseButtonDown (0)) 
//		{
//			TouchTime = 0;
//			mTouchType = TouchType.None;
//			IsTouchActive = true;
//			BeginTouchFingerPos = TempFingerPos = LastFingerPos = Input.mousePosition;
//			mFragmentSelectUI.mGridDrawMgr.BeginDrawGrid();
//		}
//		if (Input.GetMouseButtonUp (0))
//        {
//			if(mTouchType == TouchType.None&&!IsMove(BeginTouchFingerPos,Input.mousePosition))
//			{
//				mTouchType = TouchType.FragmentTouch;
//			}
//			switch (mTouchType)
//			{
//			case TouchType.DrawGrid:
//				mFragmentSelectUI.mGridDrawMgr.EndDrawGrid(GetUICameraRayHit(Input.mousePosition));
//				break;
//			case TouchType.FragmentTouch:
//				GamePlaySceneMgr.Instance.mFragmentTouchMgr.OnTouchScreen(GetGameCameraRayHitPos(Input.mousePosition));
//				break;
//			}
//		}
//		if (Input.GetMouseButton (0))
//		{
//			if(IsMove(TempFingerPos,Input.mousePosition))
//			{
//				mTouchType = TouchType.DrawGrid;
//				TempFingerPos = Input.mousePosition;
//				mFragmentSelectUI.mGridDrawMgr.OnDrawGrid(GetUICameraRayHit(Input.mousePosition));
//			}
//		}
	}

	TouchType GetTouchType()
	{
		TouchType getType = TouchType.DrawGrid;

		return getType;
	}

    bool IsMove(Vector2 pos0,Vector2 pos1)
    {
		float distance = Vector3.Distance(pos0,pos1);
//		Debug.Log(distance);
		return distance>20;
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
}

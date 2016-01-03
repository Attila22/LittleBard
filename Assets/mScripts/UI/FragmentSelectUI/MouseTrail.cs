using UnityEngine;
using System.Collections;

public class MouseTrail : MonoBehaviour {

	TrailRenderer mTrailRenderer ;
	Vector2 tempPos;
	Camera mUICamera;

	public ParticleSystem mParticleSystem ;

	void Awake()
	{
		mUICamera = UICamera.mainCamera;
		OnFingerUp();
//		mTrailRenderer = gameObject.GetComponent<mTrailRenderer >();
	}

	bool IsActive = true;

	int MaxPNum = 80;
	int MinPNum= 0;


	public void OnFingerDown()
	{
		IsActive = true;
		tempPos = Input.mousePosition;
		transform.position = mUICamera.ScreenToWorldPoint(tempPos);
	}

	public void OnFingerUp()
	{
		mParticleSystem.emissionRate = MinPNum;
		IsActive = false;
	}

	public void OnFingerTouch(Vector2 pos)
	{
		if(!IsActive)
			return;
		if(isMove(tempPos,pos))
		{
			Vector2 newPos = pos;
			if(tempPos!=newPos)
			{
				tempPos = newPos;
				Vector3 WordPos = mUICamera.ScreenToWorldPoint(newPos);
				transform.position = WordPos;
				transform.Translate(0,0,0.1f);
				if(mParticleSystem.emissionRate==MinPNum)
				{
					mParticleSystem.Clear();
					mParticleSystem.emissionRate = MaxPNum;
				}
			}
		}
		tempPos = Input.mousePosition;
	}

//
	void Update () {
		return;

		if(Input.GetMouseButtonUp(0))
				OnFingerUp();
		if(Input.GetMouseButtonDown(0))
		{
			OnFingerDown();
		}
		if(Input.GetMouseButton(0))
		{
			OnFingerTouch(Input.mousePosition);
		}
	}

	bool isMove(Vector2 pos1,Vector2 pos2)
	{
		return Vector2.Distance(pos1,pos2)>1;
	}
}

using UnityEngine;
using System.Collections;

public class GameCamraRotator : MonoBehaviour {


	float RoatorSpeed = 0.1f;
	bool mActive = false;
	float LastTempPosX = 0;
	Camera GameCamera;
	GameObject TweenFlaotObj;

	float SmoothSpeed = 0;

    bool isTweenStop = true;

	void Awake()
	{
		GameCamera = Camera.main;
	}

	public void SetActive(bool flag)
	{
		mActive = flag;
	}


	public void OnMouseDown ()
	{
		if (TweenFlaotObj != null)
			GameObject.Destroy (TweenFlaotObj);
		LastTempPosX = Input.mousePosition.x;
	}
	public void OnMouse()
	{
		float posX = Input.mousePosition.x;
		if(LastTempPosX!=posX)
		{

			float dis = posX-LastTempPosX;
			LastTempPosX = posX;
			RotatorCamera(dis);
			SmoothSpeed = (dis+SmoothSpeed)/2;
		}else
		{
			SmoothSpeed = SmoothSpeed/2;
		}
	}

	public void OnMouseUp()
	{
        EventManager.Instance.TriggerEvent(GameEventType.BeginnerGuideEvent, BeginnerGuideEventType.MoveSvreen);
        if (isTweenStop)
            TweenRotatorCamera(SmoothSpeed);
	}

    public void SetTweenStopActive(bool active)
    {
        isTweenStop = active;
    }

	void RotatorCamera(float x)
	{
		GameCamera.transform.Rotate(0,x*-RoatorSpeed,0);
	}

	void TweenRotatorCamera(float distance)
	{
		TweenFlaotObj = TweenFloat.Begin(0.5f,distance,0,RotatorCamera,null);
	}

}

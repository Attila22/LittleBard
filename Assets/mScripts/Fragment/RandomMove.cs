using UnityEngine;
using System.Collections;

public class RandomMove : MonoBehaviour {
	public enum Status
	{
		Move,
		Stay,
	}

	public Vector3 TargetPos;

	float MoveTime;
	float waitTIme = 1;

	float MoveTimeDis = 0;
	float waitTimeDis = 0;

	Status mStatus = Status.Stay;

	public bool mActive = false;
	public void SetActive(bool flag)
	{
		mActive = flag;
	}

	void Update () {

		if(!mActive)
			return;
		switch (mStatus)
		{
		case Status.Stay:
			waitTIme+=Time.deltaTime;
			if(waitTIme>waitTimeDis)
			{
				waitTIme = 0;
				mStatus = Status.Move;
				SetTargetPos();
			}
			break;
		case Status.Move:
			MoveTime+=Time.deltaTime;
			float speed = Random.Range(0.001f,0.01f);
			Vector3 toPos = Vector3.Lerp(transform.localPosition,TargetPos,MoveTime);
			transform.position = Vector3.Lerp(transform.localPosition,toPos,speed);
			if(MoveTime>MoveTimeDis)
			{
				MoveTime = 0;
				mStatus = Status.Stay;
			}
			break;
		}

	}

	void SetTargetPos()
	{
		TargetPos = transform.position+ new Vector3 (getRandomNum(),getRandomNum(),getRandomNum());
		TargetPos = GamePlaySceneMgr.Instance.mFragmentManager.GetPosInRange (TargetPos);
		MoveTimeDis = Random.Range(2.5f,4.5f);
		waitTimeDis =Random.Range(0.1f,0.5f);
	}
	
	float getRandomNum()
	{
		return Random.Range (-10f,10f);
	}
}

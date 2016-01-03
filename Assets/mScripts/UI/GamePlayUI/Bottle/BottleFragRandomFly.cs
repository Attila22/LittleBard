using UnityEngine;
using System.Collections;

public class BottleFragRandomFly : MonoBehaviour {
    public enum Status
    {
        Move,
        Stay,
    }

    public Vector3 TargetPos;
	public UISprite mSPrite;
    float MoveTime;
    float waitTIme = 1;

    float MoveTimeDis = 0;
    float waitTimeDis = 0;

    Status mStatus = Status.Stay;

    bool mActive = false;

	void Awake()
	{
		mSPrite = GetComponent<UISprite>();
	}

    public void SetActive(bool flag)
    {
        mActive = flag;
    }

    Vector3 fromPos;
    void Update()
    {

        if (!mActive)
            return;
        switch (mStatus)
        {
            case Status.Stay:
                waitTIme += Time.deltaTime;
                if (waitTIme > waitTimeDis)
                {
                    waitTIme = 0;
                    mStatus = Status.Move;
                    SetTargetPos();
                }
                break;
            case Status.Move:
                MoveTime += Time.deltaTime;
                //float speed = Random.Range(0.001f, 0.01f);
                //Vector3 toPos = Vector3.Lerp(transform.localPosition, TargetPos, MoveTime);
                transform.localPosition = Vector3.Lerp(fromPos, TargetPos, MoveTime/MoveTimeDis);
                if (MoveTime > MoveTimeDis)
                {
                    MoveTime = 0;
                    mStatus = Status.Stay;
                }
                break;
        }

    }

	public void SetFlyPosRange(int min ,int max)
	{
		MinRange = min;
		MaxRange = max;
	}

	int MinRange = 30;
	int MaxRange = 40;

    void SetTargetPos()
    {
		int randomX = Random.Range(MinRange, MaxRange);
		int randomY = Random.Range(MinRange, MaxRange);
        TargetPos = new Vector3(Random.Range(-1f, 1f) > 0 ? randomX : -randomX, Random.Range(-1f, 1f) > 0 ? randomY : -randomY, 0);

        fromPos = transform.localPosition;
        MoveTimeDis = Random.Range(1.5f, 2.5f);
        waitTimeDis = Random.Range(0.1f, 0.5f);
    }

    float getRandomNum()
    {
        return Random.Range(-10f, 10f);
    }
}

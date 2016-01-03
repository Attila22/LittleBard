//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class RandomFly : MonoBehaviour {
//
//    public delegate void MoveCompleteCallBack(object obj);
//
//    MoveSmooth m_SmoothMove = new MoveSmooth();
//
//    Vector3 FlyFromPos;
//    Vector3 FlyCenterPos;
//    Vector3 FlyTargetPos;
//
//    bool isFly = false;
//    float FlyTime = 0;
//    float TimeScale = 1;
//
//    MoveCompleteCallBack mMoveCompleteCallBack;
//    object mCallBackValue;
//
//    public static void Begin(GameObject go, float flyTime, Vector3 targetPos, MoveCompleteCallBack moveCompleteCallBack, object callbackvalue)
//    {
//        RandomFly component = go.GetComponent<RandomFly>();
//        if(component == null)
//            component = go.AddComponent<RandomFly>();
//        component.Init(go.transform.position,targetPos,flyTime,moveCompleteCallBack,callbackvalue);
//    }
//
//
//    void Init(Vector3 fromPos, Vector3 targetPos,float moveTime, MoveCompleteCallBack moveCompleteCallBack,object callbackvalue)
//    {
//        transform.position = fromPos;
//        this.FlyFromPos = fromPos;
//        this.FlyTargetPos = targetPos;
//        this.mMoveCompleteCallBack = moveCompleteCallBack;
//        this.mCallBackValue = callbackvalue;
//        isFly = false;
//        FlyTime = 0;
//        TimeScale = moveTime;
//        FlyCenterPos = GetFlyCenterPos(FlyFromPos, FlyTargetPos); 
//    }
//
//    void Update()
//    {
//        if (isFly == false)
//        {
//            isFly = true;
//            m_SmoothMove.Init(FlyFromPos, FlyCenterPos - FlyFromPos, FlyCenterPos, FlyTargetPos - FlyCenterPos, 1);
//        }
//        else if (FlyTime < 1)
//        {
//            FlyTime += Time.deltaTime/TimeScale;
//            FlyTime = FlyTime > 1 ? 1 : FlyTime;
//            gameObject.transform.position = m_SmoothMove.GetCurrentPos(FlyTime);
//        }
//        else
//        {
//            FlyFinish();
//        }
//    }
//
//
//    void FlyFinish()
//    {
//        gameObject.transform.position = FlyTargetPos;
//        if (this.mMoveCompleteCallBack != null)
//        {
//            this.mMoveCompleteCallBack(mCallBackValue);
//            enabled = false;
//        }
//    }
//
//
//    Vector3 GetFlyCenterPos(Vector3 fromPos, Vector3 targetPos)
//    {
//        Vector3 getPos = Vector3.zero;
//        Vector3 dir = targetPos - fromPos;
//        getPos = fromPos + dir * 0.1f;
//        float rangeNum = Vector3.Distance(fromPos,targetPos)/4f;
//        getPos.x += Random.Range(-rangeNum, rangeNum);
//        getPos.y += Random.Range(-rangeNum, rangeNum);
//        return getPos;
//    }
//
//
//
//}

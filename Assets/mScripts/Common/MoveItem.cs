using UnityEngine;
using System.Collections;
using System;

public class MoveItem : MonoBehaviour {

    Action mFinishCallBack;

    public void Begin(GameObject go,float dir,Action finishCallBack)
    {
        mFinishCallBack = finishCallBack;
    }



}

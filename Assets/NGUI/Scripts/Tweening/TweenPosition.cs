﻿//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright 漏 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the object's position.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	public Vector3 from;
	public Vector3 to;

	Transform mTrans;

	public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }
	public Vector3 position { get { return cachedTransform.localPosition; } set { cachedTransform.localPosition = value; } }

	protected override void OnUpdate (float factor, bool isFinished) { cachedTransform.localPosition = from * (1f - factor) + to * factor; }

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

	static public TweenPosition Begin (GameObject go, float duration, Vector3 pos)
	{
		TweenPosition comp = UITweener.Begin<TweenPosition>(go, duration);
		comp.from = comp.position;
		comp.to = pos;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}

    //Add By Jiang
    static public TweenPosition Begin(GameObject go, float duration, Vector3 fromPos,Vector3 toPos,ButtonCallBack finishCallBack)
    {
        TweenPosition comp = UITweener.Begin<TweenPosition>(go, duration);
        comp.from = fromPos;
        comp.to = toPos;
        comp.FinishCallBack = finishCallBack;

        if (duration <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }
}

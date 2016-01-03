using UnityEngine;
using System.Collections;


public class TweenFloat : MonoBehaviour {

	public delegate void FinishCallBackDelegate();
	public delegate void ChangeValueDelegate(float number);
	
	public float m_CurrentValue{get{return m_fromValue;}}

	bool IsAdd = true;
	float m_fromValue;
	float m_toValue;
	float m_addNumber;
	ChangeValueDelegate m_ChangeValueDelegate;
	FinishCallBackDelegate m_FinishCallBack;



	public static GameObject Begin(float duration,float fromValue,float toValue,ChangeValueDelegate changeValueDelegate,FinishCallBackDelegate finishCallBack)
	{
		if(fromValue == toValue)
		{
			if(changeValueDelegate!=null)
			{
				changeValueDelegate(toValue);
			}
			if(finishCallBack!=null)
			{
				finishCallBack();
			}
			return null;
		}
		GameObject tweenObj = new GameObject();
		TweenFloat tweeenFloatComponent = tweenObj.AddComponent<TweenFloat>();
		tweeenFloatComponent.m_addNumber =(toValue - fromValue)/duration;
		tweeenFloatComponent.m_fromValue = fromValue;
		tweeenFloatComponent.m_toValue = toValue;
		tweeenFloatComponent.m_ChangeValueDelegate = changeValueDelegate;
		tweeenFloatComponent.IsAdd = fromValue<toValue;
        tweeenFloatComponent.m_FinishCallBack = finishCallBack;
		return tweenObj;
	}

	void Update()
	{
		m_fromValue+= m_addNumber*Time.deltaTime;
		if(IsAdd)
        {
            SetValueCallback(m_fromValue);
			if(m_fromValue>=m_toValue)
			{
				m_fromValue = m_toValue;
				FinishAndCallback();
			}
		}else
        {
            SetValueCallback(m_fromValue);
			if(m_fromValue<=m_toValue)
			{
				m_fromValue = m_toValue;
				FinishAndCallback();
			}
		}
	}

	public void PauseTweenFloat()
	{
		enabled = false;
	}

	public void ContinueTweenFloat()
	{
		enabled = true;
	}

	void SetValueCallback(float value)
	{
		if(m_ChangeValueDelegate!=null)
		{
			m_ChangeValueDelegate(value);
		}
	}

	void FinishAndCallback()
	{
		if(m_FinishCallBack!=null)
		{
			m_FinishCallBack();
		}
		Destroy(gameObject);
	}

}

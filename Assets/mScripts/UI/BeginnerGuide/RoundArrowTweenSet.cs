using UnityEngine;
using System.Collections;

public class RoundArrowTweenSet : MonoBehaviour {

    public UISprite mSPrite;

    float mValue = 0;

    float addValue = 0.01f;

    void Awake()
    {
        mSPrite.fillAmount = 0;
    }

    void Update()
    {
        mValue += addValue;
        if ((addValue > 0 && mValue > 1) || (addValue < 0 && mValue<0))
        {
            mSPrite.invert = !mSPrite.invert;
            addValue = -addValue;
        }
        mSPrite.fillAmount = mValue;
    }

}

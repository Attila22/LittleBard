using UnityEngine;
using System.Collections;

public class SingleGrid : MonoBehaviour {


	public int mRowIndex;
	public int mLineIndex;

	float xOff = 75;
	float yOff = 75;

	public Transform[] rayPosGroup;

	public UILabel mLabel;
	public UISprite mSprite;

	public void Init(int rowIndex,int lineIndex)
	{
		mRowIndex = rowIndex;
		mLineIndex = lineIndex;
		transform.localPosition = new Vector3 (xOff*rowIndex,yOff*lineIndex,0);
	}

	public void SetLable(string info)
	{
//		return;
		mLabel.text = info;
		Color mColor = string.IsNullOrEmpty (info) ? Color.white : Color.red;
        mColor.a = 0.5f;
        mSprite.color = mColor;
	}

}

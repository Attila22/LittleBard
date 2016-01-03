using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOverBottle : MonoBehaviour {

	public UILabel BottleLabel;
	
	GameObject BottleFlyEffectPrefab { get { return Resources.Load("UI/GamePlayUI/Bottle/FragmentFlyEffect") as GameObject; } }

	List<GameObject> mBottleObjGroup = new List<GameObject>();


	public void SetMyFragments(int num,int max,FragmentType type)
	{
		mBottleObjGroup.ApplyAllItem(C=>GameObject.Destroy(C.gameObject));
		mBottleObjGroup.Clear();
		for(int i = 0;i<num;i++)
        {
            GameObject newFrag = NGUITools.AddChild(gameObject, BottleFlyEffectPrefab);
			BottleFragRandomFly randomFly = newFrag.AddComponent<BottleFragRandomFly>();
			float scale = 0.5f+((int)type)*0.5f;
			newFrag.transform.localScale*= scale;

            int randomX = Random.Range(30, 40);
            int randomY = Random.Range(30, 40);
            Vector3 targetPos = new Vector3(Random.Range(-1f, 1f) > 0 ? randomX : -randomX, Random.Range(-1f, 1f) > 0 ? randomY : -randomY, 0);
            randomFly.transform.localPosition = targetPos;
			randomFly.mSPrite.depth = 1;
            randomFly.SetActive(true);
            mBottleObjGroup.Add(newFrag);
		}
		BottleLabel.SetText(string.Format("{0}/{1}",num,max));
	}

}

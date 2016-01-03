using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FragmentManager {

    
	public List<SingleFragment> mFragmentGroup = new List<SingleFragment>();
	public List<SingleBottle> mBottleGroup = new List<SingleBottle>();
	public float radius = 60f;
	Transform mcameraTran;
	Transform CameraTran{get{
			if(mcameraTran == null)
				mcameraTran = Camera.main.transform;
			return mcameraTran;
		}}
    int mMaxFragmentNum;
    public GameObject FragmentPrefab { get { return Resources.Load("FragmentShow/Fragment") as GameObject; } }
	GameObject mroot;
	public GameObject Root{get{
			if(mroot == null)
				mroot = GamePlaySceneMgr.Instance.gameObject;
			return mroot;
		}}
	float minPosY = 10f;
	float maxPosY = 35f;

    public void InitFragment(int maxFragmentNum)
    {
        mMaxFragmentNum = maxFragmentNum;
        AddFragmentToMax();
    }

    public void AddFragmentToMax()
    {
        while (mFragmentGroup.Count < mMaxFragmentNum)
        {
            AddNewFragment(GetRandomPos(), FragmentType.Small);
        }
    }

    public SingleFragment AddNewFragment(Vector3 pos,FragmentType type,bool showEffect = false)
    {
//        Debug.Log("AddFragment");
        SingleFragment newFragment = (GameObject.Instantiate(FragmentPrefab) as GameObject).GetComponent<SingleFragment>();
        newFragment.transform.position = pos;
        newFragment.Init(type, showEffect);
		newFragment.transform.parent = Root.transform;
        mFragmentGroup.Add(newFragment);
		return newFragment;
    }

    public SingleFragment AddFragmentAtScreenPos(Vector3 viewPos, FragmentType type)
	{
		viewPos.z = 10;
		Vector3 wordPos = Camera.main.ViewportToWorldPoint (viewPos);
		wordPos = GetPosInRange (wordPos);
        SingleFragment fragment = AddNewFragment(wordPos, type);
        fragment.SetRandomMoveActive(false);
		fragment.IsBeginnerGuideFragment = true;
        return fragment;
	}

    Vector3 GetRandomPos()
    {
        Vector3 dir = Vector3.Normalize(new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f)));
        Vector3 generatorPos = CameraTran.position + dir * radius;
		generatorPos.y = Random.Range(minPosY,maxPosY);
        return generatorPos;
    }
    /// <summary>
    /// 转换到限制位置范围内
    /// </summary>
    public Vector3 GetPosInRange(Vector3 pos)
    {
        Vector3 getPos = pos;
        Vector3 dir = Vector3.Normalize(pos - CameraTran.position) ;
        getPos =CameraTran.position+ dir * radius;
        if (getPos.y < minPosY)
            getPos.y = minPosY;
        if (getPos.y > maxPosY)
            getPos.y = maxPosY;
        return getPos;
    }

	public void RemoveBigTypeFragment(SingleFragment fragment)
	{
		if(fragment.mFragmentType!= FragmentType.Small)
		{
			DestroyFragment(fragment);
			int num = fragment.mFragmentType== FragmentType.Big?6:3;
			for(int i = 0;i<num;i++)
			{
				float rangeNum = 10f;
                //Vector3 dir = Vector3.Normalize( new Vector3(Random.Range(-rangeNum,rangeNum),Random.Range(-rangeNum,rangeNum),Random.Range(-rangeNum,rangeNum)));
                //Vector3 targetPos = fragment.transform.position+dir*30f;

                //Vector3 targetDir =  Vector3.Normalize(targetPos - CameraTran.position);
                //targetPos = CameraTran.position+ targetDir*radius;

				SingleFragment newFragment = AddNewFragment(fragment.transform.position, FragmentType.Small);
                FlyToRandomPos(newFragment);
                //newFragment.FlyToPos (targetPos,(frag)=>{
                //    DestroyFragment(frag);
                //    AddFragmentToMax();
                //});
			}
		}
	}

    public void FlyToRandomPos(SingleFragment fragment)
    {

        float rangeNum = 10f;
        Vector3 dir = Vector3.Normalize(new Vector3(Random.Range(-rangeNum, rangeNum), Random.Range(-rangeNum, rangeNum), Random.Range(-rangeNum, rangeNum)));
        Vector3 targetPos = fragment.transform.position + dir*20;
        targetPos = GetPosInRange(targetPos); 
        fragment.FlyToPos(targetPos, (frag) =>
        {
            //DestroyFragment(frag);
            //AddFragmentToMax();
        });
    }

	public void DestroyFragment(SingleFragment fragment)
    {
        GameObject.Destroy(fragment.gameObject);
        if (mFragmentGroup.Contains(fragment))
        {
            mFragmentGroup.Remove(fragment);
            //Debug.Log("RemoveFragment");
        }
    }

    public void CleanUpAllFragment()
    {
		mMaxFragmentNum = 0;
        for (int i = 0; i < mFragmentGroup.Count; i++)
        {
            GameObject.Destroy(mFragmentGroup[i].gameObject);
        }
        mFragmentGroup.Clear();
    }
}

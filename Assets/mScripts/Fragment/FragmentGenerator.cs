using UnityEngine;
using System.Collections;

public class FragmentGenerator : MonoBehaviour {


	public float radius = 0.6f;

	public GameObject FragmentPrefab;




	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			GeneratorFragment(FragmentType.Big);
		}
	}

	public void GeneratorFragment(FragmentType type)
	{
		Vector3 dir =Vector3.Normalize(new Vector3(Random.Range(-1f,1f),0,Random.Range(-1f,1f)));
		Vector3 generatorPos = transform.position+dir*radius;
		generatorPos.y =Random.Range(0.25f,0.5f);

		GameObject newObj = GameObject.Instantiate(FragmentPrefab)as GameObject;
		newObj.transform.position = generatorPos;
	}


	

}

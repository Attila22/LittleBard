using UnityEngine;
using System.Collections;

public class VideoPlay : MonoBehaviour {

	static VideoPlay instance;
	public static VideoPlay Instance
	{
		get
		{
			if(instance == null)
				instance = new GameObject().AddComponent<VideoPlay>();
			return instance;
		}
	}

	bool IsInit = true;

	System.Action FinishCallBack;


	public void Play(System.Action finishCallBack)
	{
		UIManager.Instance.Show(UIType.BlackUI);

		this.FinishCallBack = finishCallBack;
		StartCoroutine(PlayForFram());
	}

	IEnumerator PlayForFram()
	{
		Debug.Log("PlayForFram");
		yield return null;
		IsInit = false;
		#if UNITY_EDITOR
		StartCoroutine(InitGame(0));
		#else
		Debug.Log("PlayFullScreenMovie");
		Handheld.PlayFullScreenMovie("Movie.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
		StartCoroutine(InitGame(97));
		#endif
	}

	void Update()
	{
		if(!IsInit)
		{
			IsInit = true;
			StopAllCoroutines();
			StartCoroutine(InitGame(0));
		}
	}
	
	IEnumerator InitGame(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		
		Debug.Log("InitGame"+waitTime);
		UIManager.Instance.Close(UIType.BlackUI);

		IsInit = true;
		Destroy(gameObject);
		instance = null;
		FinishCallBack();
	}

}

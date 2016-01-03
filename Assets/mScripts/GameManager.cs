using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public static GameManager Instance;
	public SoundManager mSoundManager ;
    public GameDataManager mGameDataManager;
	
    GameObject UIPrefab { get { return Resources.Load("UI/UI Root (2D)") as GameObject; } }
    GameObject GamePlayScenePrefab { get { return Resources.Load("FragmentShow/GamePlaySceneObj") as GameObject; } }

	void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SoundManager.Instance.PlayMusic("BackGroundMusic");
        mGameDataManager = gameObject.GetComponent<GameDataManager>();
        GameObject.Instantiate(UIPrefab);
        UIManager.Instance.Show(UIType.MainUI);
    }

       
	public void StartGame(int level)
    {
		if(GamePlaySceneMgr.Instance == null)
        	GameObject.Instantiate(GamePlayScenePrefab);
		GamePlaySceneMgr.Instance.InitGameScenes (level);
	}


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
//            StopAllCoroutines();
//            StartCoroutine(InitGame(0));
    }

}

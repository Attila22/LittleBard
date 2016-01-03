using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void ButtonCallBack(object obj);

public enum Side
{
	Up = 0,
	Down,
	Left,
	Right,
}
public enum MsgType
{
    None,
    SetLevel,
    ContinueGame,
    PauseGame,
    GameOverData,
	BeginnerGuideEvent,
    MissionBottleNum,
}

public enum FragmentType
{
	Small = 1,
	Medium,
	Big,
}

[System.Serializable]
public class LevelData
{
    public int level;
    public Texture2D PicWallPic;
    public List<LevelBottleData> BottleGetDataGroup;
}
[System.Serializable]
public class LevelBottleData
{ 
    public BottleType mBottleType;
    public int GetFragNum;
    public FragmentType mFragmentType;
}

public enum BottleType
{
    OIL_STREET_EVENTS,
    OIL_STREET,
    PMQ_EVENTS,
    PMQ_GALLERY,
    PMQ_RESTAURANT,
    PMQ,
    VA,
}

public class GameOverData
{
	public bool IsWin;
	public int BigFragmentNum;
	public int MediumFragmentNum;
	public int SmallFragmentNum;
}
[System.Serializable]
public class BottleInfo
{
    public BottleType mBottleType;
    public string Info;
}

[System.Serializable]
public class BeginnerGuideData
{
	public BeginnerGuideType mGuideType;
	public GameObject AnimPrefab;
}

public enum BeginnerGuideType
{
    Step1,
    Step2,
    Step3,
    Step4,
    Step5,
    Step6,
    Step7,
    Step8,
}

public enum BeginnerGuideEventType
{
	None,
	OnClick,
	OnFragmentEnterBottle,
	TranslateBinggerFragment,
    MoveSvreen,
	AttractFragmentComplete,
}

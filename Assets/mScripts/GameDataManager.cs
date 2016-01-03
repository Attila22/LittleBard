using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameDataManager : MonoBehaviour {


    public List<LevelData> mLevelDataGroup;
    public List<BottleInfo> mBottleInfoGroup;

    public LevelData GetLevelData(int level)
    {
        LevelData getdata = null;
        getdata = mLevelDataGroup.FirstOrDefault(C=>C.level == level);
        return getdata;
    }


    public void SaveOwnBottle(BottleType bottleType)
    {
        int intType = (int)bottleType;
        List<int> mOwnBottle = GetOwnBottle();
        if (!mOwnBottle.Contains(intType))
            mOwnBottle.Add(intType);
        SaveKey(mOwnBottle);
    }

    void SaveKey(List<int> keyGroup)
    {
        string saveKey = "";
        keyGroup.ApplyAllItem(C => saveKey += C.ToString() + "#");
        PlayerPrefs.SetString("mBottle", saveKey);
    }

    public List<int> GetOwnBottle()
    {
        List<int> getGroup = new List<int>();
        string ownBottle = PlayerPrefs.GetString("mBottle", "");
        string[] bottleTypeGroup = ownBottle.Split('#');
        bottleTypeGroup.ApplyAllItem(C =>
        {
            if (!string.IsNullOrEmpty(C))
            {
                int key = int.Parse(C);
                if (!getGroup.Contains(key))
                    getGroup.Add(key);
            }
        });
        return getGroup;
    }

    public void SaveUnLockLevel(int level)
    {
        int maxlevel = GetUnLockLevel()-1;
        if (level >= maxlevel)
        {
            PlayerPrefs.SetInt("UnLockLevel", level);
        }
    }
	[ContextMenu("Save")]
	void SaveInt()
	{
		PlayerPrefs.SetInt("UnLockLevel", 0);
	}
	
	public int GetUnLockLevel()
    {
		int unlockLevel = PlayerPrefs.GetInt("UnLockLevel", 0);
        return (unlockLevel+1);
    }

}

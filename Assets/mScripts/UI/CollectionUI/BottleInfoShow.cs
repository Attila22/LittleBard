using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BottleInfoShow : MonoBehaviour {


    public UILabel mLabel;

    public GameObject ButtonLeft;
    public GameObject ButtonRight;
    public GameObject GrayCollider;
    public List<string> mInfoGroup = new List<string>();

    int strMaxLeght = 50;
    int CurrentPage = 0;

    public string Data;

    [ContextMenu("Do")]
    void Start()
    {
        mLabel.SetText("");
        //Show(Data);
        UIEventListener.Get(ButtonLeft).onClick = OnLeftBtnClick;
        UIEventListener.Get(ButtonRight).onClick = OnRightBtnClick;
        UIEventListener.Get(GrayCollider).onClick = OnClickGrayCollider;
    }

    

    public void Show(CollectionChildBottle bottle)
    {
        string info = GameManager.Instance.mGameDataManager.mBottleInfoGroup.First(C => C.mBottleType == bottle.mType).Info;
        CurrentPage = 0;
        mInfoGroup.Clear();
//        Debug.Log("Show");
        info.Split('#').ApplyAllItem(C => {
            if (!string.IsNullOrEmpty(C))
                mInfoGroup.Add(C);
        });
        UpdatePageData();
        GrayCollider.gameObject.SetActive(true);
        TweenSetCollider(true);
        GrayCollider.transform.position = bottle.transform.position;
    }

    void OnClickGrayCollider(GameObject obj)
    {
        mInfoGroup.Clear();
        mLabel.SetText("");
        TweenSetCollider(false);
    }

    bool isShowGrayCollider = false;
    void TweenSetCollider(bool flag)
    {
        isShowGrayCollider = flag;
        float from = flag?0:1;
        float to = flag?1:0;
        TweenAlpha.Begin(GrayCollider.gameObject,0.5f,from,to,OnSetColliderComplete);
    }

    void OnSetColliderComplete(object obj)
    {
        GrayCollider.gameObject.SetActive(isShowGrayCollider);
    }

    void OnLeftBtnClick(GameObject obj)
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            UpdatePageData();
        }
    }

    void OnRightBtnClick(GameObject obj)
    {
        if (CurrentPage < mInfoGroup.Count - 1)
        {
            CurrentPage++;
            UpdatePageData();
        }
    }

    void UpdatePageData()
    {
        mLabel.SetText(mInfoGroup[CurrentPage]);
    }

    public void ShowRoleInfo(string str)
    {
        mInfoGroup.Clear();
        mLabel.SetText(str);
    }

}

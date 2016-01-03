using UnityEngine;
using System.Collections;

public class SoundPlay : MonoBehaviour {

    public enum PlayEventType { 
        Drag,
        OnClick,
    }

    public AudioClip SoundKey;
    public PlayEventType mPlayEventType = PlayEventType.OnClick;

    void OnClick() { if (mPlayEventType == PlayEventType.OnClick) SoundManager.Instance.PlaySound(SoundKey.name); }
    void OnDrag(Vector2 delta) { if (mPlayEventType == PlayEventType.Drag && !isPlay) { SoundManager.Instance.PlaySound(SoundKey.name); isPlay = true; } }
    bool isPlay = false;
    void OnPress(bool flag)
    {
        if (!flag)
            isPlay = false;
    }
}

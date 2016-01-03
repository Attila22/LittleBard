using UnityEngine;
using System.Collections;

public class GameSetting : BaseUI{
	public enum SettingType
	{
		MainSetting,
		GamePlaySetting,
        GameOverSetting,
	}


	public GameObject BackButton;
	public UISlider MusiSliderBar;
	public UISlider SoundSliderBar;

	SettingType mSettingPanelType = SettingType.GamePlaySetting;

	public override void Show ()
	{
		MusiSliderBar.value = GameManager.Instance.mSoundManager.MusicVolume;
		SoundSliderBar.value = GameManager.Instance.mSoundManager.SoundVolume;
	}

	public void OnDragSoundSlider()
	{
		GameManager.Instance.mSoundManager.SetSoundVolume (SoundSliderBar.value);
	}
	
	public void OnDragMusicSlider()
	{
		GameManager.Instance.mSoundManager.SetMusicVolume (MusiSliderBar.value);
	}

	public void OnClickBackBtn()
	{
		switch (mSettingPanelType) {
		case SettingType.GamePlaySetting:
			break;
            case SettingType.GameOverSetting:
            break;
		case SettingType.MainSetting:
			break;
		}
		Close ();
	}

	public override void OnMsg(MsgType msgType,object msg)
	{
        this.mSettingPanelType = (SettingType)msg;
	}

	public override void Close ()
	{
		gameObject.SetActive (false);
	}



}

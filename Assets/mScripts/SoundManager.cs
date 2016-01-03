using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

	public List<AudioClip> mClipGroup;
	Dictionary<string,AudioClip> ClipResDis = new Dictionary<string, AudioClip>();

	List<AudioSource> SoundAudioSourceGroup = new List<AudioSource>();
	List<AudioSource> MusicSourceGroup = new List<AudioSource>();

    Queue<AudioSource> DisableAudioSourceGroup = new Queue<AudioSource>();
    Queue<AudioSource> DisableMusicSourceGroup = new Queue<AudioSource>();

	public float SoundVolume = 1f;
	public float MusicVolume = 1f;

	void Awake()
	{
		mClipGroup.ApplyAllItem (C=>{
			ClipResDis[C.name] = C;
		});
        Instance = this;
	}

    //void Start()
    //{
    //    mClipGroup.ApplyAllItem(C => AudioSource.PlayClipAtPoint(C, Vector3.zero));
    //}

	public void PlaySound(string clipName)
    {
        AudioClip mClip = ClipResDis[clipName];
//        Debug.Log("Play:"+clipName);
		AudioSource source = GetAudioSource (true);
		source.clip = mClip;
		source.loop = false;
		source.volume = SoundVolume;
		source.Play ();
		SoundAudioSourceGroup.Add (source);
        StartCoroutine(RevoverAudioSourceForTime(source, mClip.length * Time.timeScale));
	}

	public void PlayMusic(string clipName)
    {
//        Debug.Log("PlayMusic" + clipName);
        bool isPlayed = false;
        MusicSourceGroup.ApplyAllItem(C => {
            if (C.clip!=null&&C.clip.name == clipName)
                isPlayed = true;
        });
        if (isPlayed)
            return;
        AudioSource source = GetAudioSource (false);
		AudioClip mClip = ClipResDis [clipName];
		source.clip = mClip;
		source.loop = true;
		source.volume = MusicVolume;
		source.Play ();
		MusicSourceGroup.Add (source);
	}

    IEnumerator RevoverAudioSourceForTime(AudioSource source, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
//        Debug.Log("RecoverSOund:"+source.clip.name);
        SoundAudioSourceGroup.Remove(source);
        RecoverAudioSource(source,true);
    }

	public void StopMusic(string clipName)
	{
//        Debug.Log("StopMusic");
		for (int i = MusicSourceGroup.Count-1; i>=0; i--) {
			AudioSource source = MusicSourceGroup[i];
			if(source.name == clipName)
				MusicSourceGroup.RemoveAt(i);
			RecoverAudioSource(source,false);
		}
	}

	public void SetSoundVolume(float v)
	{
		SoundAudioSourceGroup.ApplyAllItem (C=>{
			C.volume = v;
		});
		SoundVolume = v;
	}

	public void SetMusicVolume(float v)
	{
		MusicSourceGroup.ApplyAllItem (C=>{
			C.volume = v;
		});
		MusicVolume = v;
	}

    //void LateUpdate()
    //{
    //    if(SoundAudioSourceGroup.Count>0){
    //        for(int i = SoundAudioSourceGroup.Count-1;i>=0;i--)
    //        {
    //            AudioSource source = SoundAudioSourceGroup[i];
    //            if(!source.isPlaying)
    //            {
    //                SoundAudioSourceGroup.RemoveAt(i);
    //                RecoverAudioSource(source);
    //            }
    //        }
    //    }
    //}

	public AudioSource GetAudioSource(bool isSound)
	{
		AudioSource getSource = null;
		if (isSound&&DisableAudioSourceGroup.Count > 0) 
		{
           	getSource = DisableAudioSourceGroup.Dequeue();
		}else if(!isSound&&DisableMusicSourceGroup.Count>0)
		{
			getSource = DisableMusicSourceGroup.Dequeue();
		}
		else {
			getSource = gameObject.AddComponent<AudioSource>();		
		}
		getSource.enabled = true;
		return getSource;
	}

	public void RecoverAudioSource(AudioSource source,bool isShound)
	{
		source.clip = null;
		source.enabled = false;
//        Debug.Log("AddRecoverSouce"+source.GetInstanceID());
        if (isShound)
		    DisableAudioSourceGroup.Enqueue (source);
        else
            DisableMusicSourceGroup.Enqueue(source);
	}
}

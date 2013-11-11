using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgm;

    private AudioSource source;
    private AudioSource bgmSource;

    private Dictionary<string, string> audioDict = new Dictionary<string, string>(){
        {"MenuButtonOver","buttonOver"},
        {"MenuButtonSelect", "buttonSelect"}
    };

	// Use this for initialization
	void Start ()
    {
        source = this.gameObject.AddComponent<AudioSource>();
        bgmSource = this.gameObject.AddComponent<AudioSource>();

        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.playOnAwake = true;
	}
	
	// Update is called once per frame
	void Update () { }

    public void PlaySoundEffect(string effectName)
    {
        //Debug.Log("Playing Sound Effect: " + effectName);
        //Debug.Log("Clip name: " + audioDict[effectName]);
        AudioClip clip = (AudioClip)Resources.Load(audioDict[effectName], typeof(AudioClip));
        //Debug.Log("Clip name: " + clip.name);
        source.PlayOneShot(clip);
    }
}

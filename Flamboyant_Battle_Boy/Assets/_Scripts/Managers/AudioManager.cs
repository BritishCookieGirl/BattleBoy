using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;
    private AudioSource bgmSource1;
    private AudioSource bgmSource2;

    private int currentSourceActive = 1;
    private string currentTrack = "";
    private bool soundRunning = true;
    private bool interrupted = false;
    private float fadeLength = 1.0f;

    private Dictionary<string, string> bgmDict = new Dictionary<string, string>(){
        {"MainIntro", "FBB - Intro"},
        {"Main", "FBB - Main"},
        {"Loop1Ramp", "FBB - Ramp"},
        {"Loop1", "FBB - Loop 1"},
        {"Loop2", "FBB - Loop 2"},
        {"Loop3Intro", "FBB - Loop 3 Intro"},
        {"Loop3", "FBB - Loop 3"}
    };

    private Dictionary<string, string> audioDict = new Dictionary<string, string>(){
        {"MenuButtonOver","buttonOver"},
        {"MenuButtonSelect", "buttonSelect"},
        {"Cheer", "Cheering"},
        {"Coin", "Coin"},
        {"Die", "Die"},
        {"Firecracker", "firecracker"},
        {"Fireworks", "Firework Explode"},
        {"Grenade", "Grenade 1"},
        {"Kick1", "Kick 1"},
        {"Kick2", "Kick 2"},
        {"Kick3", "Kick 3"},
        {"Punch1", "Punch Hit 1"},
        {"Punch2", "Punch Hit 2"},
        {"Punch3", "Punch Hit 3"},
        {"Whoosh1", "Whoosh 1"},
        {"Whoosh2", "Whoosh 2"},
        {"Whoosh3", "Whoosh 3"}
    };

	// Use this for initialization
	void Start ()
    {
        AudioListener.volume = 0.1f;
        source = this.gameObject.AddComponent<AudioSource>();
        bgmSource1 = this.gameObject.AddComponent<AudioSource>();
        bgmSource2 = this.gameObject.AddComponent<AudioSource>();

        bgmSource1.audio.volume = 1.0f;
        bgmSource2.audio.volume = 1.0f;

        StartInterrupt("MainIntro");
	}

    public void ChangeBGM(string changeTo)
    {
        StartInterrupt(changeTo);
    }

    private IEnumerator StartIntro(AudioClip intro, string mainPart)
    {
        Debug.Log("Waiting for song to finish in: " + (intro.length - fadeLength));
        yield return new WaitForSeconds(intro.length - fadeLength);
        Debug.Log("Starting Post-Intro Part");

        AudioClip mainLoop = (AudioClip)Resources.Load(bgmDict[mainPart], typeof(AudioClip));

        if (currentSourceActive == 1)
        {
            Debug.Log("switching to source 2");
            bgmSource1.audio.Stop();
            bgmSource2.loop = true;
            bgmSource2.clip = mainLoop;
            bgmSource2.Play();
            currentSourceActive = 2;
        }
        else
        {
            Debug.Log("switching to source 1");
            bgmSource2.audio.Stop();
            bgmSource1.loop = true;
            bgmSource1.clip = mainLoop;
            bgmSource1.Play();
            currentSourceActive = 1;
        }
        Debug.Log("waiting for end of bgm");
        yield return new WaitForSeconds(mainLoop.length - fadeLength);
        interrupted = false;
    }

    private void StartInterrupt(string changeTo)
    {
        Debug.Log("interrupting");
        if (interrupted)
            return;

        interrupted = true;

        bool isRamp = false;
        bool isIntro = false;

        Debug.Log("Song: " + changeTo);

        if (changeTo.Contains("Ramp"))
            isRamp = true;
        else if (changeTo.Contains("Intro"))
            isIntro = true;

        Debug.Log("isRamp = " + isRamp + "; isIntro = " + isIntro);

        AudioClip with = (AudioClip)Resources.Load(bgmDict[changeTo], typeof(AudioClip));

        if (isRamp) //the interrupting clip is a ramp-up, so no crossfade
        {
            if (currentSourceActive == 1)
            {
                Debug.Log("Playing " + with.name);
                bgmSource2.clip = with;
                bgmSource2.loop = false;
                bgmSource2.Play();
                currentSourceActive = 2;
            }
            else
            {
                Debug.Log("Playing " + with.name);
                bgmSource1.clip = with;
                bgmSource1.loop = false;
                bgmSource1.Play();
                currentSourceActive = 1;
            }
            StartCoroutine(StartIntro(with, changeTo.Substring(0, changeTo.IndexOf("Ramp"))));
        }

        else
        {
            if (isIntro)
            {
                if (currentSourceActive == 1)
                {
                    Debug.Log("Playing " + with.name);
                    bgmSource2.clip = with;
                    bgmSource2.loop = false;
                    bgmSource2.Play();
                    currentSourceActive = 2;
                }
                else
                {
                    Debug.Log("Playing " + with.name);
                    bgmSource1.clip = with;
                    bgmSource2.loop = false;
                    bgmSource1.Play();
                    currentSourceActive = 1;
                }

                Debug.Log("Calling StartIntro");
                StartCoroutine(StartIntro(with, changeTo.Substring(0, changeTo.IndexOf("Intro"))));
            }

            else
            {
                if (currentSourceActive == 1)
                {
                    Debug.Log("Playing " + with.name);
                    bgmSource2.clip = with;
                    bgmSource2.loop = true;
                    bgmSource2.Play();
                    StartCoroutine(Crossfade(bgmSource2.audio, bgmSource1.audio, 1.0f));
                    currentSourceActive = 2;
                }
                else
                {
                    Debug.Log("Playing " + with.name);
                    bgmSource1.clip = with;
                    bgmSource2.loop = true;
                    bgmSource1.Play();
                    StartCoroutine(Crossfade(bgmSource1.audio, bgmSource2.audio, 1.0f));
                    currentSourceActive = 1;
                }

                float waitFor = with.length - 2.0f;
                StartCoroutine(Interrupt(waitFor)); //prevents new interrupt from happening until end of clip
            }
        }
    }

    private IEnumerator Interrupt(float waitFor)
    {
        yield return new WaitForSeconds(1.0f);

        if(waitFor > 0)
            yield return new WaitForSeconds(waitFor);
        EndInterrupt();
    }

    private void EndInterrupt()
    {
        if (!interrupted)
            return;

        interrupted = false;

        if (currentSourceActive == 1)
            bgmSource1.Stop();
        else
            bgmSource2.Stop();
    }

    private IEnumerator Crossfade(AudioSource up, AudioSource down, float duration)
    {
        float vol = 0.0f;
        while (vol < 1.0)
        {
            vol += Time.deltaTime / duration;
            up.volume = vol;
            down.volume = 1.0f - vol;
            yield return new WaitForFixedUpdate();
        }
        up.volume = 1.0f;
        down.volume = 0.0f;
    }

    public void PlaySoundEffect(string effectName)
    {
        if (effectName == "Kick" || effectName == "Punch" || effectName == "Whoosh")
        {
            System.Random r = new System.Random();
            effectName += r.Next(1, 3);
        }

        AudioClip clip = (AudioClip)Resources.Load(audioDict[effectName], typeof(AudioClip));
        //Debug.Log("Clip name: " + clip.name);
        source.PlayOneShot(clip);
    }
}

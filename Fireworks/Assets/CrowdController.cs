using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CrowdController : MonoBehaviour
{

    private string[] Animations = { "idle", "applause", "applause2", "celebration", "celebration2", "celebration3" };

    // Use this for initialization
    Animation[] AudienceMembers;

    public List<AudioClip> CrowdNoises = new List<AudioClip>();

    bool Cheering;
    bool source2Playing = false;

    AudioSource Sounds1;
    AudioSource Sounds2;

    FadingAudioSource fadeSounds;

    GameObject Head;

    float realVol;

    void Start()
    {
        Sounds1 = GetComponents<AudioSource>()[1];
        Sounds2 = GetComponents<AudioSource>()[2];

        realVol = 1;

        Head = GameObject.Find("Main Camera");

        AudienceMembers = gameObject.GetComponentsInChildren<Animation>();
        string thisAnimation = Animations[0];

        foreach (Animation anim in AudienceMembers)
        {
            LoopAnimation(thisAnimation, anim);
            anim.transform.LookAt(new Vector3(Head.transform.position.x, anim.transform.position.y, Head.transform.position.z));            
        }
    }

    void FixedUpdate()
    {
        //foreach (Animation anim in AudienceMembers)
        //{
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        StartCoroutine(AnimationXtoY(anim, Random.Range(1, Animations.Length), 0, (Random.Range(0, 1.0f) + 3.0f)));
        //    }
        //}
    }

    public void Cheer()
    {
        if (!Cheering)
        {
            source2Playing = false;
            Cheering = true;

            //Sounds.volume = realVol;

            StartCoroutine(playHappyCrowd(0, Sounds1));

            StartCoroutine(StartAnimation(0.5f));
        }
        else if(!source2Playing)
        {
            source2Playing = true;
            StartCoroutine(playHappyCrowd(0, Sounds2));
        }
        else
        {
            source2Playing = false;
            StartCoroutine(playHappyCrowd(0, Sounds1));
        }
    }

    private void LoopAnimation(string thisAnimation, Animation anim)
    {
        anim.wrapMode = WrapMode.Loop;

        anim.CrossFade(thisAnimation);

        anim[thisAnimation].time = Random.Range(0f, 3f);
    }

    IEnumerator StartAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Animation anim in AudienceMembers)
        {
            StartCoroutine(AnimationXtoY(anim, Random.Range(1, Animations.Length), 0, (Random.Range(0, 1.0f) + 3.0f)));
        } 
    }

    IEnumerator AnimationXtoY(Animation anim, int X, int Y, float Time)
    {
        LoopAnimation(Animations[X], anim);

        yield return new WaitForSeconds(Time);

        LoopAnimation(Animations[Y], anim);
    }

    IEnumerator playHappyCrowd(float time, AudioSource source)
    {
        source.clip = CrowdNoises[Random.Range(0, CrowdNoises.Count - 1)];
        //Sounds.Play();
        source.PlayScheduled(time);
        //fadeSounds.Fade(CrowdNoises[Random.Range(1, 3)], realVol, false);

        //while (Sounds.volume < 0.5f)
        //{
        //    Sounds.volume = Mathf.Lerp(Sounds.volume, 1, Time.deltaTime);

        //    yield return new WaitForEndOfFrame();
        //}

        yield return new WaitForSeconds(3.0f);

        //while (Sounds.volume > 0.1)
        //{
        //    Sounds.volume = Mathf.Lerp(Sounds.volume, 0, Time.deltaTime);

        //    new WaitForEndOfFrame();
        //}        

        //Sounds.clip = CrowdNoises[0];
        //Sounds.Play();
        //Sounds.volume = realVol;

        //fadeSounds.Fade(CrowdNoises[0], realVol, false);

        Cheering = false;
    }
}

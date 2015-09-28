﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CrowdController : MonoBehaviour
{

    private string[] Animations = { "idle", "applause", "applause2", "celebration", "celebration2", "celebration3" };

    // Use this for initialization
    Animation[] AudienceMembers;

    public List<AudioClip> CrowdNoises = new List<AudioClip>();

    bool Cheering;

    AudioSource Sounds;

    GameObject Head;

    float realVol;

    void Start()
    {
        

        Sounds = GetComponent<AudioSource>();
        
        realVol = Sounds.volume;

        Head = GameObject.Find("VRCamera");

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
            Cheering = true;

            Sounds.volume = realVol;

            StartCoroutine(playHappyCrowd());

            foreach (Animation anim in AudienceMembers)
            {
                StartCoroutine(AnimationXtoY(anim, Random.Range(1, Animations.Length), 0, (Random.Range(0, 1.0f) + 3.0f)));
            }
        }
    }

    private void LoopAnimation(string thisAnimation, Animation anim)
    {
        anim.wrapMode = WrapMode.Loop;

        anim.CrossFade(thisAnimation);

        anim[thisAnimation].time = Random.Range(0f, 3f);
    }

    IEnumerator AnimationXtoY(Animation anim, int X, int Y, float Time)
    {
        LoopAnimation(Animations[X], anim);

        yield return new WaitForSeconds(Time);

        LoopAnimation(Animations[Y], anim);
    }

    IEnumerator playHappyCrowd()
    {
        Sounds.clip = CrowdNoises[Random.Range(1, 5)]; ;
        Sounds.Play();
        yield return new WaitForSeconds(3.0f);

        while (Sounds.volume > 0)
        {
            Sounds.volume -= 0.01f;

            new WaitForEndOfFrame();
        }

        Sounds.volume = realVol;

        Sounds.clip = CrowdNoises[0];
        Sounds.Play();
        Cheering = false;
    }
}
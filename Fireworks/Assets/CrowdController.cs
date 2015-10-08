using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CrowdController : MonoBehaviour
{
    // Use this for initialization
    Animator[] AudienceAnims;

    public List<AudioClip> CrowdNoises = new List<AudioClip>();

    bool Cheering;

    public GameObject audioSource;

    GameObject Head;

    void Start()
    {
        Head = GameObject.Find("Main Camera");

        AudienceAnims = GetComponentsInChildren<Animator>();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).LookAt(new Vector3(Head.transform.position.x, transform.GetChild(i).position.y, Head.transform.position.z));
        }

        RandomAnimationPos();
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
            if (!Cheering)
            {
                Cheering = true;

                Invoke("StartAnimation", 0.5f);
            }

            StartCoroutine(playHappyCrowd(1, 0));
        }
    }

    private void LoopAnimation(string thisAnimation, Animation anim)
    {
        anim.wrapMode = WrapMode.Loop;

        anim.CrossFade(thisAnimation);

        anim[thisAnimation].time = Random.Range(0f, 3f);
    }

    void RandomAnimationPos()
    {
        foreach (Animator anim in AudienceAnims)
        {
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            anim.Play(state.fullPathHash, -1, Random.value);
        }
    }

    void StartAnimation()
    {
        foreach (Animator anim in AudienceAnims)
        {
            anim.SetTrigger("Play");            
        }
    }

    IEnumerator AnimationXtoY(Animation anim, int X, int Y, float Time)
    {
        //LoopAnimation(Animations[X], anim);

        yield return new WaitForSeconds(Time);

        //LoopAnimation(Animations[Y], anim);
    }

    IEnumerator playHappyCrowd(float delay, float time)
    {
        yield return new WaitForSeconds(delay);

        GameObject go = Instantiate(audioSource);
        go.transform.parent = transform;

        AudioSource source = go.GetComponent<AudioSource>();
        source.clip = CrowdNoises[Random.Range(0, CrowdNoises.Count - 1)];
        source.PlayScheduled(time);

        yield return new WaitForSeconds(3.0f);

        Cheering = false;
    }
}

using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{
    // Use this for initialization
    public Vector3 axis = new Vector3(0, 0, 1);
    public float timeStamp;
    public float AmountOfTime = 1;
    public bool exit = false;
    public bool play = false;
    public bool lookedAt = false;
    bool paused = false;

    void Start()
    {
        timeStamp = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {         
        if (paused)
        {
            return;
        }
        //float angle = -((Time.time - timeStamp) / AmountOfTime) * 360.0f;
        //transform.localRotation = Quaternion.AngleAxis(angle, axis);       
        if (Time.time - timeStamp  > (AmountOfTime))
        {
            StartCoroutine(Delete());
            //Destroy(transform.parent.gameObject);
            if (exit)
            {
                Application.Quit();               
            }
            if (play)
            {
                Application.LoadLevel("AlexScene");                
            }
        }
    }

    public void Poof()
    {
        StartCoroutine(Delete());

        foreach(ParticleSystem sys in GetComponentsInChildren<ParticleSystem>())
        {
            sys.startSpeed = 2;
            sys.enableEmission = false;
            sys.playbackSpeed = 3;
        }
    }

    public void Reset()
    {     
        timeStamp = Time.time;      
        //StartCoroutine(ResetTrail(GetComponentInChildren<TrailRenderer>()));
    }

    static IEnumerator ResetTrail(TrailRenderer trail)
    {
        trail.time = 0;
        yield return new WaitForEndOfFrame(); 
        trail.time = 5;  
    } 

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(5);

        Destroy(transform.parent.gameObject);
    }
}

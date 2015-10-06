using UnityEngine;
using System.Collections;

public class TimerSpawner : MonoBehaviour {
	public GameObject lookable;
    public Vector3 prevOffset = Vector3.zero;
    public float dist = 20;
    public GameObject Head;
    public Transform[] XBounds = new Transform[2];
    public Transform[] YBounds = new Transform[2];
    public Transform[] ZBounds = new Transform[2];

	private float timer;
	public float timerMax = 1;

	// Use this for initialization
	void Start () 
    {
		timer = timerMax;
        Head = GameObject.Find("Main Camera");
	}

	// Update is called once per frame
	void Update () 
    {
		timer -= Time.deltaTime;

		if (timer <= 0) 
		{
			//Vector3 offset = Vector3.Normalize((prevOffset) + Random.onUnitSphere * Random.Range(50,150)) * dist;
			//offset.y = Mathf.Abs(offset.y);
            //offset.y /= 2;

            Vector3 offset = new Vector3(Random.Range(XBounds[0].position.x, XBounds[1].position.x),
                                         Random.Range(YBounds[0].position.y, YBounds[1].position.y),
                                         Random.Range(ZBounds[0].position.z, ZBounds[1].position.z)).normalized * dist;

            Vector3 t = offset;//transform.position + offset;
			GameObject g = (GameObject)GameObject.Instantiate(lookable, t, Quaternion.identity);
            g.layer = 8;
			g.transform.parent = this.transform;
            g.transform.LookAt(Head.transform.position);
			timer = timerMax;
            Debug.DrawRay(g.transform.position, g.transform.forward,Color.yellow,9999999);
            prevOffset = offset;
		}
	}
}

using UnityEngine;
using System.Collections;

public class TimerSpawner : MonoBehaviour {
	public GameObject lookable;
    public Vector3 prevOffset = Vector3.zero;
    public float dist = 20;
    public GameObject Head;
	// Use this for initialization
	void Start () {
		timer = timerMax;
        Head = GameObject.Find("VRCamera");
	}
	private float timer;
	public float timerMax = 1;
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) 
		{
			Vector3 offset = Vector3.Normalize((prevOffset)+ Random.onUnitSphere*Random.Range(50,150))*dist;
			offset.y = Mathf.Abs(offset.y);
            offset.y /= 2;
			Vector3 t = transform.position + offset;
			GameObject g = (GameObject)GameObject.Instantiate(lookable, t, Quaternion.identity);
           
			g.transform.parent = this.transform;
            g.transform.LookAt(Head.transform.position);
			timer = timerMax;
            Debug.DrawRay(g.transform.position, g.transform.forward,Color.yellow,9999999);
            prevOffset = offset;
		}
	}
}

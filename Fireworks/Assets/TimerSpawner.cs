using UnityEngine;
using System.Collections;

public class TimerSpawner : MonoBehaviour {
	public GameObject lookable;
	// Use this for initialization
	void Start () {
		timer = timerMax;

	}
	private float timer;
	public float timerMax = 1;
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) 
		{
			Vector3 offset = Random.onUnitSphere*20;
			offset.y = Mathf.Abs(offset.y);
			Vector3 t = Camera.main.transform.position + offset;
			GameObject g = (GameObject)GameObject.Instantiate(lookable, t, Quaternion.identity);


           
			g.transform.parent = this.transform;
            g.transform.LookAt(Camera.main.transform.position);
			timer = timerMax;
            Debug.DrawRay(g.transform.position, g.transform.forward,Color.yellow,9999999);
		}
	}
}

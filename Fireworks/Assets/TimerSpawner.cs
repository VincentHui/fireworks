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
			Vector3 t = Camera.main.transform.position + Random.onUnitSphere*10;
			GameObject g = (GameObject)GameObject.Instantiate(lookable, t, Quaternion.LookRotation(-(t- Camera.main.transform.position)));
			g.transform.parent = this.transform.parent;
			timer = timerMax;
		}
	}
}

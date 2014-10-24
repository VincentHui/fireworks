using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour {
	public GameObject star;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 200; i++) {
			Vector3 offset = Random.onUnitSphere*(i<100?30:60);
			offset.y = Mathf.Abs(offset.y);
			Vector3 t = Camera.main.transform.position + offset;
			GameObject g = (GameObject)GameObject.Instantiate(star, t, Quaternion.identity);
			
			g.transform.parent = this.transform;
			g.transform.LookAt(Camera.main.transform.position);
			Debug.DrawRay(g.transform.position, g.transform.forward,Color.yellow,9999999);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Ray r = new Ray (this.transform.position, transform.TransformDirection (Vector3.forward));

		Debug.DrawRay (this.transform.position, transform.TransformDirection (Vector3.forward));
		RaycastHit hit = new RaycastHit ();
		if(Physics.Raycast(r, out hit, 20))
		{
			GameObject.Destroy(hit.transform.gameObject);
		}
	}
}

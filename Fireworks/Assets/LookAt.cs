using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookAt : MonoBehaviour {
    GameObject[] _fireworks;
	// Use this for initialization
	void Start () {
        _fireworks = Resources.LoadAll<GameObject>("Fireworks");
        Debug.Log(_fireworks.Length);
	}
    
	// Update is called once per frame
	void Update () {
		Ray r = new Ray (this.transform.position, transform.TransformDirection (Vector3.forward * 10));

		Debug.DrawRay (this.transform.position, transform.TransformDirection (Vector3.forward));
		RaycastHit hit = new RaycastHit ();
		if(Physics.Raycast(r, out hit, 50))
		{
            makeFirework(hit.transform.gameObject);
            GameObject.Destroy(hit.transform.gameObject);
           
		}
	}

    void makeFirework(GameObject hit)
    {
        GameObject.Instantiate(_fireworks[Random.Range(0, _fireworks.Length)], new Vector3(hit.transform.position.x, -30.0f, hit.transform.position.z), Quaternion.identity);
        Debug.Log("made firework");
    }
}

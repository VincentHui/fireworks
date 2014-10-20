using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour {

	// Use this for initialization
    public Vector3 axis = new Vector3(0, 1, 0);
    public float timeStamp;
    public float AmountOfTime = 1;   
	void Start () {
        timeStamp = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float angle = (Time.time - timeStamp) / AmountOfTime * Mathf.PI*2;
        transform.Rotate(axis, angle);
       
        if (Time.time- timeStamp>(AmountOfTime))
        {

         
          Destroy(this);
        }
	}
}

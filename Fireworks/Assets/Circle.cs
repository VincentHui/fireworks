using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour
{

    // Use this for initialization
    public Vector3 axis = new Vector3(0, 0, 1);
    public float timeStamp;
    public float AmountOfTime = 1;
    void Start()
    {
        timeStamp = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float angle = -((Time.time - timeStamp) / AmountOfTime) * 360.0f;
        transform.localRotation = Quaternion.AngleAxis(angle, axis);
        if (Time.time - timeStamp > (AmountOfTime))
        {
          

            transform.GetComponentInChildren<TrailRenderer>().materials[0].color = new Color(1, 0, 0, 1-(Time.time-AmountOfTime - timeStamp));
             }
        if (Time.time - timeStamp - 1 > (AmountOfTime))
        {

            Destroy(transform.parent.gameObject);
        }
      
       
    }
}

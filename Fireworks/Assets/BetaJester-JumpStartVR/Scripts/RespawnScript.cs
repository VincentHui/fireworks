using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour
{

    public Transform RespawnPoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        // Make sure the "Player" tag is set on the player
        // Make sure the "Player" tag is set on the player
        if (col.CompareTag("Player"))
        {
            col.transform.position = RespawnPoint.position;
        }
    }
}

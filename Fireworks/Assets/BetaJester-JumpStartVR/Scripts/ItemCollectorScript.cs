using UnityEngine;
using System.Collections;
using System;

public class ItemCollectorScript : MonoBehaviour {

    public int NumberCollected = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void ItemCollected(string tag)
    {
        NumberCollected++;
    }
}

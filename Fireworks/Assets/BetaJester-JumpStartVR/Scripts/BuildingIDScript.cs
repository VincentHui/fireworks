using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildingIDScript : MonoBehaviour {

    public int ID;
    public GameObject TriggerPoint;
    public GameObject LandingPoint;
    public BNG_ZapperAction ZapperAction;
    List<BuildingIDScript> closestBuildingScripts = new List<BuildingIDScript>();

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void PushToTrigger(GameObject target, string Function, GameObject obj)
    {
        ZapperAction.GameObjectToActivate = target;
        ZapperAction.CallFunctionOnActivate = Function;
        ZapperAction.OnActivateValue = obj;
    }

    internal void SetClosestBuildingScripts(List<GameObject> closestIDs)
    {
        foreach(GameObject i in closestIDs)
        {
            closestBuildingScripts.Add(i.GetComponent<BuildingIDScript>());
        }
    }

   
    private void SetClosestState(bool State)
    {
        foreach (BuildingIDScript i in closestBuildingScripts)
        {
            i.OnCloseStateChange(State);
        }
    }

    internal void OnCloseStateChange(bool State)
    {
        TriggerPoint.SetActive(State);
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            SetClosestState(true);
            TriggerPoint.SetActive(false);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            SetClosestState(false);
        }
    }
}

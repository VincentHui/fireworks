﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookAt : MonoBehaviour {
    GameObject[] _fireworks;
    GameObject menu;
    public float test = 0;
    CrowdController CC;

	// Use this for initialization
	void Start () 
    {
        _fireworks = Resources.LoadAll<GameObject>("Fireworks");
        menu = GameObject.Find("menu");
        CC = GameObject.Find("Crowd").GetComponent<CrowdController>();	
	}
    
	// Update is called once per frame
	void FixedUpdate () 
    {
        bool play = false;
        bool exit = false;
        int layerMask = 1 << 8;

		Ray r = new Ray (this.transform.position, transform.TransformDirection (Vector3.forward)*200);

		Debug.DrawRay (this.transform.position, transform.TransformDirection (Vector3.forward)*200);
        
		RaycastHit hit = new RaycastHit ();

		if(Physics.Raycast(r, out hit, 500, layerMask))
		{
            makeFirework(hit.transform.gameObject,ref play,ref exit);
            CC.Cheer();
		}
        if (menu)
        {
            if (!play)
            {
                menu.GetComponent<MenuManager>().play.GetComponentInChildren<Circle>().Reset();
            }
            if (!exit)
            {
                menu.GetComponent<MenuManager>().exit.GetComponentInChildren<Circle>().Reset();
            }
        }
	}

    void makeFirework(GameObject hit,ref bool play, ref bool exit)
    {
        if (!hit || !hit.GetComponentInChildren<Circle>()) return;

        hit.GetComponent<SphereCollider>().enabled = false;
        hit.GetComponentInChildren<Circle>().Poof();

        if (hit.GetComponentInChildren<Circle>().play)
        {
            play = true;
            return;
        }
        if (hit.GetComponentInChildren<Circle>().exit)
        {
            exit = true;
            return;
        }
        GameObject go = (GameObject)GameObject.Instantiate(_fireworks[Random.Range(0, _fireworks.Length)], new Vector3(hit.transform.position.x, -90, hit.transform.position.z), Quaternion.identity);

        ParticleSystem ps = go.GetComponent<ParticleSystem>();

        ps.startLifetime = (hit.transform.position.y+90) / 90;
        test = hit.transform.position.y; 

        //ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];

        //ps.GetParticles(particles);

        //particles[0].lifetime = hit.transform.position.y / 90;
    }
}

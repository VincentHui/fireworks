using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookAt : MonoBehaviour {
    GameObject[] _fireworks;

    public float test = 0;
	public int CountdownToLogo = 1;
	private int countdown;

	public GameObject logo;
	// Use this for initialization
	void Start () {
        _fireworks = Resources.LoadAll<GameObject>("Fireworks");
        Debug.Log(_fireworks.Length);
		logo.renderer.material.color = new Color (1, 1, 1, 0);
		countdown = CountdownToLogo;
	}
    
	// Update is called once per frame
	void Update () {
		Ray r = new Ray (this.transform.position, transform.TransformDirection (Vector3.forward)*20);

		Debug.DrawRay (this.transform.position, transform.TransformDirection (Vector3.forward)*20)
          ;
		RaycastHit hit = new RaycastHit ();
		if(Physics.Raycast(r, out hit, 50))
		{
            makeFirework(hit.transform.gameObject);    

			countdown--;

			if(countdown<=0)
			{

				//begin the logo fade in
				StartCoroutine("fadeIn");
				countdown = CountdownToLogo;
			}
		}
	}

	IEnumerator fadeIn()
	{
		// fade in
		for (int i = 0; i < 10; i++) {	
			Color c= logo.renderer.material.color;
			c.a = i/10.0f;
			logo.renderer.material.color = c;
			Debug.Log ("showing logo: " + i);			
			yield return new WaitForSeconds (0.1f);

		}

		// fade out
		for (int i = 10 - 1; i >= 0; i--) {
			Color c= logo.renderer.material.color;
			c.a = i/10.0f;
			logo.renderer.material.color = c;
			Debug.Log ("showing logo: " + i);			
			yield return new WaitForSeconds (0.1f);
		}
	}


    void makeFirework(GameObject hit)
    {
        GameObject go = (GameObject)GameObject.Instantiate(_fireworks[Random.Range(0, _fireworks.Length)], new Vector3(hit.transform.position.x, 0, hit.transform.position.z), Quaternion.identity);

        ParticleSystem ps = go.GetComponent<ParticleSystem>();

        ps.startLifetime = hit.transform.position.y / 90;
        test = hit.transform.position.y; 

        //ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];

        //ps.GetParticles(particles);

        //particles[0].lifetime = hit.transform.position.y / 90;


        GameObject.Destroy(hit.transform.gameObject);
        Debug.Log("made firework");
    }
}

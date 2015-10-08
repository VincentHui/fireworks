using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowCurrentCollectablesScript : MonoBehaviour
{

    Text text;
    public int MaxCollectables;
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = player.GetComponent<ItemCollectorScript>().NumberCollected + "/" + MaxCollectables;
    }
}

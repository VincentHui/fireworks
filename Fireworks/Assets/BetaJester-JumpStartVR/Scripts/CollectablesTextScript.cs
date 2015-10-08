using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectablesTextScript : MonoBehaviour {

    public int MaxNumOfCollectables;
    TypogenicText typoGenic;
    Text textNorm;
    int statCollected;

    public string thisLevel = "";

	// Use this for initialization
	void Start () {
        typoGenic = this.GetComponent<TypogenicText>();
        LoadLevelStats();

        if(typoGenic != null)
        typoGenic.Text = statCollected + "/" + MaxNumOfCollectables;
        else
            textNorm.text = statCollected + "/" + MaxNumOfCollectables;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void LoadLevelStats()
    {
        statCollected = SaveData.GetIntData(thisLevel + "Collected", 0);
        //SaveData.AddToStringDictionary(Application.loadedLevelName + "Rank", Rank);
    }
}

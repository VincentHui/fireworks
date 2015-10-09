using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]
public class SetDrawLayer : MonoBehaviour {

    public int Layer = 0;
	void Update () {
        GetComponent<MeshRenderer>().sortingOrder = Layer;
	}
}

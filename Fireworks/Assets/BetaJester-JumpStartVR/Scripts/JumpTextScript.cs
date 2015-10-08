using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JumpTextScript : MonoBehaviour
{

    TypogenicText textMesh;
    Text textNorm;

    public static int JumpCount;
    // int oldJumpCount;

    public bool onUpdate = false;

    // Use this for initialization
    void Start()
    {
        textMesh = GetComponent<TypogenicText>();

        if (textMesh != null)
            textMesh.Text = "" + JumpTextScript.JumpCount;
        else
        {
            textNorm = GetComponent<Text>();
            textNorm.text = "" + JumpTextScript.JumpCount;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (onUpdate)
        {
            if (textMesh == null)
            {
                textMesh = GetComponent<TypogenicText>();

                if (textMesh == null)
                {
                    if (textNorm == null)
                        textNorm = GetComponent<Text>();

                    textNorm.text = "" + JumpTextScript.JumpCount;
                }
            }
            else
            {
                textMesh.Text = "" + JumpTextScript.JumpCount;
            }
            //oldJumpCount = JumpCount;
        }
    }

    void OnEnable()
    {
        //if (JumpCount != oldJumpCount)
        {
            if (textMesh == null)
            {
                textMesh = GetComponent<TypogenicText>();

                if (textMesh == null)
                {
                    if(textNorm == null)
                        textNorm = GetComponent<Text>();

                    textNorm.text = "" + JumpTextScript.JumpCount;
                }
            }
            else
            {
                textMesh.Text = "" + JumpTextScript.JumpCount;
            }
            //oldJumpCount = JumpCount;
        }
    }

    internal static void Reset()
    {
        JumpCount = 0;
    }
}

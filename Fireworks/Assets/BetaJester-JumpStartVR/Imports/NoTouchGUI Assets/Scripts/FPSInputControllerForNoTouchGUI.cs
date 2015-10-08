using UnityEngine;
using System.Collections;

public class FPSInputControllerForNoTouchGUI : MonoBehaviour
{

    GameObject camGroup ;
    private CharacterMotor motor;

    public int autowalk =0;
    public int inhibit_autowalk=1;
    private int jumpcommand=0;

    // Use this for initialization
    public void Awake () {
        motor = this.GetComponent<CharacterMotor>();
    }

    public void jumpAndSpin(){

        jumpcommand = 1;

    }

    public void toggle_autowalk()
    {
        if (autowalk == 0) 
            autowalk = 1;
        else 
            autowalk = 0;
    }

    // Update is called once per frame
    public void Update () {
        // Get the input vector from keyboard or analog stick
        var directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	
        if (autowalk==1)
            directionVector= new Vector3(0,0,1*inhibit_autowalk);

        if (directionVector != Vector3.zero) {
            // Get the length of the directon vector and then normalize it
            // Dividing by the length is cheaper than normalizing when we already have the length anyway
            var directionLength = directionVector.magnitude;
            directionVector = directionVector / directionLength;
		
            // Make sure the length is no bigger than 1
            directionLength = Mathf.Min(1, directionLength);
		
            // Make the input vector more sensitive towards the extremes and less sensitive in the middle
            // This makes it easier to control slow speeds when using analog sticks
            directionLength = directionLength * directionLength;
		
            // Multiply the normalized direction vector by the modified length
            directionVector = directionVector * directionLength;
        }
	
        // Apply the direction to the CharacterMotor
        motor.inputMoveDirection = camGroup.transform.rotation * directionVector; //adding the cam group to this makes it so you move in the direction you're looking
        motor.inputJump = Input.GetButton("Jump");

        if(jumpcommand==1){
            motor.inputJump=true;	
            jumpcommand=0;
        }
    }

    // Require a character controller to be attached to the same game object
}

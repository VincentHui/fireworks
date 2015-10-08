using UnityEngine;
using System.Collections;

public class MoveToBuildingScript : MonoBehaviour
{

    public BuildingIDScript StartingBuilding;
    BuildingIDScript nextBuilding;
    public BuildingIDScript LastBuilding;

    bool isMoving = false;
    bool isHalfway = false;

    public float MoveSpeed = 3f;

    public float ThresholdDistance = 1f;

    float PercentageMoved = 0;

    public AudioClip JumpSound;
    public AudioClip LandSound;
    AudioSource audSource;
    bool hasJumped = false;

    Vector3 halfway;
    // Use this for initialization
    void Start()
    {
        audSource = this.GetComponent<AudioSource>();
    }

    Transform[] waypoints = new Transform[0];

    int followIndex = 0;
    bool moving = false;
    private bool isStartedMoving = false;
    private bool endSet;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (LevelManagerScript.levelState == LevelManagerScript.LevelState.Playing)
        {
            if (isStartedMoving)
            {
                if (!hasJumped)
                {
                    hasJumped = true;
                    audSource.clip = JumpSound;
                    audSource.Play();
                }

                PercentageMoved += Time.deltaTime * MoveSpeed;

                this.transform.position = new Vector3(
                    Mathf.Lerp(StartingBuilding.LandingPoint.transform.position.x, nextBuilding.LandingPoint.transform.position.x, PercentageMoved),
                    Mathf.Lerp(StartingBuilding.LandingPoint.transform.position.y, nextBuilding.LandingPoint.transform.position.y, PercentageMoved) + ((Vector3.Distance(nextBuilding.LandingPoint.transform.position, StartingBuilding.LandingPoint.transform.position) / 2) * Mathf.Sin((Mathf.PI) * PercentageMoved)),
                    Mathf.Lerp(StartingBuilding.LandingPoint.transform.position.z, nextBuilding.LandingPoint.transform.position.z, PercentageMoved)
                    );

                if ((this.transform.position - nextBuilding.LandingPoint.transform.position).sqrMagnitude < 0.01F || PercentageMoved >= 1)
                {
                    StartingBuilding = nextBuilding;
                    isStartedMoving = false;
                    PercentageMoved = 0;


                    hasJumped = false;
                    audSource.clip = LandSound;
                    audSource.Play();
                }
            }
        }

        if (!endSet  && StartingBuilding == LastBuilding)
        {
            LevelManagerScript.EndGame();
            endSet = true;
        }
    }

    void OnBuildingTriggered(GameObject newBuilding)
    {
        if (newBuilding != nextBuilding)
            nextBuilding = newBuilding.GetComponent<BuildingIDScript>();

        isHalfway = false;

        isStartedMoving = true;

        JumpTextScript.JumpCount++;

    }
}

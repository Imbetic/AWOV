using UnityEngine;
using System.Collections;

public class BeastAI : CharacterActions
{
    public Transform Target;
    Vector2 TargetPosition;
    public LayerMask DodgePlatformsCollision;
    public GameObject SourceOfMadness;

    public float ObservationFrequency = 0.3f;
    float ObservationTimer;

    bool PreviousJump;

    // Use this for initialization
    void Start()
    {
        //Target = GameObject.Find("Player").transform;
        ObservationTimer = ObservationFrequency;
    }

    void Update()
    {
        ObservationTimer -= Time.deltaTime;

        if(ObservationTimer <= 0)
        {
            TargetPosition.Set(Target.transform.position.x, Target.transform.position.y);
            ObservationTimer = ObservationFrequency;
        }
    }

    public override void UpdateMovementCommands()
    {
        PreviousJump = jump;
        base.UpdateMovementCommands();

        if (Target != null)
        {

            if (TargetPosition.x > transform.position.x)
            {
                moveRight = true;
            }
            else moveRight = false;
            if (TargetPosition.x < transform.position.x)
            {
                moveLeft = true;
            }
            else moveLeft = false;

            if (TargetPosition.y - 0.5f > transform.position.y)
            {
                jump = true;
            }
            else jump = false;

            if (TargetPosition.y + 0.5f < transform.position.y)
            {
                moveDown = true;
            }
            else moveDown = false;

            RaycastHit2D JumpCheck = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), 1, DodgePlatformsCollision);

            if (JumpCheck.collider != null)
            {
                jump = true;
            }

            if(GetComponent<RaycastPlatformer>().JumpAscendDuration <= 0f && PreviousJump)
            {
                jump = false;
            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SourceOfMadness.GetComponent<ParticleSource>().CreateParticles(100, Vector2.zero);
    }

    // Update is called once per frame

}

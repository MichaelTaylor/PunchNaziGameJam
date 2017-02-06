using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [Header("General Variables")]
    public float Speed;
    public bool MovingLeft;
    public GameObject Bullet;

    [Header("Vector Variables")]
    public Vector2 StartingPoint;
    public float TimeToSwitch;

    [Header("Timer Variables")]
    public float MoveTimer;

    private Rigidbody2D RB2D;

    public EnemyStates States;
    public enum EnemyStates
    {
        Idle,
        Moving,
        Attacking,
        Alerted
    };  

	// Use this for initialization
	void Start ()
    {
        GetVariables();
	}

    void GetVariables()
    {
        StartingPoint = transform.position;
    }

    void GetProperties()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        StateChecker();	
	}

    void DetectPlayer()
    {

    }

   /* private bool IsMovingLeft()
    {
        if (MoveTimer)
        {
            return false;
        }
        else
        {
            return true;
        }
    }*/

    void WalkBackAndForth()
    {
        MoveTimer = AllPurposeTimer(MoveTimer);
        if (MoveTimer > TimeToSwitch)
        {
            MoveTimer = 0f;
            if (MovingLeft) MovingLeft = false;
            else MovingLeft = true;
        }

        if (!MovingLeft)
        {
            EnemyMovement(1);
        }
        else
        {
            EnemyMovement(-1);
        }
    }

    void EnemyMovement(float dir)
    {
        transform.position += new Vector3(dir, 0f, 0f) * (Time.deltaTime * Speed);
    }

    void IdleThenMove()
    {
        if (Time.time > 2f)
        {
            States = EnemyStates.Moving;
        }
    }

    void StateChecker()
    {
        switch(States)
        {
            case EnemyStates.Idle:
                {
                    IdleThenMove();
                    break;
                }
            case EnemyStates.Moving:
                {
                    WalkBackAndForth();
                    break;
                }
            case EnemyStates.Alerted:
                {
                    break;
                }
            case EnemyStates.Attacking:
                {
                    break;
                }
        }
    }

    private float AllPurposeTimer(float Timer)
    {
        return Timer += Time.deltaTime * 1f;
    }
}

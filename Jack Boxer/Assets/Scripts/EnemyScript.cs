using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [Header("General Variables")]
    public float Speed;
    public bool MovingLeft;
    private GameObject Player;
    public Transform VisiblityLimit;
    public bool Alerted;

    [Header("Vector Variables")]
    public Vector2 StartingPoint;
    public float TimeToSwitch;

    [Header("Attacking Variables")]
    public GameObject Bullet;
    public float BulletForce;
    public bool CanShoot;
    public Transform BulletSpawnPoint;

    [Header("Timer Variables")]
    public float MoveTimer;
    public float ResetShootTimer;

    [Header("Gameplay Variables")]
    private GameplayMNG GamePlayManager;
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
        GetProperties();
	}

    void GetVariables()
    {
        StartingPoint = transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        CanShoot = true;
    }

    void GetProperties()
    {
        GamePlayManager = GameObject.FindObjectOfType<GameplayMNG>();
        RB2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        DetectPlayer();
        StateChecker();	
	}

    void DetectPlayer()
    {
        Alerted = Physics2D.Linecast(transform.position, VisiblityLimit.position, 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawLine(transform.position, VisiblityLimit.position, Color.green);

        if (Alerted)
        {
            States = EnemyStates.Attacking;
        }
    }

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
        ScaleChecker(dir);
    }

    void ScaleChecker(float dir)
    {
        transform.localScale = new Vector2(-0.5f * dir, 3f);
    }

    void ResetShooting()
    {
        if (!CanShoot) ResetShootTimer = AllPurposeTimer(ResetShootTimer);
        else ResetShootTimer = 0f;

        if (ResetShootTimer > 1.5f) CanShoot = true;
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
                    ResetShooting();
                    break;
                }
            case EnemyStates.Attacking:
                {
                    Shooting();
                    break;
                }
        }
    }

    void Shooting()
    {
        if (Alerted)
        {
            if (CanShoot)
            {
                GameObject NewBullet = Instantiate(Bullet, BulletSpawnPoint.position, Quaternion.identity) as GameObject;
                NewBullet.GetComponent<Rigidbody2D>().AddForce((Player.transform.position - transform.position) * BulletForce, ForceMode2D.Impulse);
                Destroy(NewBullet, 2f);
                CanShoot = false;
            }
        }
        else
        {
            States = EnemyStates.Moving;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            DeathFunction();
        }
    }

    void DeathFunction()
    {
        Destroy(gameObject);
        GamePlayManager.PlaySFX(Player.GetComponent<PlayerScript>().GloveSFX[1]);
    }

    private float AllPurposeTimer(float Timer)
    {
        return Timer += Time.deltaTime * 1f;
    }
}

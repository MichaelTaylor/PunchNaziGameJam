using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    [Header("General Variables")]
    public float Speed;

    [Header("Jump Variables")]
    public float JumpForce;
    public float JumpEscalation;
    public float MaxJumpForce;
    public bool IsGrounded;

    [Header("Attack Variables")]
    public bool IsAttacking;
    public GameObject SideAttack;
    public GameObject TopAttack;

    [Header("Various Timers")]
    public float AttackTimer;

    [Header("Sound Variables")]
    public AudioSource audiosource;
    public AudioClip[] GloveSFX;
    public AudioClip[] JumpSFX;

    [Header("Gameplay Variables")]
    private GameplayMNG GamePlayManager;
    private Rigidbody2D RB2D;

    void Start()
    {
        GetProperties();
        SetVariables();
    }

    void SetVariables()
    {
        TopAttack.SetActive(false);
        SideAttack.SetActive(false);
    }

    void GetProperties()
    {
        GamePlayManager = GameObject.FindObjectOfType<GameplayMNG>();
        RB2D = GetComponent<Rigidbody2D>();
    }

    void BoolCheckers()
    {
        IsGrounded = CheckGround();
        IsAttacking = CheckAttacking();
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        BoolCheckers();
        CharacterMovement(xAxis);
        JumpAttacking();
        AttackControls();
    }

    void CharacterMovement(float dir)
    {
        RB2D.velocity = new Vector2(dir * Speed, RB2D.velocity.y);
        ScaleChecker();
        JumpControls();
    }

    void AttackControls()
    {
        if (Input.GetButton("Fire1")) SideAttacking();
        if (Input.GetButtonDown("Fire1")) GamePlayManager.PlaySFX(GloveSFX[0]);
    }

    void JumpControls()
    {
        if (Input.GetButton("Jump")) ChargeJump();
        if (Input.GetButtonUp("Jump")) Jump();
        if (Input.GetButtonDown("Jump")) GamePlayManager.PlaySFX(JumpSFX[0]);
    }

    void ChargeJump()
    {
        if (IsGrounded)
        {
            JumpForce += JumpEscalation * Time.deltaTime;
            if (JumpForce > MaxJumpForce) JumpForce = MaxJumpForce;
        }
    }

    void Jump()
    {
        RB2D.velocity = new Vector2(RB2D.velocity.x, JumpForce);
        IsGrounded = false;
        ResetVariables();
        GamePlayManager.PlaySFX(JumpSFX[1]);
    }
    
    void SideAttacking()
    {      
        if (IsAttacking)
        {       
            if (AttackTimer < 0.5f)
            {
                SideAttack.SetActive(true);
                AttackTimer += AllPurposeTimer(AttackTimer);
            }
            else
            {
                SideAttack.SetActive(false);
            }  
        }
    }

    void JumpAttacking()
    {
        if (IsGrounded)
        {
            TopAttack.SetActive(false);
        }
        else
        {
            TopAttack.SetActive(true);
        }
    }

    void ScaleChecker()
    {
        if (RB2D.velocity.x != 0)
        {
            if (RB2D.velocity.x > 0)
            {
                ScaleChanger(1);
            }
            else if (RB2D.velocity.x < 0)
            {
                ScaleChanger(-1);
            }
        }
    }

    void ScaleChanger(float ScaleDir)
    {
        transform.localScale = new Vector2(ScaleDir, 1);
    }

    private bool CheckGround()
    {
        Collider2D GroundObjects = Physics2D.OverlapCircle(new Vector2(transform.position.x,
            (transform.position.y - 0.5f)), 0.25f, 1 << LayerMask.NameToLayer("Ground"));
        if (GroundObjects != null)
        {
            return true;
        }
        return false;
    }

    private bool CheckAttacking()
    {
        if (Input.GetButton("Fire1"))
        {
            return true;
        }
        else
        {
            AttackTimer = 0;
            SideAttack.SetActive(false);
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector2(transform.position.x, transform.position.y - 0.5f), 0.25f);
    }

    void ResetVariables()
    {
        JumpForce = 0f;
    }

    void ResetAttack()
    {
        TopAttack.SetActive(false);
        SideAttack.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
            DeathFunction();
        }
    }

    private float AllPurposeTimer(float Timer)
    {
        return Timer += Time.deltaTime * 1f;
    }

    public void DeathFunction()
    {
        GamePlayManager.GameOver();
    }
}

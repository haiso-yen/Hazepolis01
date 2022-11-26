using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eammon : MonoBehaviour
{
    //Singleton Mode
    public static Eammon instance;
    //change scene
    public string scenePassWord;

    private SkeletonAnimation skeletonAnimation;
    private string previousState, currentState ;
    private Rigidbody2D rigid2D;
    private CircleCollider2D collider2D;
    private CapsuleCollider2D capcollider2D;
    //private Animator animator;
    private float jumpForce = 2000.0f;
    public float walkForce = 25.0f;
    public float maxWalkForce = 6.0f;
    private bool isJumping = false;
    private bool afterSu = false;
    public int key ;

    bool inputEnabled = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        rigid2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<CircleCollider2D>();
        capcollider2D = GetComponent<CapsuleCollider2D>();
        skeletonAnimation.state.SetAnimation(0, "sleep", false);
        currentState = "sleep";
    }
    void Update()
    {
        //對話時角色停止移動
        if (DialogManager.isActive == true)
            return;

        if (inputEnabled == true)
        {
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box" || collision.gameObject.tag == "Bridge") && afterSu)
        {
            isJumping = false;
            skeletonAnimation.state.SetAnimation(0, currentState, true);
        }
    }
    private void WakeUp()
    {
        afterSu = true;
    }
    private void Move()
    {
        if (afterSu == false)
        {
            previousState = currentState;
            currentState = "su";
            if (previousState != currentState)
            {
                skeletonAnimation.state.SetAnimation(0, currentState, false);
            }
            else
            {
                Invoke("WakeUp", 7f);
            }
            previousState = currentState;
        }
        else if (afterSu)
        {
            key = 0;
            //jump
            if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
            {
                isJumping = true;
                rigid2D.AddForce(transform.up * jumpForce);
                skeletonAnimation.state.SetAnimation(0, "jump", false);
            }
            //basic movement
            if (Input.GetKey(KeyCode.A))
            {
                key = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                key = 1;
            }
            //Crouch or Standing
            if (Input.GetKey(KeyCode.S)){
                currentState = key == 0 ? "Crouch idle2" : "Crouch walk2";
                maxWalkForce = 3.0f;
                collider2D.enabled = false;
            }
            else
            {
                currentState = key == 0 ? "idle" : "walk";
                maxWalkForce = 6.0f;
                collider2D.enabled = true;
            }

            float speedx = Mathf.Abs(this.rigid2D.velocity.x);
            if (speedx < maxWalkForce)
            {
                rigid2D.AddForce(transform.right * key * walkForce);

            }
            if (previousState != currentState)
            {
                skeletonAnimation.state.SetAnimation(0, currentState, true);
            }
            previousState = currentState;

            if (key != 0)
            {
                skeletonAnimation.Skeleton.ScaleX = key > 0 ? -1f : 1f;
            }
        }
    }
    private void Magic()
    {
        skeletonAnimation.state.SetAnimation(0, "magic", false);
    }

    private void Magic2()
    {
        skeletonAnimation.state.SetAnimation(0, "magic 2", false);
    }

    public void Activate()
    {
        inputEnabled = true;
        gameObject.GetComponent<FollowingPlayer>().enabled = true;
    }

    public void Deactivate()
    {
        inputEnabled = false;
        gameObject.GetComponent<FollowingPlayer>().enabled = false;
        gameObject.GetComponent<FollowingPlayer>().positionList.Clear();
    }
}



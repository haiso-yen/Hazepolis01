using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dan : MonoBehaviour
{

    //Singleton Mode
    public static Dan instance;

    private SkeletonAnimation skeletonAnimation;
    private string previousState, currentState;
    private Rigidbody2D rigid2D;
    private CircleCollider2D collider2D;
    private CapsuleCollider2D capcollider2D;
    private float jumpForce = 1000.0f;
    private float walkForce = 20.0f;
    private float maxWalkForce = 4.0f;
    private bool isJumping = false;
    public int key;

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


    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        rigid2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<CircleCollider2D>();
        capcollider2D = GetComponent<CapsuleCollider2D>();
        skeletonAnimation.state.SetAnimation(0, "idle", false);
        currentState = "idle";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Box")
        {
            isJumping = false;
            skeletonAnimation.state.SetAnimation(0, currentState, true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (inputEnabled == true)
        {
            Move();
        }
    }

    private void Move()
    {
        key = 0;
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            isJumping = true;
            rigid2D.AddForce(transform.up * jumpForce);
            skeletonAnimation.state.SetAnimation(0, "jump", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            key = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            key = 1;
        }
        //Crouch or Standing
        if (Input.GetKey(KeyCode.S))
        {
            currentState = key == 0 ? "crouchidle" : "crouchwalk";
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

    private void Magic()
    {
        skeletonAnimation.state.SetAnimation(0, "magic", false);
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

using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;



public enum CharacterState {
    IDLE,
    RUNNING,
    JUMPING,
    DEAD
}
public class PlayerController : MonoBehaviour {
    public CharacterState mPlayerState = CharacterState.IDLE;

    [Header("Movement Settings")]
    public float mSpeed = 5.0f;
    public float mJumpStrength = 10.0f;

    // Setting the animated sprites for the different states
    [Header("State Sprites")]
    public RuntimeAnimatorController mIdleController;
    public RuntimeAnimatorController mRunningController;
    public RuntimeAnimatorController mJumpingController;


    // We'll be caching the animator component to easily change current player animation
    private Animator _mAnimatorComponent;

    // Tracking the direction our player is going
    private bool _bIsGoingRight = true;
    private bool _bPlayerStateChanged = false;


    void Start()
    {
        _mAnimatorComponent = gameObject.GetComponent<Animator>();
        _mAnimatorComponent.runtimeAnimatorController = mIdleController;
    }

    void Update()
    {

        _bPlayerStateChanged = false;
        // check state changes
        if (mPlayerState == CharacterState.IDLE)
        {
            if (Input.GetKey(KeyCode.RightArrow) || (Input.GetKey(KeyCode.LeftArrow)))
            {
                _bPlayerStateChanged = true;
                mPlayerState = CharacterState.RUNNING;
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    _bIsGoingRight = true;
                }
                else
                {
                    _bIsGoingRight = false;
                }
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * mJumpStrength;
                _bPlayerStateChanged = true;
                mPlayerState = CharacterState.JUMPING;
                StartCoroutine("CheckGrounded");
            }
        }
        else if (mPlayerState == CharacterState.RUNNING)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * mJumpStrength;
                _bPlayerStateChanged = true;
                mPlayerState = CharacterState.JUMPING;
                StartCoroutine("CheckGrounded");
            }
            else if (!Input.GetKey(KeyCode.RightArrow) && (!Input.GetKey(KeyCode.LeftArrow)))
            {
                _bPlayerStateChanged = true;
                mPlayerState = CharacterState.IDLE;
            }
        }

        if (mPlayerState == CharacterState.JUMPING || mPlayerState == CharacterState.RUNNING)
        {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _bIsGoingRight = true;
            transform.Translate(transform.right * Time.deltaTime * mSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _bIsGoingRight = false;
            transform.Translate(-transform.right * Time.deltaTime * mSpeed);
        }
    }
    }

    IEnumerator CheckGrounded()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position - Vector3.up * 1f, -Vector2.up, 0.05f);
            if (hit.collider != null)
            {
                if (hit.transform.tag == "Terrain")
                {
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                    {
                        mPlayerState = CharacterState.RUNNING;
                    }
                    else
                    {
                        mPlayerState = CharacterState.IDLE;
                    }
                    break;
                }
            }

            yield return new WaitForSeconds(0.05f);

        }

        ChangeAnimator();
        yield return null;
    }

     public void ChangeAnimator()
    {
        RuntimeAnimatorController newAnimator = mIdleController;

        if (mPlayerState == CharacterState.RUNNING || mPlayerState == CharacterState.JUMPING)
        {
            newAnimator = mRunningController;
            if (_bIsGoingRight)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        gameObject.GetComponent<Animator>().runtimeAnimatorController = newAnimator;
    }
}

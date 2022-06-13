using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.SceneManagement;

public enum CharacterState
{
    IDLE,
    RUNNING,
    JUMPING,
    DEAD
}

public class PlayerController : MonoBehaviour
{
    public CharacterState mPlayerState = CharacterState.IDLE;

    [Header("Movement Settings")]
    public float mSpeed = 5.0f;
    public float mJumpStrength = 7f;

    [Header("Weaponry")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool hasGun1 = false;

    [Header("State Sprites")]
    public RuntimeAnimatorController mIdleController;
    public RuntimeAnimatorController mRunningController;
    public RuntimeAnimatorController mJumpingController;

    [Header("Score")]
    public TMP_Text coinScore;
    int totalCoins = 0;

    [SerializeField]
    public Sprite ogSprite;
    public Sprite newSprite;

    [SerializeField]
    public GameObject menuPanel;
    public GameObject restartPanel;


    private Animator _mAnimatorComponent;
    private bool _bIsGoingRight = true;
    private bool _bPlayerStateChanged = false;

    private bool _bInputsDisabled = false;

    private bool _bPlayerInvincible = false;

    private SpriteRenderer _mspriteRenderer;

    

    void Start()
    {
        Time.timeScale = 0f;
        menuPanel.SetActive(true);

        if(SceneManager.GetActiveScene().name == "bosslvl"){
            Time.timeScale = 1f;
        }
        
        _mAnimatorComponent = gameObject.GetComponent<Animator>();
        _mAnimatorComponent.runtimeAnimatorController = mIdleController;
        _mspriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    // Use state machine, much better
    void Update()
    {

       coinScore.SetText("Coins: " + totalCoins);
    

        if (!_bInputsDisabled)
        {
            if (Input.GetKeyDown(KeyCode.Space) && hasGun1) {
                Shoot();
            }   

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
                    ChangeSprite();
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
                    ChangeSprite();
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

            gameObject.GetComponent<SpriteRenderer>().flipX = !_bIsGoingRight;
            if (_bPlayerStateChanged)
            {
                ChangeAnimator();
            }
        }

        // Check if close to wall
        CheckWall();
    }


    void ChangeSprite()
    {
        _mspriteRenderer.sprite = newSprite; 
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

    IEnumerator CheckGrounded()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position - Vector3.up * 1f, -Vector2.up, 0.05f);
            if (hit.collider != null)
            {
                // if (hit.transform.tag == "Terrain")
                // {
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                    {
                        mPlayerState = CharacterState.RUNNING;
                    }
                    else
                    {
                        mPlayerState = CharacterState.IDLE;
                    }
                    break;
                //}
            }

            yield return new WaitForSeconds(0.05f);

        }
        _mspriteRenderer.sprite = ogSprite;
        ChangeAnimator();
        yield return null;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        
        
        if (other.transform.tag == "Monster" && !_bPlayerInvincible)
        {
            Vector3 heading = other.transform.position - transform.position;
            float magnitude = heading.magnitude;
            gameObject.GetComponent<Rigidbody2D>().velocity = -10f * heading / magnitude;
            StartCoroutine("Coroutine_BlockPlayerInputs");
            StartCoroutine("Coroutine_SetPlayerInvincible");

        }
    }

    IEnumerator Coroutine_BlockPlayerInputs()
    {
        _bInputsDisabled = true;
        yield return new WaitForSeconds(0.5f);
        _bInputsDisabled = false;
        yield return null;
    }

    IEnumerator Coroutine_SetPlayerInvincible()
    {
        _bPlayerInvincible = true;
        yield return new WaitForSeconds(1.5f);

        _bPlayerInvincible = false;
        yield return null;
    }

    public void playButton(){
        menuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void restartButton(){
        restartPanel.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void quitButton(){
        Debug.Log("Quit button pressed");
        Application.Quit();
    }

    public void BlockPlayerInputs()
    {
        _bInputsDisabled = true;
    }

    public void SetStatePlayerInvincible(bool newState)
    {
        _bPlayerInvincible = newState;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Slimeball"))
        {
            hasGun1 = true;
        }

        if(c2d.CompareTag("Coin")){
            totalCoins++;
        }

        if(c2d.CompareTag("Enemy")){
            restartPanel.SetActive(true);
            Time.timeScale = 0f;
            
        }

        if(c2d.CompareTag("LevelChanger")){
            SceneManager.LoadScene("bosslvl");

            
        }
    }

    void Shoot(){
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void CheckWall() {
        List<float> directions = new List<float>{-1, 1};

        for (int i = 0; i < directions.Count; i++) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.right * directions[i], transform.right * directions[i], 0.05f);
            if (hit.collider != null)
            {
                if (hit.transform.tag == "Terrain") {
                    transform.Translate(-1f * transform.right * directions[i] * 0.025f);
                }
            }
        }
            
    }

}
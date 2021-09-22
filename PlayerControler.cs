using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Animator playerAnimatror;
    private Health myHealth;
    private bool moveRight = false;
    private bool moveLeft = false;
    private bool blockAttack = false;
    private bool isAlive = true;
    private bool isJump = true;
    private bool isFlashing = false;
    private bool flash = false;
    private float currentManaAmount = 100f;
    private float manaAmount = 150f;
    private float jumpMagazine = 2;
    private float startFlashTime = 0.1f;
    private float flashSpeed = 5f;
    private float jumpForce = 17;
    private float flashTime;
    [SerializeField]
    private float extraGravity = -7;
    [SerializeField]
    private Rigidbody playerPhysic;

    public SkinnedMeshRenderer playerSkin;
    public GameObject flashEffect;
    public PlayerAttack playerAttack;
    public float movementSpeed;
    public Image manaImage;
    public Vector3 Dir;
    public bool isGrounded = true;


    // Start is called before the first frame update
    void Start()
    {
        flashTime = startFlashTime;
        currentManaAmount = manaAmount;
        playerAnimatror = GetComponent<Animator>();
        myHealth = GetComponent<Health>();
        movementSpeed = 8f;
        // playerAttack = gameObject.transform.Find("FirePoint (1)").GetComponent<PlayerAttack>();

    }

    // All the moving function must be checked before perfrome any kind of move(prevent from player to move while he did etc)
    void Update()
    {

        if (myHealth.isAlive)
        {

            {
              //  if (Input.GetKeyDown("a"))
                //    SetMoveLeft(true);
                if (moveRight) { }
                else if (moveLeft) { }
                else
                {
                    playerAnimatror.SetBool("Go", false);
                }

            }
            // If jump is true active jump function
            if (isJump)
            {
                Jump();
                isJump = false; //Prevent double jump in one click
            }

            Flash();
            flash = false;//NEED CHECK!!!! do it cus there are multi flash maybe because lags
        }
        else PlayerStopRun();





    }
    private void PlayerStopRun()
    {
        moveLeft = false;
        moveRight = false;
    }
    private void FixedUpdate()
    {

        if (moveRight)
            MoveRight();
        else if (moveLeft)
            MoveLeft();


    }
    private void MoveRight()
    {


        FlipCharacterSide("right");

        playerAnimatror.SetBool("Go", true);
        playerPhysic.velocity = new Vector3(movementSpeed, playerPhysic.velocity.y, 0);

        //When player jump you need move force to maintain the same movement speed as on the ground
        if (!isGrounded)
            playerPhysic.velocity = new Vector3(movementSpeed*2.5f, playerPhysic.velocity.y, 0);



    }

    private void MoveLeft()
    {
        FlipCharacterSide("left");

        playerAnimatror.SetBool("Go", true);
        playerPhysic.velocity = new Vector3(-movementSpeed, playerPhysic.velocity.y, 0);
        //When player is jumping more force need to maintain the same velocity as on the ground
        if(!isGrounded)
            playerPhysic.velocity = new Vector3(-movementSpeed*2.5f, playerPhysic.velocity.y, 0);
    }

    private void FlipCharacterSide(string side)
    {
        if (side == "right")
        {
            if (playerPhysic.transform.rotation.eulerAngles.y != 90)
            {
                //transform.localRotation = Quaternion.Euler(0, 90, 0);
                playerPhysic.transform.eulerAngles = new Vector3(0, 90, 0);

                //change to global/local rotation relative to unity north
            }
        }
        else if (side == "left")
        {
            if (playerPhysic.transform.rotation.eulerAngles.y != -90)
            {
                //  transform.localRotation = Quaternion.Euler(0, -90, 0);      //change to global/local rotation relative to unity north
                playerPhysic.transform.eulerAngles = new Vector3(0, -90, 0);

            }

        }

    }
    //Make the player jump. first jump higher then second
    private void Jump()
    {

        // "jumpMagzine" can't be more then 1 because when player jump the colider of the groun sill detect the ground reset jumpMagazine"

        if (jumpMagazine == 1 && isGrounded)
            {

            playerPhysic.velocity = new Vector3(playerPhysic.velocity.x, playerPhysic.velocity.y + jumpForce, 0);
            playerAnimatror.SetBool("Jump", true);         
            }
            else if (jumpMagazine == 1 && !isGrounded) //Double jump when player in the air
            {
            playerPhysic.velocity = new Vector3(playerPhysic.velocity.x, playerPhysic.velocity.y + jumpForce,0);
            playerAnimatror.SetBool("Jump", true);
            }

            jumpMagazine -= 1;
        
    }

    private void Flash()
    {
        if (flash)
        {
            if (currentManaAmount > 0)
            {
                currentManaAmount -= 30f;
                manaImage.fillAmount = currentManaAmount / manaAmount;

                isFlashing = true;
                Vector3 pos = new Vector3(0, 1.5f, 1);
                Instantiate(flashEffect, transform.position + pos, Quaternion.identity);
            }
        }
        if (isFlashing)
        {
            if (flashTime <= 0)
            {
                playerPhysic.velocity = Vector3.zero;
                flashTime = startFlashTime;
                isFlashing = false;
                playerSkin.enabled = true;
            }

            else if(moveRight)
            {
                playerPhysic.AddForce(Vector3.right*flashSpeed, ForceMode.Impulse);
                flashTime -= Time.deltaTime;
                playerSkin.enabled = false;
            }
            else if (moveLeft)
            {
                playerPhysic.AddForce(Vector3.left*flashSpeed, ForceMode.Impulse);
                //playerPhysic.velocity = Vector3.left * flashSpeed;
                playerSkin.enabled = false;
            }

            flashTime -= Time.deltaTime; //Cound down flash timer 
        }
        else if (!isFlashing)
        {
            if (isGrounded)
            {
                if (currentManaAmount < manaAmount)
                    currentManaAmount += 50 * Time.deltaTime;
                manaImage.fillAmount = currentManaAmount / manaAmount;

            }
        }

    }

    // Seter to move the character left only if there is no opposite move
    public void SetMoveLeft(bool is_move)
    {
        //User can't move while blocking on or moving to the conter direction
        if (!blockAttack && moveRight == false)
        moveLeft = is_move;
    }

    // Seter to move the character right only if there is no opposite move
    public void SetMoveRight(bool is_move)
    {
        //User can't move while blocking on or moving to the conter direction
        if (!blockAttack && moveLeft == false)
        moveRight = is_move;
    }

    //Set jump variable that make the player jump 
    public void SetJump(bool is_jump)
    {
        isJump = is_jump;   
    }
    public void SetBlockAttack(bool is_block)
    {
        
        blockAttack = is_block;
            myHealth.isBlock = is_block;
            playerAnimatror.SetBool("Block", is_block);
        //When player is blocking attack user can't move 
        if (is_block)
            PlayerStopRun();       
    }

    public void SetFlash(bool _isFlash)
    {
        flash = _isFlash;
    }


    //When player no tuch the ground movent speed reduce
    private void OnTriggerExit(Collider other)
    {
        playerAnimatror.SetBool("Jump", true);
        isGrounded = false;
        movementSpeed = 3.5f;
    }
    // When player hit the ground
    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
        jumpMagazine = 1;    // "jumpMagzine can't be more then 1 because when player jump the colider of the groun sill detect the ground reset jumpMagazine"
        movementSpeed = 11f;
        playerAnimatror.SetBool("Jump", false);
    }

}


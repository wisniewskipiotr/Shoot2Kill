using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{


    public float moveSpeed, gravityChanger, jump, runSpeed = 15f; 
    public CharacterController CharController;

    private Vector3 moveInput;

    public Transform camPlayer;

    public float sensitivity;

    private bool canJump;
    public Transform groundCheck;
    public LayerMask whatIsGround;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //First version of movemnet control

        //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        //adding gravity force
        float gravForce = moveInput.y;

        Vector3 verticalMove = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalMove = transform.right * Input.GetAxis("Horizontal");

        moveInput = verticalMove + horizontalMove;
        moveInput.Normalize();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveInput = moveInput * runSpeed;
        }else
        {
            moveInput = moveInput * moveSpeed;
        }
        

        moveInput.y = gravForce;
        moveInput.y += Physics.gravity.y * gravityChanger * Time.deltaTime;

        if(CharController.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityChanger * Time.deltaTime;
        }


        canJump = Physics.OverlapSphere(groundCheck.position, .25f, whatIsGround).Length > 0;

        //jumping
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            moveInput.y = jump;
        }

        CharController.Move(moveInput * Time.deltaTime);

        //camera rotation
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * sensitivity;


        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

        // same thing asd above but different method(and it also works:))

        camPlayer.rotation = Quaternion.Euler(camPlayer.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }
}

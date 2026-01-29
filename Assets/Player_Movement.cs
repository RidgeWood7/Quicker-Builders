using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float movespeed_L;
    public float movelength_L;
    public float movespeed_R;
    public float movelength_R;
    public float jumptime_J;
    public float jumpheight_J;
    public float Groundspeed_G;
    public float Gravityspeed_G;
    public bool Grounding_G;
    public float jumptimeleft_J;
    //dashing

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //myRigidbody.linearVelocityY = jumpspeed_J
    }

    // Update is called once per frame
    void Update()
    {
        //Gravity
        myRigidbody.linearVelocityY -= Gravityspeed_G * Time.deltaTime;
        Physics2D.gravity = new Vector2(0, 0);
        //Jumping
        /*if ((Input.GetKeyDown(KeyCode.UpArrow) == true || Input.GetKeyDown(KeyCode.W) == true) && (GetComponent<Detection>().Detection_D == true))
        {
            jumptimeleft_J = jumptime_J;
            GetComponent<Detection>().Detection_D = false;
        }*/
        if (jumptimeleft_J < 0)
        {
            jumptimeleft_J = 0;
        }
        if ((jumptime_J - jumptimeleft_J > 0.5) && ((GetComponent<Detection>().Detection_D == true) || (GetComponent<Detection>().Detection_U == true)))
        {
            jumptimeleft_J = 0;
        }
        if (jumptimeleft_J > 0)
        {
            jumptimeleft_J -= Time.deltaTime * 0.5f;
            transform.position += Vector3.up * 102 / 100 * jumptimeleft_J * Time.deltaTime * 2 * jumpheight_J / jumptime_J / jumptime_J;
            myRigidbody.linearVelocityY += Gravityspeed_G * Time.deltaTime;
            jumptimeleft_J -= Time.deltaTime * 0.5f;
        }
        //Grounding
        if ((Input.GetKeyDown(KeyCode.DownArrow) == true || Input.GetKeyDown(KeyCode.S) == true) && Grounding_G == true)
        {
            myRigidbody.linearVelocityY = -Groundspeed_G;
        }
        //Moving L
        if ((Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKeyDown(KeyCode.A) == true) && (GetComponent<Detection>().Detection_L == false))
        {
            myRigidbody.linearVelocityX = -movespeed_L;
        }
        //Moving R
        if ((Input.GetKey(KeyCode.RightArrow) == true || Input.GetKeyDown(KeyCode.D) == true) && (GetComponent<Detection>().Detection_R == false))
        {
            myRigidbody.linearVelocityX = movespeed_R;
        }


    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        if ((ctx.started) && (GetComponent<Detection>().Detection_D == true))
        {
            jumptimeleft_J = jumptime_J;
            GetComponent<Detection>().Detection_D = false;
        }
        else if (ctx.canceled)
        {
            
        }
    }
}

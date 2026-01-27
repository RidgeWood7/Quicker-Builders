using UnityEngine;

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
        // Physics2D.gravity = new Vector2(0, 0);
        //Jumping
        if ((Input.GetKeyDown(KeyCode.UpArrow) == true || Input.GetKeyDown(KeyCode.W) == true) && (GetComponent<Detection>().Detection_D == true))
        {
            jumptimeleft_J = jumptime_J;
            GetComponent<Detection>().Detection_D = false;
        }
        if (jumptimeleft_J < 0)
        {
            jumptimeleft_J = 0;
        }
        if (jumptimeleft_J > 0)
        {
            jumptimeleft_J -= Time.deltaTime * 1f;
            transform.position += Vector3.up * jumptimeleft_J * Time.deltaTime;
            
        }
        //Grounding
        if (Input.GetKeyDown(KeyCode.DownArrow) == true || Input.GetKeyDown(KeyCode.S) == true)
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
}

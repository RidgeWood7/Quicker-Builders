using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public bool isDead_D;

    public Rigidbody2D myRigidbody;
    public float movespeed_L;
    public float movelength_L;
    public bool Moving_L;
    public float movespeed_R;
    public float movelength_R;
    public bool Moving_R;
    public float jumptime_J;
    public float jumpheight_J;
    public float jumptimeleft_J;
    public bool Jumping_J;
    public float Groundspeed_G;
    public float Gravityspeed_G;
    public bool Grounding_G;
    public float Stuntime_SA;
    public float Stuntimeleft_SA;
    public bool Enable_Move_ME;
    public bool Enable_Jump_JE;
    public bool Enable_Ground_GE;
    public bool Enable_Stun_SAE;


    private float moveX;

    //dashing

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //myRigidbody.linearVelocityY = jumpspeed_J
    }

    // Update is called once per frame
    void Update()
    {
        //Death
        if (isDead_D == true)
        {
            Enable_Move_ME = false;
            Enable_Jump_JE = false;
            Enable_Ground_GE = false;
            Enable_Stun_SAE = false;
        }
        
        //Gravity
        myRigidbody.linearVelocityY -= Gravityspeed_G * Time.deltaTime;
        Physics2D.gravity = new Vector2(0, 0);
        //Jumping
        if ((Input.GetKeyDown(KeyCode.UpArrow) == true || Input.GetKeyDown(KeyCode.W) == true) && (GetComponent<Detection>().Detection_D == true) && (Jumping_J == true) && (Enable_Jump_JE == true))
        {
            jumptimeleft_J = jumptime_J;
            GetComponent<Detection>().Detection_D = false;
        }
        if ((jumptimeleft_J < 0) || (Enable_Jump_JE == false))
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
        if ((Input.GetKeyDown(KeyCode.DownArrow) == true || Input.GetKeyDown(KeyCode.S) == true) && Grounding_G == true && Enable_Ground_GE == true)
        {
            myRigidbody.linearVelocityY = -Groundspeed_G;
        }
        //Moving L
        if ((Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true) && (GetComponent<Detection>().Detection_L == false) && (Moving_L == true) && (Enable_Move_ME == true))
        {
            myRigidbody.linearVelocityX = -movespeed_L;
        }
        //Moving R
        if ((Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true) && (GetComponent<Detection>().Detection_R == false) && (Moving_R == true) && (Enable_Move_ME == true))
        {
            myRigidbody.linearVelocityX = movespeed_R;
        }
        //Moving M
        if ((Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.A) == false) && (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.D) == false) && (Moving_L == true) && (Moving_R == true))
        {
            myRigidbody.linearVelocityX = 0;
        }

        myRigidbody.linearVelocityX = moveX;

        //Stun
        if (Enable_Stun_SAE == false)
        {
            GetComponent<Stun>().Ability_Stun_SA = false;
        }
        if (Enable_Stun_SAE == true)
        {
            GetComponent<Stun>().Ability_Stun_SA = true;
        }
        //Stun Timer
        if (Stuntimeleft_SA > -1)
        {
            Stuntimeleft_SA -= Time.deltaTime;
        }
        if (Stuntimeleft_SA < 1 && Stuntimeleft_SA > -1)
        {
            Enable_Move_ME = true;
            Enable_Jump_JE = true;
            Enable_Ground_GE = true;
            Enable_Stun_SAE = true;
        }
        if (Stuntimeleft_SA < -2)
        {
            Stuntimeleft_SA = -2;
        }
        if (Stuntimeleft_SA < 0 && Stuntimeleft_SA > -2)
        {
            Stuntimeleft_SA = 0;
        }
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        if ((ctx.started) && (GetComponent<Detection>().Detection_D == true) && (Jumping_J == false) && (Enable_Jump_JE == true))
        {
            jumptimeleft_J = jumptime_J;
            GetComponent<Detection>().Detection_D = false;
        }
        else if ((ctx.canceled) && (Jumping_J == false))
        {
            jumptimeleft_J = 0;
        }
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        if ((GetComponent<Detection>().Detection_R == false) && (ctx.ReadValue<Vector2>().x > 0) && (Moving_L == false) && (Moving_R == false) && (Enable_Move_ME == true))
        {
            moveX = ctx.ReadValue<Vector2>().x * movespeed_L;
        }
        if ((GetComponent<Detection>().Detection_L == false) && (ctx.ReadValue<Vector2>().x < 0) && (Moving_L == false) && (Moving_R == false) && (Enable_Move_ME == true))
        {
            moveX = ctx.ReadValue<Vector2>().x * movespeed_R;
        }
        if ((ctx.ReadValue<Vector2>().x == 0) && (Moving_L == false) && (Moving_R == false))
        {
            moveX = ctx.ReadValue<Vector2>().x;
        }
    }
    public void Placing(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("Placing");
        }
        else if (ctx.canceled)
        {
            Debug.Log("Placing");
        }
    }
    public void Stun(InputAction.CallbackContext ctx)
    {
        if (ctx.started && (GetComponent<Stun>().Ability_Stun_SA == true) && Enable_Stun_SAE == true)
        {

            var hit=Physics2D.CircleCastAll(transform.position, GetComponent<Stun>().Ability_Stunsize_SA, Vector2.zero);

            foreach(var h in hit)
            {
                if (h.collider.gameObject != gameObject && h.collider.gameObject.GetComponent<Stun>() != null && h.collider.gameObject.GetComponent<Player_Movement>() != null && (h.collider.gameObject.GetComponent<Player_Movement>().Stuntimeleft_SA < 1 && h.collider.gameObject.GetComponent<Player_Movement>().Stuntimeleft_SA > -1))
                {
                    h.collider.gameObject.GetComponent<Stun>().isStun_Opponent = true;
                    h.collider.gameObject.GetComponent<Player_Movement>().Stuntimeleft_SA = Stuntime_SA + 1;
                    h.collider.gameObject.GetComponent<Player_Movement>().Enable_Move_ME = false;
                    h.collider.gameObject.GetComponent<Player_Movement>().Enable_Jump_JE = false;
                    h.collider.gameObject.GetComponent<Player_Movement>().Enable_Ground_GE = false;
                    h.collider.gameObject.GetComponent<Player_Movement>().Enable_Stun_SAE = false;
                }
            }
        }
        else if (ctx.canceled)
        {
            Debug.Log("Stun");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, GetComponent<Stun>().Ability_Stunsize_SA);
    }
}
//Gonna be for stunning opponents and detecting them to do so.

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tile") && gameObject.name == "Detect L")
        {
            Detection_L = true;
        }
    }*/

using UnityEngine;

public class Detection : MonoBehaviour
{
    public bool isParent;

    public bool Detection_L = false;
    public bool Detection_R = false;
    public bool Detection_U = false;
    public bool Detection_D = false;

    private void Update()
    {
        if (isParent)
        {
            // Find each of the children, get their detection scripts, and apply them to the parent (only if this object is the parent).
            Detection_L = transform.Find("Detect L").GetComponent<Detection>().Detection_L;
            Detection_R = transform.Find("Detect R").GetComponent<Detection>().Detection_R;
            Detection_U = transform.Find("Detect U").GetComponent<Detection>().Detection_U;
            Detection_D = transform.Find("Detect D").GetComponent<Detection>().Detection_D;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tile") && gameObject.name == "Detect L")
        {
            Detection_L = true;
        }
        if (collision.gameObject.CompareTag("Tile") && gameObject.name == "Detect R")
        {
            Detection_R = true;
        }
        if (collision.gameObject.CompareTag("Tile") && gameObject.name == "Detect U")
        {
            Detection_U = true;
        }
        if (collision.gameObject.CompareTag("Tile") && gameObject.name == "Detect D")
        {
            Detection_D = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.name == "Detect L")
        {
            Detection_L = false;
        }
        if (gameObject.name == "Detect R")
        {
            Detection_R = false;
        }
        if (gameObject.name == "Detect U")
        {
            Detection_U = false;
        }
        if (gameObject.name == "Detect D")
        {
            Detection_D = false;
        }
    }
}

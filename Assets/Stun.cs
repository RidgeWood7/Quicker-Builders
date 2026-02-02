using UnityEngine;

public class Stun : MonoBehaviour
{
    public float Ability_Stunsize_SA;
    public bool Ability_Stun_SA;
    public bool isStun; // Put this on the child.
    private void Update()
    {
        //Stun Ability
        if (isStun)
        {
            Ability_Stunsize_SA = transform.parent.GetComponent<Stun>().Ability_Stunsize_SA;
            Ability_Stun_SA = transform.parent.GetComponent<Stun>().Ability_Stun_SA;
        }
        if (gameObject.name == "Ability Stun")
        {
            transform.localScale = Vector3.one * (2 * Ability_Stunsize_SA + 1);
        }
    }
}

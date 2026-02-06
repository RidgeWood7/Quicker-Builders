using UnityEngine;

[ExecuteAlways]
public class Stun : MonoBehaviour
{
    public float Ability_Stunsize_SA;
    public bool Ability_Stun_SA;
    public bool isStunchild; // Put this on the child.
    public bool isStun_Me;
    public bool isStun_Opponent;
    private void Update()
    {
        //Stun Ability
        if (isStunchild)
        {
            Ability_Stunsize_SA = transform.parent.GetComponent<Stun>().Ability_Stunsize_SA;
            Ability_Stun_SA = transform.parent.GetComponent<Stun>().Ability_Stun_SA;
            isStun_Opponent = transform.parent.GetComponent<Stun>().isStun_Opponent;
        }
        if (gameObject.name == "Ability Stun")
        {
            transform.localScale = Vector3.one * (2 * Ability_Stunsize_SA + 1);
        }
        isStun_Me = isStun_Opponent;
    }
}

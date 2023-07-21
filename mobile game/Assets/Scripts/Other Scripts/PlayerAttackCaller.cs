using UnityEngine;

//used for animation events of attack
public class PlayerAttackCaller : MonoBehaviour
{
    public Attack Player;

    void Attack()
    {
        Player.attack();
    }
}

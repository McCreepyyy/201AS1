using UnityEngine;

public class Combat : MonoBehaviour
{
    public int damage = 10;

    public void Attack()
    {
        // This method represents the attack action
        // Here, you can add any attack logic you want, such as dealing damage to the enemy
        Debug.Log(gameObject.name + " performs an attack.");
    }
}
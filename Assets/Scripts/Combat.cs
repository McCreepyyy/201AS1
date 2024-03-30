using UnityEngine;

public class Combat : MonoBehaviour
{
    public int damage = 10;

    public void Attack()
    {
        Debug.Log(gameObject.name + " performs an attack.");
    }
}
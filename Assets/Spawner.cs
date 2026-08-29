using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class Spawner : Health
{
    public GameObject Enemy;
    public Transform placement;
    public float speed = 7f;
    IEnumerator Start()
    {
        while (true)
        {

            Instantiate(Enemy,placement.position,Quaternion.identity);
            yield return new WaitForSeconds(speed);
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}

using DefaultNamespace;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int maxbounces = 3;
    private int bouncesLeft = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bouncesLeft = maxbounces;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.GetComponent<Health>() != null)
        {
            other.transform.GetComponent<Health>().TakeDamage(10f);
            Destroy(gameObject);
        }
        bouncesLeft--;
        if (bouncesLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
}

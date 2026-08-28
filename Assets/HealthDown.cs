using UnityEngine;

public class HealthDown : MonoBehaviour
{
    public GameObject Enemy;
    public Transform placement;
    public bool hasshown = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On Trigger Enter : " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && !hasshown)
        {
            hasshown = true;
            Debug.Log("Health Down");
            var p = collision.transform.parent.GetComponent<Player>();
            p.maxHealth = 100f;
            p.currentHealth = 100f;
            p.ValidateHealth();
            p.OnHealthChanged();
            if (p.skiptutorial )
            {
             return;
            }
            Time.timeScale = 0.05f;
            p.Prompt("Now try to defend yourself!\n" +
                     "You can only take so many hits!");
            Instantiate(Enemy, placement.position, Quaternion.identity);
        }
    }
}

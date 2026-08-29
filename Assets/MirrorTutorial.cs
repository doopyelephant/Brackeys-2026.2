using UnityEngine;

public class MirrorTutorial : MonoBehaviour
{
    public GameObject Mirror;
    public Transform placement;
    public bool hasshown = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("On Trigger Enter : " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && !hasshown)
        {
            hasshown = true;
            Debug.Log("Mirror Tutorial");
            var p = collision.transform.parent.GetComponent<Player>();
            if (p.skiptutorial )
            {
                return;
            }
            Time.timeScale = 0.05f;
            p.Prompt("This is a mirror!\n\n" +
                     "Your bullets will also home towards your reflection in the mirror if they see you in the mirror.\n\n" +
                     "Try it!");
            Instantiate(Mirror, placement.position, Quaternion.identity);
        }
    }
}
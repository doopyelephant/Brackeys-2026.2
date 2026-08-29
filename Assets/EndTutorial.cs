using DefaultNamespace;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{

    public bool hasshown = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasshown)
        {
            hasshown = true;
            var p = collision.transform.parent.GetComponent<Player>();
            if (p.skiptutorial )
            {
                return;
            }
            Time.timeScale = 0.05f;
            p.Prompt(".... You are Ready! ....\n\n" +
                     "BREAK OUT!", () => {
                p.skiptutorial = true;
                p.hascompletedtutorial = true;
                GlobalLevelManager.LoadLevel(0);
                return true;});
        }
    }
}

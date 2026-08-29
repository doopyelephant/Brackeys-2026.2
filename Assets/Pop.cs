using System;
using System.Collections;
using UnityEngine;

public class Pop : MonoBehaviour
{
    public float time;
    private float maxtime;
    private SpriteRenderer spriteRenderer;
    private IEnumerator Start()
    {
        maxtime = time;
        spriteRenderer = GetComponent<SpriteRenderer>();
        while (time > 0)
        {
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,spriteRenderer.color.b, time / maxtime);
        }
        Destroy(gameObject);
    }
}

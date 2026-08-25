using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShootGun()
    {
        StartCoroutine(ShootAsync());
    }

    IEnumerator ShootAsync()
    {
        spriteRenderer.sprite = s2;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.sprite = s3;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = s1;
    }
}

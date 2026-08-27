using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Enemy : Health
{
    private SpriteRenderer spriteRenderer;

    public Sprite idlesprite;
    public Sprite movingsprite;
    public Sprite walkcyclesprite;
    private float targety = 1f;
    public float idley = 1.3f;

    public float maxy = 1.3f;
    public float miny = 0.9f;

    public float speed = 10f;

    public float bobspeed = 2f;
    public float stride = 5f;
    private bool walkcycle = false;
    private float dis = 0f;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector3 prevmov = Vector3.zero;
    float lerpspeed = 10f;
    private bool shooting = false;
    private SpriteRenderer gunsprite;
    public Shoot sht;
    public GameObject Bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunsprite = sht.GetComponent<SpriteRenderer>();
        gunsprite.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var movement = Vector3.MoveTowards(prevmov,GetMovement(),lerpspeed * Time.deltaTime);
        var moving = movement.magnitude > 0.1f;
        if (moving)
        {
            spriteRenderer.sprite = movingsprite;
        }
        else
        {
            spriteRenderer.sprite = idlesprite;
        }
        var c = spriteRenderer.transform.localScale.y;
        if (moving)
        {
            if (Mathf.Abs(targety - c) < 0.05f)
            {
                if (targety - miny == 0)
                {
                    targety = maxy;
                }
                else
                {
                    targety = miny;
                }
            }
        }
        else
        {
            targety = idley;
        }
        if (movement.x != 0)
        {
            if (movement.x > 0f)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        if (dis > stride)
        {
            if (walkcycle)
            {
                spriteRenderer.sprite = movingsprite;
            }
            else
            {
                spriteRenderer.sprite = walkcyclesprite;
            }
            walkcycle = !walkcycle;
            dis = 0f;
        }
        spriteRenderer.transform.localScale = new Vector3(idley, Mathf.Lerp(c,targety,Time.deltaTime * bobspeed), idley);
        transform.Translate(movement * Time.deltaTime);
        dis += movement.magnitude * Time.deltaTime;
        prevmov = movement;
    }

    private Vector3 GetMovement()
    {
        if (Random.value < 0.5f * Time.deltaTime)
        {
            StartCoroutine(ShootGun());
        }
        if (shooting)
        {
            return Vector3.zero;
        }
        if (Vector3.Distance(player.transform.position, transform.position) > 5)
        {
            return (player.transform.position - transform.position).normalized * speed;
        }
        else
        {
            return Vector3.Cross(Vector3.forward, player.transform.position - transform.position).normalized * speed;
        }

    }

    IEnumerator ShootGun()
    {
        gunsprite.transform.position = transform.position + (player.transform.position - transform.position).normalized;
        gunsprite.enabled = true;
        if (player.transform.position.x > transform.position.x)
        {
            gunsprite.flipX = false;
        }
        else
        {
            gunsprite.flipX = true;
        }
        shooting = true;
        sht.ShootGun();
        var tmp = Instantiate(Bullet, gunsprite.transform.position + (player.transform.position - transform.position).normalized, Quaternion.identity);
        tmp.GetComponent<Rigidbody2D>().linearVelocity = (player.transform.position - transform.position).normalized * 10f;
        yield return new WaitForSeconds(1.5f);
        shooting = false;
        gunsprite.enabled = false;
    }
    public override void DamageEffect(float fraction)
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.red, fraction * 2f);
        StartCoroutine(ResetColor(0.2f));
    }

    public override void HealEffect(float fraction)
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.green, fraction * 2f);
        StartCoroutine(ResetColor(0.2f));
    }
    IEnumerator ResetColor(float time)
    {
        yield return new WaitForSeconds(time);
        spriteRenderer.color = Color.white;
    }

    public override void Die()
    {
     Destroy(gameObject);
    }
}

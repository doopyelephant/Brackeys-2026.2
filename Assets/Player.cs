using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class Player : Health
{
    SpriteRenderer spriteRenderer;
    bool moving = false;

    private float targety = 1f;

    public float maxy = 1.3f;
    public float miny = 0.9f;

    public float speed = 10f;

    public float bobspeed = 2f;
    private float dis = 0f;
    public bool gunequipped = true;
    private Vector3 prevpos;
    private GameObject Gun;
    private Shoot sht;
    public Sprite char1;
    public Sprite char2;
    public GameObject Bullet;
    private Rigidbody2D rb;

    public float stride = 5f;
    private bool walkcycle = false;
    public float cooldown = 0.5f;
    private bool canShoot = true;

    public GameObject DiedMenu;
    private AudioSource audioSource;

    private AudioSource Gunaudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        targety = miny;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = Vector2.zero;
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");
        dis += new Vector2(hor, ver).magnitude;
        if (hor != 0)
        {
            if (hor > 0f)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        if (dis > stride)
        {
            if (walkcycle)
            {
                spriteRenderer.sprite = char1;
            }
            else
            {
                spriteRenderer.sprite = char2;
            }
            walkcycle = !walkcycle;
            dis = 0f;
        }
        transform.Translate(new Vector3(hor, ver, 0) * (Time.deltaTime * speed));
       // transform.Translate();
        moving = hor != 0 || ver != 0;
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
            targety = 2f;
        }

        if (!moving)
        {
            audioSource.Stop();
            audioSource.time = 0;
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        if (Gun != null)
        {
            var mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            Gun.transform.position = transform.position + Vector3.Normalize(mouse - transform.position) * 1;
            Gun.transform.rotation = Quaternion.LookRotation(mouse - transform.position) * Quaternion.Euler(0, 90, 0);
            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                Gunaudio.Play();
                canShoot = false;
                StartCoroutine(Cooldown());
                sht.ShootGun();
                var bulletobj = Instantiate(Bullet, Gun.transform.position + (mouse - transform.position).normalized * 1f, Quaternion.LookRotation((Gun.transform.position - transform.position).normalized) * Quaternion.Euler(0, 90,0));
                var bullet = bulletobj.GetComponent<Bullet>();
                bullet.target = (mouse - transform.position).normalized * 500f;
                bulletobj.GetComponent<Rigidbody2D>().linearVelocity = (transform.position - prevpos)/Time.deltaTime;
                bullet.Init();
            }
        }
        else
        {
            if (gunequipped)
            {
                Gun = GameObject.Find("Gun");
                sht = Gun.GetComponent<Shoot>();
                Gunaudio = Gun.GetComponent<AudioSource>();
            }
        }


        spriteRenderer.transform.localScale = new Vector3(2, Mathf.Lerp(c,targety,Time.deltaTime * bobspeed), 2);
        prevpos = transform.position;
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

    public override void Die()
    {
    spriteRenderer.color = Color.red;
    Camera.main.backgroundColor = Color.crimson;
    Time.timeScale = 0.05f;
    Instantiate(DiedMenu);
    this.enabled = false;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

    IEnumerator ResetColor(float time)
    {
        yield return new WaitForSeconds(time);
        spriteRenderer.color = Color.white;
    }
}

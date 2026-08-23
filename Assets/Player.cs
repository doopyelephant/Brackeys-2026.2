using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool moving = false;

    private float targety = 1f;

    public float maxy = 1.3f;
    public float miny = 0.9f;

    public float speed = 10f;

    public float bobspeed = 2f;
    public bool gunequipped = true;
    private Vector3 prevpos;
    private GameObject Gun;

    public GameObject Bullet;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        targety = miny;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = Vector2.zero;
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(hor, ver, 0) * (Time.deltaTime * speed));
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
            targety = 1.1f;
        }

        if (Gun != null)
        {
            var mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Gun.transform.position = transform.position + Vector3.Normalize(mouse - transform.position) * 2;
            Gun.transform.rotation = Quaternion.LookRotation(mouse - transform.position) * Quaternion.Euler(0, 90, 0);
            if (Input.GetMouseButtonDown(0))
            {
                var bulletobj = Instantiate(Bullet, Gun.transform.position, Quaternion.LookRotation(mouse - transform.position));
                var bullet = bulletobj.GetComponent<Bullet>();
                bullet.target = mouse;
                bulletobj.GetComponent<Rigidbody2D>().linearVelocity = (transform.position - prevpos)/Time.deltaTime;
                bullet.Init();
            }
        }
        else
        {
            if (gunequipped)
            {
                Gun = GameObject.Find("Gun");
            }
        }


        spriteRenderer.transform.localScale = new Vector3(1, Mathf.Lerp(c,targety,Time.deltaTime * bobspeed), 1);
        prevpos = transform.position;
    }
}

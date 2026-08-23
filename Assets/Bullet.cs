using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 target;
    private Rigidbody2D rb;
    public float speed = 10f;
    public Camera cam = null;
    private PolygonCollider2D poly = null;
    private ContactFilter2D filter = new ContactFilter2D();
    public GameObject trig;
    public float lockspeed = 10f;

    public void Init()
    {
        filter.layerMask = LayerMask.GetMask("Player");
        filter.useLayerMask = true;
        poly = GetComponentInChildren<PolygonCollider2D>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = (Vector2)rb.linearVelocity + (Vector2)(target - transform.position).normalized * speed;
    }

    public void Update()
    {
        trig.transform.rotation = Quaternion.LookRotation(rb.linearVelocity.normalized, Vector3.forward) *
                                  Quaternion.Euler(90, 90, 90);
        if ((transform.position - cam.transform.position).magnitude > 100f || rb.linearVelocity.magnitude < 0.1f)
        {
            Destroy(gameObject);
        }

        Collider2D[] colliders = new Collider2D[1];
        if (poly.Overlap(filter, colliders) != 0)
        {
            Debug.Log("hit");
            rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity,
                (colliders[0].transform.position - transform.position).normalized * speed, lockspeed * Time.deltaTime);
        }

    }
}



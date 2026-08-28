using System.Linq;
using DefaultNamespace;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class Extensions
{
    public static float ClosestPoint(this LineRenderer line,Transform transform)
    {
        float closest = Mathf.Infinity;
        Vector3[] points = new Vector3[line.positionCount];
        line.GetPositions(points);
        closest = Mathf.Min(closest, Vector3.Distance(transform.position, PointOnLine(points[0], points[1],transform.position)));
        closest = Mathf.Min(closest, Vector3.Distance(transform.position, PointOnLine(points[1], points[2],transform.position)));
        return closest;
    }
    public static Vector2 PointOnLine(Vector2 start, Vector2 end,Vector2 point)
    {
      Vector2 dir = end - start;
      float lensq = dir.sqrMagnitude;
      if (lensq == 0) return start;
      Vector2 v = point - start;
      float t = Vector2.Dot(v, dir) / lensq;
      t = Mathf.Clamp01(t);
      return start + dir * t;
    }
}
public class Bullet : MonoBehaviour
{
    public int bounces = 3;
    private int bouncesLeft = 3;
    public Vector3 target;
    private Rigidbody2D rb;
    public float speed = 10f;
    public Camera cam = null;
    private PolygonCollider2D poly = null;
    private ContactFilter2D filter = new ContactFilter2D();
   // private ContactFilter2D filter2 = new ContactFilter2D();
    public GameObject trig;
    public float lockspeed = 10f;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box;

    public void Init()
    {
        box = GetComponent<BoxCollider2D>();
        bouncesLeft = bounces;
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        filter.layerMask = LayerMask.GetMask("Player","MirrorBox");
        filter.useLayerMask = true;
       /* filter2.layerMask = LayerMask.GetMask("Mirror");
        filter2.useLayerMask = true;*/
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

        Collider2D[] colliders = new Collider2D[10];
      //  Collider2D[] colliders2 = new Collider2D[1];
        var got = poly.Overlap(filter, colliders);
        colliders = colliders.OrderBy(c =>
        {
            if (c == null)
            {
                return float.MaxValue;
            }
            return Vector3.Distance(c.ClosestPoint(transform.position), transform.position);
        }).ToArray();

        if (got != 0 && ((colliders[0].transform.parent.GetComponent<Mirror>() == null) || /*box.Overlap(filter2, colliders2) != 0*/ Physics2D.OverlapPoint(transform.position,LayerMask.GetMask("Mirror")) != null))
        {

            var color = Vector3.MoveTowards(new Vector3(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b),Vector3.right,100f * Time.deltaTime);
            spriteRenderer.color = new Color(color.x,color.y,color.z);
            rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity,
                (colliders[0].transform.position - transform.position).normalized * speed, lockspeed * Time.deltaTime * (colliders[0].transform.parent.GetComponent<Mirror>() == null ? 1f : 2f));
        }
        else
        {
            var color = Vector3.MoveTowards(new Vector3(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b),Vector3.one,100f * Time.deltaTime);
            spriteRenderer.color = new Color(color.x,color.y,color.z);
        }

    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(20f);
            Destroy(gameObject);
        }
        else
        {
            bouncesLeft--;
            if (bouncesLeft <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}



using System;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class Mirror : MonoBehaviour
{
    private GameObject player;
    private LineRenderer lr;
    public SpriteRenderer ref1;
    public SpriteRenderer ref2;
    private PolygonCollider2D poly1;
    private PolygonCollider2D poly2;
    private ushort [] verts = new ushort []{0,1,2,1,2,3};
    private BoxCollider2D box;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        box = transform.GetChild(0).GetComponent<BoxCollider2D>();
        poly1 = ref1.GetComponent<PolygonCollider2D>();
        poly2 = ref2.GetComponent<PolygonCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        /*lr = GetComponent<LineRenderer>();
        lr.positionCount = 3;
        lr.widthMultiplier = box.size.x;*/
    }

    // Update is called once per frame
    void Update()
    {
        var norm = transform.rotation * Vector3.up;
        if (Vector3.Dot(norm, (player.transform.position - transform.position).normalized) < 0)
        {
            norm = -norm;
        }

        if (Vector3.Distance(player.transform.position, transform.position) > 100)
        {
            ref1.enabled = false;
            ref2.enabled = false;
            return;
        }

        if (!Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, 100,LayerMask.GetMask("Player","Default")).collider.gameObject.CompareTag("Player"))
        {
ref1.enabled = false;
ref2.enabled = false;
poly1.enabled = false;
poly2.enabled = false;
return;
        }
        ref1.enabled = true;
        ref2.enabled = true;
        poly1.enabled = true;
        poly2.enabled = true;
        var sprite = ref1.sprite;
        sprite.SetVertexCount(4);
        var sprite2 = ref2.sprite;
        sprite2.SetVertexCount(4);
        Vector2[] ref1verts = new Vector2[4];
        Vector2[] ref2verts = new Vector2[4];
        ref1verts = sprite.vertices;
        ref2verts = sprite2.vertices;
        ref1verts[0] = new Vector3(128,128) + Vector3.Cross(Vector3.forward, norm) * (box.size.x * 0.5f);
        ref1verts[1] = new Vector3(128,128) + Vector3.Cross(norm, Vector3.forward) * (box.size.x * 0.5f);
        ref1verts[2] = new Vector3(128,128) + (player.transform.position - transform.position) + Vector3.Cross(Vector3.forward, norm) * (box.size.x * 0.5f);
        ref1verts[3] = new Vector3(128,128) + (player.transform.position - transform.position) + Vector3.Cross(norm, Vector3.forward) * (box.size.x * 0.5f);
        ref2verts[0] = new Vector3(128,128) + Vector3.Cross(Vector3.forward, norm) * (box.size.x * 0.5f);
        ref2verts[1] = new Vector3(128,128) + Vector3.Cross(norm, Vector3.forward) * (box.size.x * 0.5f);
        ref2verts[2] = new Vector3(128,128) + Vector3.Reflect((transform.position - player.transform.position).normalized, norm).normalized * Physics2D.Raycast(transform.position,Vector3.Reflect((transform.position - player.transform.position).normalized, norm).normalized,100,LayerMask.GetMask("Player","Default")).distance + Vector3.Cross(Physics2D.Raycast(transform.position,Vector3.Reflect((transform.position - player.transform.position).normalized, norm).normalized,100,LayerMask.GetMask("Player","Default")).normal,Vector3.forward) * (box.size.x * 0.5f);
        ref2verts[3] = new Vector3(128,128) + Vector3.Reflect((transform.position - player.transform.position).normalized, norm).normalized * Physics2D.Raycast(transform.position,Vector3.Reflect((transform.position - player.transform.position).normalized, norm).normalized,100,LayerMask.GetMask("Player","Default")).distance + Vector3.Cross(Vector3.forward, Physics2D.Raycast(transform.position,Vector3.Reflect((transform.position - player.transform.position).normalized, norm).normalized,100f,LayerMask.GetMask("Player","Default")).normal) * (box.size.x * 0.5f);
        /*Debug.Log(sprite.rect.width + " " + sprite.rect.height + " " + sprite.rect.x + " " + sprite.rect.y);
        Debug.Log(sprite2.rect.width + " " + sprite2.rect.height + " " + sprite2.rect.x + " " + sprite2.rect.y);
        Debug.Log(ref1verts[0] + " " + ref1verts[1] + " " + ref1verts[2]);
        Debug.Log(ref2verts[0] + " " + ref2verts[1] + " " + ref2verts[2]);*/
        sprite.OverrideGeometry(ref1verts, verts);
        sprite2.OverrideGeometry(ref2verts, verts);
        ref1.sprite = sprite;
        ref2.sprite = sprite2;
        var scaler = new Vector3(1f/(transform.localScale.x/sprite.rect.width),1f/(transform.localScale.y/sprite.rect.width),0);
        ref1.transform.localScale = scaler;
        ref2.transform.localScale = scaler;
        poly1.SetPath(0,ref1verts.Select(v => new Vector2((v.x - 128f)/256f,(v.y - 128f)/256f)).OrderByDescending(v => v.y).ThenBy(v => Mathf.Abs(v.y) == 0f ? v.x : -v.x).ToArray());
        poly2.SetPath(0, ref2verts.Select(v => new Vector2((v.x - 128f)/256f,(v.y - 128f)/256f)).OrderByDescending(v => v.y).ThenBy(v => Mathf.Abs(v.y) == 0f ? v.x : -v.x).ToArray());


        /*ref1.transform.localScale = new Vector3(10,10,10);
        ref2.transform.localScale = new Vector3(10,10,10);*/
        /* lr.SetPosition(1, transform.position + norm * (transform.localScale.x/2));
     lr.SetPosition(0, player.transform.position);
     lr.SetPosition(2, /
     */

    }

}

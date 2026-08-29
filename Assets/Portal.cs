using System;
using DefaultNamespace;
using UnityEngine;

public class Portal : MonoBehaviour
{
    float speed = 10f;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalLevelManager.LoadLevel(GlobalLevelManager.currentlevel + 1);
        }
    }

    public void Update()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0f, 0f, speed * Time.deltaTime);
    }
}

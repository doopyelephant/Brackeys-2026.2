using DefaultNamespace;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Health health;
    float maxsize = 1f;
    Vector3 startposition;

    public void Start()
    {
        startposition = transform.localPosition;
        maxsize = transform.localScale.x;
        health.healthBar = this;
    }
    public void Changed(float health)
    {
        transform.localPosition = startposition + maxsize * Vector3.left * 0.5f * (1 - health);
        transform.localScale = new Vector3(maxsize * health, 1, 1);
    }
}

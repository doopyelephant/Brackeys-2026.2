using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x,player.transform.position.y,-10), 0.5f * Time.deltaTime * Mathf.Abs((transform.position - player.transform.position).magnitude));
    }
}

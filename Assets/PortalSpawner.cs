using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class PortalSpawner : MonoBehaviour
    {
        public GameObject Portal;
        public Transform placement;
        public void Start()
        {
            StartCoroutine(CheckForEnemies());
        }
        IEnumerator CheckForEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                Instantiate(Portal,placement.position, Quaternion.identity);
                break;
                }
            }
        }
    }
}
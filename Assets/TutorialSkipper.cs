using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class TutorialSkipper : MonoBehaviour
    {
        public void Update()
        {
            if (!GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<Player>().skiptutorial)
            {
                GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<Player>().skiptutorial = true;
                Destroy(gameObject);
            }
        }
    }
}
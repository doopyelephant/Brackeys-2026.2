using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
    public class LevelSwitcher : MonoBehaviour
    {
        public List<GameObject> PersistentObjects;
        public List<int> Levels;

        public void Awake()
        {
            GlobalLevelManager.Init(this);
        }

        public void LoadLevel(int level)
        {
           StartCoroutine(LoadLevelAsync(level));
        }

        IEnumerator LoadLevelAsync(int level)
        {
            Persist();
            SceneManager.LoadScene(Levels[level]);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            Debug.Log("loaded level " + SceneManager.GetActiveScene().name);
            Integrate(SceneManager.GetActiveScene());
        }

        private void Persist()
        {
            foreach (var obj in PersistentObjects.Append(gameObject))
            {
                if (obj.name == "Player")
                {
                    obj.GetComponent<Player>().enabled = true;
                    obj.GetComponent<Player>().canShoot = true;
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                    obj.GetComponent<Health>().Heal(1000f);
                    obj.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                    obj.GetComponent<Rigidbody2D>().freezeRotation = true;
                    obj.transform.rotation = Quaternion.identity;
                }
                obj.transform.position = new Vector3(0, 0, obj.transform.position.z);
                DontDestroyOnLoad(obj);
            }
        }

        private void Integrate(Scene scene)
        {
            foreach (var obj in PersistentObjects.Append(gameObject))
            {
             SceneManager.MoveGameObjectToScene(obj, scene);
            }
        }

    }

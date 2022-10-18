using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolingSystem
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        [SerializeField] int poolSize = 4;
        [SerializeField] float minSpawnTimer = 2f;
        [SerializeField] float MaxSpawnTimer = 8f;

        //List<GameObject> pool;
        //int lastInstanceIndex = 0;
        GameObject[] pool;
        //Random random = System.Random();

        void Awake()
        {
            PopulatePool();
        }

        void Start() 
        {
            StartCoroutine(SpawnObject());    
        }

        private void PopulatePool()
        {
            //pool = new List<GameObject>();
            pool = new GameObject[poolSize];
            for (int i = 0; i < pool.Length; i++)
            {
                // var instance = Instantiate(prefab, transform);
                // instance.SetActive(false);

                // pool.Add(instance);
                pool[i] = Instantiate(prefab, this.transform);
                pool[i].SetActive(false);
            }
        }

        void EnableObjectInPool()
        {
            for(int i = 0; i < pool.Length; i++)
            {
                if(pool[i].activeInHierarchy == false)
                {
                    pool[i].SetActive(true);
                    return;
                }    
            }
        }

        IEnumerator SpawnObject()
        {
            while (true)
            {
                EnableObjectInPool();
                float spawnTimer = Random.Range(minSpawnTimer, MaxSpawnTimer);
                yield return new WaitForSeconds(spawnTimer);
            }
        }

    //     public GameObject GetInstances()
    //    {
    //         GameObject instance =  pool[lastInstanceIndex++];
    //         if (lastInstanceIndex >= pool.Count)
    //         {
    //             lastInstanceIndex = 0;
    //         }
    //         return instance;
    //    }
    }
}

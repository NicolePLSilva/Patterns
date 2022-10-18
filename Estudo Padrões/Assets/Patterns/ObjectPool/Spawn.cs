using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolingSystem
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] ObjectPool objectPool;
        void Start()
        {
            
        }

        // Update is called once per frame
        // void Update()
        // {
        //     var spawnObject = objectPool.GetInstances();
        //     spawnObject.transform.position = transform.position;

        //     spawnObject.SetActive(true);
        // }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectPooler : MonoBehaviour
{
    [SerializeField] private Objects[] objects;
    private Dictionary<string, Queue<Transform>> poolDictionary = new Dictionary<string, Queue<Transform>>();

    public void CreateObjects(Transform parent = null)
    {
        foreach (var item in objects)
        {
            Queue<Transform>  queue = new Queue<Transform>();
            for (int i = 0; i < item.quantity; i++)
            {
                Transform prefab = Instantiate(item.prefab, parent);
                prefab.gameObject.SetActive(false);
                queue.Enqueue(prefab);
            }
            poolDictionary.Add(item.tag, queue);
        }
    }

    public Transform ReleaseObject(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"There're no objects with tag {tag}");
            return null;
        }

        Transform obj = poolDictionary[tag].Dequeue();
        obj.position = position;
        obj.rotation = rotation;
        obj.gameObject.SetActive(true);
        poolDictionary[tag].Enqueue(obj);
        return obj; 
    }

    [Serializable]
    public struct Objects
    {
        public Transform prefab;
        public string tag;
        public int quantity;
    }
}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPool", menuName = "ScriptableObjects/ObjectPool")]
public class ObjectPool : ScriptableObject{
    public GameObject Prefab;
    public int Size;
    protected Queue<GameObject> pool = new Queue<GameObject>();

    [HideInInspector]
    public Transform parent = null;

    public void Initialize(){
        for (int i = 0; i < Size; i++){
            GameObject obj = Instantiate(Prefab);
            obj.transform.SetParent(parent);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject(){
        if (pool.Count == 0){
            GameObject objNew = Instantiate(Prefab);
            objNew.transform.SetParent(parent);
            return objNew;
        }
        var obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj){
        obj.BroadcastMessage("OnReturnToPool", SendMessageOptions.DontRequireReceiver);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
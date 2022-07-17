using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolMng : Singleton<ObjectPoolMng>
{
    [SerializeField] ObjectPoolInfo[] objectPools;

    List<Unit> AllObjectPoolList = new List<Unit>();

    void Awake()
    {
        for (int i = 0; i < objectPools.Length; i++)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.name = objectPools[i].Name + "_Pools";

            for (int j = 0; j < objectPools[i].Count; j++)
            {
                GameObject pObj = Instantiate(objectPools[i].Object);
                pObj.SetActive(false);
                pObj.name = objectPools[i].Name;
                pObj.transform.SetParent(obj.transform);
                AllObjectPoolList.Add(pObj.GetComponent<Unit>());
            }

            Debug.Log("Object Create");
        }
    }

    private void LateUpdate()
    {
        for(int i=0;i<AllObjectPoolList.Count; i++)
        {
            AllObjectPoolList[i].StatUpdate();
        }
    }

    ObjectPoolInfo GetNameToObject(string name)
    {
        for (int i = 0; i < objectPools.Length; i++)
        {
            if (objectPools[i].Name.Equals(name))
                return objectPools[i];
        }

        Debug.LogError(string.Format("Not found Object name is {0} to objectPool Array", name));
        return null;
    }

    public T SpawnObject<T>(string name, Vector2 v, float time = 0, string usage = "") where T : Unit
    {
        T spawnObj;
        for (int i = 0; i < AllObjectPoolList.Count; i++)
        {
            if(AllObjectPoolList[i].name.Equals(name) && AllObjectPoolList[i].gameObject.activeSelf == false)
            {
                spawnObj = AllObjectPoolList[i].GetComponent<T>();
                StartCoroutine(Spawn(spawnObj, v, time, usage));
                return spawnObj;
            }
        }

        ObjectPoolInfo info = GetNameToObject(name);
        GameObject pObj = Instantiate(info.Object);
        pObj.SetActive(false);
        pObj.name = info.Name;
        StartCoroutine(Spawn(pObj.GetComponent<Unit>(), v, time, usage));
        for (int i=0;i<transform.childCount;i++)
        {
            if(transform.GetChild(i).name.Equals(info.Name+"_Pools"))
                pObj.transform.SetParent(transform.GetChild(i));
        }
        return pObj.GetComponent<T>();
    }

    IEnumerator Spawn(Unit obj, Vector2 v, float time = 0, string usage = "")
    {
        yield return new WaitForSeconds(time);
        obj.Spawn(v, usage);

    }
}

[System.Serializable]
public class ObjectPoolInfo
{
    public string Name;
    public GameObject Object;
    public uint Count;
}
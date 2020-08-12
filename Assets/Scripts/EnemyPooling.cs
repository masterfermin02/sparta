using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public GameObject prefabs;
    public int amount = 10;
    public int instantiateGap = 5;

    // Start is called before the first frame update
    void Start()
    {
        InitializePool();
        InvokeRepeating("GetEnemyFromPool", 1f, instantiateGap);
    }

    private void InitializePool()
    {
        for(int i = 0; i < amount; i++)
        {
            AddEnemyToPool();
        }
    }

    private void AddEnemyToPool()
    {
        GameObject enemy = Instantiate(prefabs, transform.position, Quaternion.identity, transform);
        enemy.SetActive(false);
    }

    private GameObject GetEnemyFromPool()
    {
        GameObject enemy = null;

        for(int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            if (!obj.activeSelf)
            {
                enemy = obj;
                break;
            }
        }

        if (enemy == null)
        {
            AddEnemyToPool();
            enemy =  transform.GetChild(transform.childCount - 1).gameObject;
        }

        enemy.transform.position = transform.position;
        enemy.SetActive(true);
        return enemy;
    }
}

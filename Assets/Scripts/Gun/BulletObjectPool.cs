using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    /// <summary>
    /// Object pool for bullet instantiation
    /// </summary>
    public GameObject bulletPrefab;
        
    private List<GameObject> bullets = new List<GameObject>();

    private const int INITIAL_POOL_SIZE = 10;

    private const int MAX_POOL_SIZE = 20;


    private void Start()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Need a reference to the bullet prefab");
        }

        for (int i = 0; i < INITIAL_POOL_SIZE; i++)
        {
            GenerateBullet();
        }
    }


    private void GenerateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform);

        newBullet.SetActive(false);

        bullets.Add(newBullet);
    }


    public GameObject GetBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            GameObject thisBullet = bullets[i];

            if (!thisBullet.activeInHierarchy)
            {                
                return thisBullet;
            }
        }

        if (bullets.Count < MAX_POOL_SIZE)
        {
            GenerateBullet();

            GameObject lastBullet = bullets[bullets.Count - 1];

            return lastBullet;
        }

        return null;
    }
}

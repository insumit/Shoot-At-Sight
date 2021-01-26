using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public BulletObjectPool bulletPool;

    private float fireTimer;

    private float fireInterval = 0.1f;

    void Start()
    {
        fireTimer = Mathf.Infinity;

        if (bulletPool == null)
        {
            Debug.LogError("Need a reference to the object pool");
        }
    }



    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && fireTimer > fireInterval)
        {
            fireTimer = 0f;

            GameObject newBullet = GetABullet();

            if (newBullet != null)
            {
                newBullet.SetActive(true);

                newBullet.transform.forward = transform.forward;

                newBullet.transform.position = transform.position + transform.forward * 2f;
            }
            else
            {
                Debug.Log("Couldn't find a new bullet");
            }
        }

        fireTimer += Time.deltaTime;
    }



    private GameObject GetABullet()
    {
        GameObject bullet = bulletPool.GetBullet();

        return bullet;
    }
}

using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hittEffect;
    [SerializeField] private LayerMask layersToCollide;

    [SerializeField] private float autoDestroyTime = 5f;

    private new Rigidbody rigidbody;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Instantiate(hittEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Awake()
    {
        StartCoroutine(
            DestroySelfAfterSeconds(autoDestroyTime)); // Destruir ela depois de um tempo para não consumir memoria
    }

    private IEnumerator DestroySelfAfterSeconds(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hittEffect;
    [SerializeField] private LayerMask layersToCollide;
    [SerializeField] private float autoDestroyTime = 5f;

    [HideInInspector] public float bulletSpeed;

    [HideInInspector] public Vector3 target;
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
        Destroy(gameObject, autoDestroyTime);
    }

    private void Update()
    {

        transform.position = 
            Vector3.MoveTowards(transform.position, target,
            bulletSpeed * Time.deltaTime);
    }
}
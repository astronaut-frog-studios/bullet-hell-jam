using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hittEffect;
    [SerializeField] private LayerMask layersToCollide;
    [SerializeField] private float autoDestroyTime = 5f;

    [HideInInspector] private float bulletSpeed;

    [HideInInspector] private Vector3 target;
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

    public void Shoot(float speed, Vector3 direction)
    {
        bulletSpeed = speed;
        target = direction;
        rigidbody.AddForce(target.normalized * bulletSpeed, ForceMode.Impulse);
    }
}
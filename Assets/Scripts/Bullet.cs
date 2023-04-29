using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hittEffect;
    [SerializeField] private LayerMask layersToCollide;

    [SerializeField] private float autoDestroyTime = 5f;

    public Rigidbody rigid;

    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((layersToCollide.value & (1 << other.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }

        //can add components 
    }

    private void Awake()
    {
        StartCoroutine(DestroySelfAfterSeconds(autoDestroyTime)); // Destruir ela depois de um tempo para não consumir memoria
    }

    private IEnumerator DestroySelfAfterSeconds(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
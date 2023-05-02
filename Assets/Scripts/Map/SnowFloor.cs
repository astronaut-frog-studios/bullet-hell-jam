using System;
using System.Collections;
using UnityEngine;

namespace Map
{
    public class SnowFloor : MonoBehaviour
    {
        public bool recover;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Material groundMaterial;
        [SerializeField] private Material snowMaterial;
        
        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        // voids normais
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                meshRenderer.material = groundMaterial;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && !recover)
            {
                StartCoroutine(BackToSnow());
            }
        }

        private IEnumerator BackToSnow()
        {
            recover = true;
            yield return new WaitForSeconds(2);
            recover = false;
            meshRenderer.material = snowMaterial;
        }

        public bool HasSnow()
        {
            return !recover;
        }
    }
}
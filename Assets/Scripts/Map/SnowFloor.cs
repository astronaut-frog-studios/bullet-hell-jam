using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    public bool recover = false;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material groundMaterial;
    [SerializeField] private Material snowMaterial;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // voids normais

    void OnTriggerEnter ( Collider other ){
        if(other.CompareTag("Player")){
            meshRenderer.material =groundMaterial;
        }
    }
    

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") && !recover){
            StartCoroutine(BackToSnow());
        }
    }


    IEnumerator BackToSnow(){
        recover = true;
        yield return new WaitForSeconds(2);
        recover = false;
        meshRenderer.material = snowMaterial;
    }

    public bool HasSnow(){
        return !recover;
    }
}

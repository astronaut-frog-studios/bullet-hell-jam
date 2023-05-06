using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public float Health { get; private set; }
    public float DamageToLight { get; private set; }

    [SerializeField] private string myTest;
    [SerializeField] private UnityEvent LessLight;
    
 
}

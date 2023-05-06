using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    //classe health e jogar a lógica lá
    public float CurrentHealth { get; private set; }
    public float DamageToLight { get; private set; }

    [SerializeField] private string myTest;
    [SerializeField] private UnityEvent LessLight;

    private void Start()
    {
        /*
        PlayerEvents.DamageReceived += ReceivedDamage;
        PlayerEvents.PlayerDifficulty += PlayerDifficultyChange;

        playerCollision = GetComponent<PlayerMeleeCollision>();

        Health = playerObject.health;
        Damage = playerObject.damage;
        */
    }

    private void ReceivedDamage(float amountToLose, bool isMelee)
    {
        /*
        if (isMelee)
        {
            playerCollision.DamageReceived();
        }

        Health -= amountToLose;
        health = Health;

        print("Health: " + Health);

        if (Health <= 0)
        {
            GameManager.Instance.OnGameEnded(false);
        }
        */
    }
    public void Life()
    {
        
    }
 
}

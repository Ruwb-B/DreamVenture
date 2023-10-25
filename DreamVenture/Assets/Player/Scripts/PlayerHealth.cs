using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float _damageImmuneDuration = 1f;
    [SerializeField] HealthBar _healthBar;
    bool _damageImmune = false;
    bool _isDeath = false;
    [SerializeField] CentralPoint _centralPoint;

    private void Start()
    {
        _healthBar.SetMaxHealth(GameManager.gameManager._playerHealth.MaxHealth);
        _healthBar.SetHealth(GameManager.gameManager._playerHealth.Health);
    }

    public void TakeDamage(int damageAmount)
    {
        if (!_damageImmune && !_isDeath)
        {
            _centralPoint.PlayDamagedSound();
            GameManager.gameManager._playerHealth.DamageUnit(damageAmount);
            _healthBar.SetHealth(GameManager.gameManager._playerHealth.Health);
            if (GameManager.gameManager._playerHealth.Health > 0)
            {
                StartCoroutine(ImmuneDamage());
            }
            else
            {
                _centralPoint.PlayDeathSound();
                OnDeath();
            }
        }
    }

    public void Heal(int healAmount)
    {
        GameManager.gameManager._playerHealth.HealUnit(healAmount);
        _healthBar.SetHealth(GameManager.gameManager._playerHealth.Health);
    }

    IEnumerator ImmuneDamage()
    {
        _damageImmune = true;
        yield return new WaitForSeconds(_damageImmuneDuration);
        _damageImmune = false;
    }


    // Disable player's scripts
    void OnDeath()
    {
        Balance[] balances = GetComponentsInChildren<Balance>();
        foreach (Balance balance in balances)
        {
            balance.enabled = false;
        }
        Grabbing[] grabbings = GetComponentsInChildren<Grabbing>();
        foreach(Grabbing grabbing in grabbings)
        {
            grabbing.enabled = false;
        }
        PickUp[] pickUps = GetComponentsInChildren<PickUp>();
        foreach(PickUp pickUp in pickUps)
        {
            pickUp.enabled = false;
        }
        PlayerController playerController = GetComponentInChildren<PlayerController>();
        playerController.enabled = false;
        ArmController[] armControllers = GetComponentsInChildren<ArmController>();
        foreach(ArmController armController in armControllers)
        {
            armController.enabled = false;
        }
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        _isDeath = true;
    }
}

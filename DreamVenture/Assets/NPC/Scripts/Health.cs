using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _baseHealth = 1000;
    [SerializeField] int _maxHealth = 1000;
    [SerializeField] float _damageImmuneDuration = 1f;
    [SerializeField] HealthBar _healthBar;
    [SerializeField] CentralPoint _centralPoint;
    [SerializeField] GameObject _weaponHolding;
    [SerializeField] GameObject _weaponSpawn;
    UnitHealth _unitHealth;
    bool _damageImmune = false;
    bool _isDeath = false;

    private void Awake()
    {
        _unitHealth = new UnitHealth(_baseHealth, _maxHealth);
    }

    private void Start()
    {
        _healthBar.SetMaxHealth(_maxHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        if (!_damageImmune && !_isDeath)
        {
            _unitHealth.DamageUnit(damageAmount);
            _healthBar.SetHealth(_unitHealth.Health);

            if (_unitHealth.Health > 0)
            {
                _centralPoint.PlayDamagedSound();
                StartCoroutine(ImmuneDamage());
            }
            else
            {
                _centralPoint.PlayDeathSound();
                OnDeath();
            }
        }
    }

    IEnumerator ImmuneDamage()
    {
        _damageImmune = true;
        yield return new WaitForSeconds(_damageImmuneDuration);
        _damageImmune = false;
    }

    void OnDeath()
    {
        Balance[] balances = GetComponentsInChildren<Balance>();
        foreach (Balance balance in balances)
        {
            balance.enabled = false;
        }
        Canvas canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        PlayerDetector playerDetector = GetComponentInChildren<PlayerDetector>();
        playerDetector.enabled = false;
        EnemyBehaviors enemyBehaviors = GetComponentInChildren<EnemyBehaviors>();
        enemyBehaviors.StopArmControll();
        enemyBehaviors.enabled = false;
        _isDeath = true;
        if (_weaponHolding != null)
        {
            GameObject weapon = Instantiate(_weaponSpawn, _weaponHolding.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Rigidbody2D rigidbody2D = weapon.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(Vector2.up * 100 * Time.deltaTime);
            Destroy(_weaponHolding);
        }

    }
}


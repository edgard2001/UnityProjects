using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Transform _healthBar;
    [SerializeField] private Transform _filling;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth = 100f;

    // Start is called before the first frame update
    private void Start()
    {
        if (_healthBar == null) _healthBar = gameObject.transform.GetChild(2);
        if (_filling == null) _filling = _healthBar.GetChild(0).GetChild(1);
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0.0f, _maxHealth);
        UpdateHealthBar();

        if (_currentHealth == 0.0f)
        {
            Destroy(gameObject);
        }

        
    }

    private void UpdateHealthBar()
    {
        Vector3 scale = _filling.localScale;
        scale.x = Mathf.Clamp(_currentHealth / _maxHealth, 0.0f, 1.0f);
        _filling.localScale = scale;
    }

}

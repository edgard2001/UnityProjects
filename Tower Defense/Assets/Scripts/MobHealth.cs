using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHealth : MonoBehaviour
{
    [SerializeField] private Transform _healthBar;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _currentHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
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
        Vector3 scale = _healthBar.localScale;
        scale.x = Mathf.Clamp(_currentHealth / _maxHealth, 0.0f, 1.0f);
        _healthBar.localScale = scale;
    }
}

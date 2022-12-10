using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Rigidbody2D rb;

    private float _health = 0f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Slider _healthSlider;

    private void Start()
    {
        _health = _maxHealth;
        _healthSlider.maxValue = _maxHealth;
    }

    public void UpdateHealth(float mod)
    {
        _health += mod;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        else if (_health <= 0f)
        {
            _health = 0f;
            _healthSlider.value = _health;
            PlayerDied();
           
        }
    }

    private void PlayerDied() 
    {
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }

    private void OnGUI()
    {
        float t = Time.deltaTime / 1f;
        _healthSlider.value = Mathf.Lerp(_healthSlider.value, _health,t);
    }
}

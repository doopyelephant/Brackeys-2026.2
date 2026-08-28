using UnityEngine;

namespace DefaultNamespace
{
    public class Health : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth = 100f;
        public HealthBar healthBar;
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            ValidateHealth();
            OnHealthChanged();
            DamageEffect(damage / maxHealth);
        }

        public void OnHealthChanged()
        {
            if (healthBar == null)
                return;
            healthBar.Changed(currentHealth / maxHealth);
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
            ValidateHealth();
            OnHealthChanged();
            HealEffect( amount / maxHealth);
        }

        public void ValidateHealth()
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            if (currentHealth <= 0f)
            {
                Die();
            }
        }

        public virtual void DamageEffect(float fraction)
        {

        }

        public virtual void HealEffect(float fraction)
        {

        }

        public virtual void Die()
        {

        }
    }
}
using UnityEngine;

public class DummyEnemy : MonoBehaviour, IDamageable
{
    public int maxHP = 50;
    int hp;

    void Awake() { hp = maxHP; }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log($"Enemy HP: {hp}");
        if (hp <= 0) Destroy(gameObject);
    }
}

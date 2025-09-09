using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    public int damage = 10;
    Collider2D col;
    bool active;

    void Awake() { col = GetComponent<Collider2D>(); col.isTrigger = true; col.enabled = false; }

    public void SetActive(bool on) { active = on; col.enabled = on; }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!active) return;
        if (other.TryGetComponent<IDamageable>(out var d)) d.TakeDamage(damage);
    }
}

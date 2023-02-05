using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected float health;
    protected abstract void Die();
    [SerializeField] protected int delayDieTime;
    public float Health
    {
        get => health;
        set => health = value;
    }
}
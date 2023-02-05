using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected float health;

    public float Health
    {
        get => health;
        set => health = value;
    }
}
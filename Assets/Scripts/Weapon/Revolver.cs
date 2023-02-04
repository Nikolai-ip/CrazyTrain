using UnityEngine;

public class Revolver : Weapon
{
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
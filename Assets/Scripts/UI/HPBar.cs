using System;
using UnityEngine;

namespace UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private int currentHp = 0;
        [SerializeField] private Animator animator;
        [SerializeField] private Entity attachedEntity;
        private const int MaxHp = 10;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            GetEntityHp();
            animator.SetInteger("currHp", currentHp);
        }

        private void GetEntityHp()
        {
            var entityHp = attachedEntity.Health;
            SetHp((int)Math.Ceiling(entityHp));;
        }

        public void SetHp(int hp)
        {
            if (hp > MaxHp)
            {
                hp = MaxHp;
            }

            if (hp < 0)
            {
                hp = 0;
            }
            currentHp = hp;
        }
    }
}

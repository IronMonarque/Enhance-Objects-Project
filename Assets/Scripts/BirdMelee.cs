using UnityEngine;

namespace Assets.Scenes.Project.Scripts
{
    public class BirdMelee : BirdEnemy
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float attackTime = 0;

        BirdEnemy enemy;

        private float timer = 0;

        private float setSpeed = 0;

        protected override void Start()
        {
            base.Start();
            health = new HealthSeed(1, 0, 1);
            setSpeed = speed;
        }

        protected override void Update()
        {
            base.Update();

            if (target == null)
                return;

            if (Vector2.Distance(transform.position, target.position) < attackRange)
            {
                speed = 0;
                Attack(attackTime);
            }
            else
            {
                speed = setSpeed;
            }
        }

        public override void Attack(float interval)
        {
            if (timer <= interval)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                target.GetComponent<BirdIDamageable>().GetDamage(weapon.GetDamage());
                Debug.Log($"Melee enemy damage ; {weapon.GetDamage()}");
            }
        }

        public void SetMeleeEnemy(float attackRange, float attackTime)
        {
            this.attackRange = attackRange;
            this.attackTime = attackTime;
        }
    }
}
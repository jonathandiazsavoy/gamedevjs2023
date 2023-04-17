using Godot;

namespace code.StateMachines.CharacterStates.NPCStates.EnemyStates
{
    public abstract class EnemyState : NPCState, IHurtable
    {
        protected Enemy enemy;

        protected EnemyState(Enemy enemy) : base(enemy)
        {
            this.enemy = enemy;
        }

        public void ApplyIncomingAttack(Node2D attacker, Attack attack)
        {
            this.SwitchState(new TakingDamage(enemy));
            int damage = attack.Damage - character.CurrentStats.Defense;
            character.TakeDamage(damage);
            if (attack.PushForce > 0)
            {
                Vector2 hitDirection = (character.GlobalPosition - attacker.GlobalPosition).Normalized();
                character.MoveAndSlide(hitDirection * attack.PushForce);
            }
        }

        public void TakeDamage(int hpAmount)
        {
            if (character.CurrentStats.ApplyDamage(hpAmount))
            {
                // When hp is depleted
                this.SwitchState(new Dying(enemy));
            }
        }
    }
}

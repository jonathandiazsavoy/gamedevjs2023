using Godot;

namespace code.StateMachines.CharacterStates.EnemyStates
{
    public class Dying : EnemyState
    {
        public Dying(Enemy enemy) : base(enemy)
        {
        }

        [Signal]
        public delegate void EnemyDied(Enemy enemy);

        public override BaseFSMState Update(float delta)
        {
            enemy.MoveAndSlide(enemy.incomingAttackForce);
            return this;
        }

        protected override void EnterState()
        {
            enemy.AnimationPlayer.Play("DEATH");
        }
    }
}

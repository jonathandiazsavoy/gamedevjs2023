using Godot;

namespace code.StateMachines.CharacterStates.EnemyStates.FlyingEnemyStates
{
    public class Sleeping : FlyingEnemyState
    {
        public Sleeping(FlyingEnemy flyingEnemy) : base(flyingEnemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            if (enemy.Alerted) return SwitchState(new ChasingTarget(flyingEnemy));
            return this;
        }

        protected override void EnterState()
        {
            flyingEnemy.AnimationPlayer.Play("sleeping");
        }
    }
}

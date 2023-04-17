using code.Items;
using Godot;

namespace code.StateMachines.CharacterStates.NPCStates.EnemyStates
{
    public class Dying : EnemyState
    {
        public Dying(Enemy enemy) : base(enemy)
        {
        }

        [Signal]
        public delegate void EnemyDied(Item item);

        public override BaseFSMState Update(float delta)
        {
            return this;
        }

        protected override void EnterState()
        {
            character.AnimationPlayer.Play("DEATH");
        }
    }
}

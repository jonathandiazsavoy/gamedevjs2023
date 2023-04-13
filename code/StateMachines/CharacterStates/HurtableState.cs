using Godot;

public abstract class HurtableState : CharacterState, IHurtable
{
    protected HurtableState(Character character) : base(character)
    {
    }

    public void ApplyIncomingAttack(Node2D attacker, Attack attack)
    {
        this.SwitchState(new TakingDamage(character));
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
            this.SwitchState(new Dying(character));
        }
    }
}


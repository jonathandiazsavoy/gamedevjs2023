using Godot;

public interface IHurtable
{
    void ApplyIncomingAttack(Node2D attacker, Attack attack);
    void TakeDamage(int hpAmount);
}

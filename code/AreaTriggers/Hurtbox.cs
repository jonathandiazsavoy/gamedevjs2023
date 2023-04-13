using Godot;

public class Hurtbox : Area2D
{
    private IHurtable parent;

    public override void _Ready()
    {
        parent = this.GetParentOrNull<IHurtable>();
        if (parent == null) GD.PushWarning("HURTBOX AT " + this.GlobalPosition + " COULD NOT GET PARENT THAT IS IHURTABLE");
    }

    public void ApplyAttack(Hitbox hit)
    {
        parent.ApplyIncomingAttack(hit.Parent.Attacker, hit.Parent.Attack);
    }
}

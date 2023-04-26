using Godot;

public class Hitbox : Area2D
{
    public IAttacker Parent { get; private set; }

    public override void _Ready()
    {
        Parent = this.GetParentOrNull<IAttacker>();
        if (Parent == null) GD.PushWarning("HITBOX AT "+this.GlobalPosition+" COULD NOT GET PARENT THAT IS AN IATTACKER");
    }
    public override void _Process(float delta)
    {
        // TODO 9 Lock hitbox rotation
        //this.GlobalRotationDegrees = 0;
    }

    public void OnHitboxAreaEntered(Area2D area2d)
    {
        if (area2d is Hurtbox hurtbox) hurtbox.ApplyAttack(this);
    }
}

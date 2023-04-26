using Godot;

public class Attack : Resource
{
    public Attack(Character owner, int damage, float pushForce) 
    {
        this.Owner = owner;
        this.Damage = damage;
        this.PushForce = pushForce;
    }

    public Character Owner { get; private set; }
    public int Damage { get; private set; }
    public float PushForce { get; private set; }
}

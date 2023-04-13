using Godot;

public class Attack : Resource
{
    public Attack(int damage, float pushForce) 
    {
        this.Damage = damage;
        this.PushForce = pushForce;
    }

    public int Damage { get; private set; }
    public float PushForce { get; private set; }
}

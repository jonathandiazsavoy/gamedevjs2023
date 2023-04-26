using Godot;

public class CharacterStats : Resource
{
    public CharacterStats()
    {
        this.MaxHp = 1;
        this.MaxMp = 0;
        this.Strength = 1;
        this.Defense = 0;
        this.Speed = 1;

        this.Hp= MaxHp;
        this.Mp= MaxMp;
    }
    public CharacterStats(int maxHp, int maxMp, int strength, int defense, float speed)
    {
        this.MaxHp = maxHp;
        this.MaxMp = maxMp;
        this.Strength = strength;
        this.Defense = defense;
        this.Speed = speed;

        this.Hp = MaxHp;
        this.Mp = MaxMp;
    }

    [Export]
    public int MaxHp { get; set; }
    [Export]
    public int MaxMp { get; private set; }
    [Export]
    public int Strength { get; set; }
    [Export]
    public int Defense { get; private set; }
    [Export]
    public float Speed { get; set; }

    [Export]
    public int Hp { get; set; }
    [Export]
    public int Mp { get; set; }

    public bool ApplyDamage(int hpDamage)
    {
        if(this.Hp > hpDamage)
        {
            this.Hp -= hpDamage;
            return false;
        }
        else
        {
            this.Hp = 0;
            return true;
        }
    }
    public void ApplyHeal(int hpHeal)
    {
        this.Hp += hpHeal;
        if (this.Hp > this.MaxHp) this.Hp = this.MaxHp; 
    }
    public bool UseMp(int mpUsage)
    {
        if (this.Mp > mpUsage)
        {
            this.Mp -= mpUsage;
            return true;
        }
        else
        {
            return false;
        }
    }
}

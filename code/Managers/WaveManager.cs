using code.Items;
using Godot;

public class WaveManager : YSort
{
    public const string PATH_TO_WAVES = "res://scenes/levels/waves/";
    public const string PATH_TO_PORTALS = "res://scenes/area_triggers/portals/";
    public const string WAVE_FILE_NAME_PREFIX = "wave";
    public const string SHOP_PORTAL_FILE_NAME = "ShopPortal";
    public const string NEXT_WAVE_PORTAL_FILE_NAME = "NextWavePortal";

    public const string SHOP_PORTAL_NODE_NAME = "ShopPortal";

    private GameManager gameManager;
    private bool allEnemiesKilled = false;

    public Wave Wave 
    { get 
        {
            return this.GetNode<Wave>("Wave");
            /* TODO to be removed
            foreach (Node child in this.GetChildren())
            {
                if (child is Wave wave) return wave;
            }
            return null;
            */
        } 
    }

    public int EnemyCount { get { return Wave.Enemies.GetChildCount(); } }

    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
    }

    public void OpenPortal()
    {
        PackedScene packed = GD.Load<PackedScene>(PATH_TO_PORTALS + NEXT_WAVE_PORTAL_FILE_NAME + ".tscn");
        NextWavePortal node = packed.Instance<NextWavePortal>();
        node.GlobalPosition = gameManager.MapCenter.GlobalPosition;
        this.Wave.AddChild(node);
    }
    public void OpenShop()
    {
        PackedScene packed = GD.Load<PackedScene>(PATH_TO_PORTALS + SHOP_PORTAL_FILE_NAME + ".tscn");
        ShopPortal node = packed.Instance<ShopPortal>();
        node.GlobalPosition = gameManager.MapCenter.GlobalPosition;
        this.Wave.AddChild(node);
    }
    public void CloseShop()
    {
        Node shopNode = Wave.GetNodeOrNull(SHOP_PORTAL_NODE_NAME);
        if (shopNode != null) shopNode.QueueFree();
    }

    // **************************************************
    // Signal listeners
    // **************************************************
    public void OnAlarmCountdownHalfTime()
    {
        if (allEnemiesKilled) return;
        OpenShop();
        this.gameManager.SoundPlayer.Play("ding");
    }
    public void OnAlarmCountdownTimeout()
    {
        Wave.Items.QueueFree();
        CloseShop();
        this.gameManager.SoundPlayer.Play("alarm");
        // Alert all enemies
        foreach (Enemy enemy in Wave.Enemies.GetChildren())
        {
            enemy.Alerted= true;
            enemy.currentTarget = gameManager.GetNode<Player>(GameManager.NODE_PATH_TO_PLAYER);
        }
    }
    public void OnAllEnemiesKilled()
    {
        Node shopNode = Wave.GetNodeOrNull(SHOP_PORTAL_NODE_NAME);
        if (shopNode != null) shopNode.Free();
        //Wave.QueueFree();
        OpenPortal();
        this.gameManager.SoundPlayer.Play("victory");
        this.gameManager.MusicPlayer.Play("calm_phase");
        allEnemiesKilled = true;
    }
    public void LoadWave(int waveNumber)
    {
        allEnemiesKilled=false;
        PackedScene packedWave = GD.Load<PackedScene>(PATH_TO_WAVES + WAVE_FILE_NAME_PREFIX + waveNumber + ".tscn");
        Wave.Free();
        //this.CallDeferred("AddChild", packedWave.Instance<Wave>());
        this.AddChild(packedWave.Instance<Wave>()); // TODO 9 loading this way throws errors
        foreach (Enemy enemy in Wave.Enemies.GetChildren())
        {
            // Load enemies in as unalerted
            enemy.Alerted = false;
            enemy.Connect("EnemyDied", gameManager, "OnEnemyDied");
        }
        foreach (Item item in Wave.Items.GetChildren())
        { 
            item.Connect("ItemUsed", gameManager, "OnCountDownModifierItemUsed");
        }
    }   
}

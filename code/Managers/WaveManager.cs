using Godot;

public class WaveManager : YSort
{
    public const string PATH_TO_WAVES = "res://scenes/levels/waves/";
    public const string WAVE_FILE_NAME_PREFIX = "wave";

    private GameManager gameManager;
    private Wave Wave 
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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>("/root/GameManager");
    }

    // **************************************************
    // Signal listeners
    // **************************************************
    public void OnAlarmCountdownTimeout()
    {
        Wave.Items.QueueFree();
    }
    public void OnAllEnemiesKilled()
    {
        //TODO portal opens and victory fanfare plays
    }
    public void LoadWave(int waveNumber)
    {
        PackedScene packedWave = GD.Load<PackedScene>(PATH_TO_WAVES + WAVE_FILE_NAME_PREFIX + waveNumber + ".tscn");
        Wave.Free();
        this.AddChild(packedWave.Instance<Wave>());
    }   
}

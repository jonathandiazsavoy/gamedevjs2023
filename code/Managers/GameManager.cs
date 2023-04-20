using code.Helpers;
using code.Items.TimeItems;
using Godot;

public class GameManager : Node2D
{
    const float ALARM_COUNTDOWN_MAX_START = 60;
    const int FINAL_WAVE_NUMBER = 12;

    public const string PATH_TO_SOUNDS = "res://assets/audio/music/";

    public Timer AlarmCountdown { get; private set; }
    public WaveManager WaveManager { get; private set; }
    public AudioStreamPlayer AudioStreamPlayer { get; private set; }
    public Node2D Level { get; private set; }
    
    public MusicPlayer MusicPlayer;

    public float TotalRunTime { get; private set; }
    public float CenterClockTotal
    {
        get 
        {
            return (Mathf.Abs(AlarmCountdown.TimeLeft - ALARM_COUNTDOWN_MAX_START)) + (CurrentWaveNumber-1)*AnalogueClock.MINUTES_IN_HOUR;
        }
    }
    public float SecondsUntilNextWave
    {
        get
        {
            return (Mathf.Abs(AlarmCountdown.TimeLeft - ALARM_COUNTDOWN_MAX_START));
        }
    }
    public int EnemyCount { get; private set; }
    public int CurrentWaveNumber { get; private set; }

    private float initialMusicPitchScale;
    public bool AlarmTriggered;
    public int Score;
    public int Money;

    [Signal]
    public delegate void AllEnemiesKilled();

    public override void _Ready()
    {
        AlarmCountdown = this.GetNode<Timer>("AlarmCountdown");
        WaveManager = this.GetNode<WaveManager>("Level/Objects/WaveManager");
        AudioStreamPlayer = this.GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        Level = this.GetNode<Node2D>("Level");
        MusicPlayer = new MusicPlayer(AudioStreamPlayer, PATH_TO_SOUNDS);

        StartNewRun();
        StartNewLoop();
        
        initialMusicPitchScale = AudioStreamPlayer.PitchScale;
    }
    public override void _Process(float delta)
    {
        TotalRunTime += delta;

        if (!AlarmTriggered) 
        {
            AdjustMusicAccordingToAlarmCountdown();
        }

        //TODO remove this test stuff
        if (Input.IsActionJustPressed("test1"))
        {
            AdjustAlarmCountdown(5);
            //Level.GetTree().Paused= !Level.GetTree().Paused;
        }
        if (Input.IsActionJustPressed("test2"))
        {
            AdjustAlarmCountdown(-5);
            GetTree().Paused = false;
        }
    }

    public void AdjustMoney(int adjustmentAmount)
    {
        if(adjustmentAmount > 0)
        {
            this.Money += adjustmentAmount;
            this.Score += adjustmentAmount;
        }
        else
        {
            this.Money += adjustmentAmount;
        }
    }

    private void StartNewRun()
    {
        TotalRunTime = 0;
        CurrentWaveNumber = 1;
    }
    private void StartNewLoop()
    {
        AlarmTriggered = false;
        AlarmCountdown.Start(ALARM_COUNTDOWN_MAX_START);
        MusicPlayer.Play("calm_phase");

        WaveManager.LoadWave(CurrentWaveNumber);
        EnemyCount = WaveManager.EnemyCount;
    }

    private void AdjustAlarmCountdown(float adjustAmount)
    {
        // TODO this is bugged if you get a negative adjustment when the timer is near 0 - it goes back up to 3
        if (!AlarmTriggered) AlarmCountdown.Start(Mathf.Clamp(AlarmCountdown.TimeLeft + adjustAmount, 0, ALARM_COUNTDOWN_MAX_START));
    }
    private void AdjustMusicAccordingToAlarmCountdown()
    {
        // TODO refactor later
        float adjustTo = Mathf.Clamp(SecondsUntilNextWave - 45, 0, 20);
        if (adjustTo != 0)
        {
            // Increase speed and pitch exponentially as countdown gets closer to 0
            AudioStreamPlayer.PitchScale = initialMusicPitchScale + (adjustTo / 25) * (adjustTo / 25);
        }
        else
        {
            AudioStreamPlayer.PitchScale = initialMusicPitchScale;
        }
    }

    // **************************************************
    // Signal listeners
    // **************************************************
    public void OnCountDownModifierItemUsed(CountDownModifier countdownModifier)
    {
        AdjustAlarmCountdown(countdownModifier.CountDownChange);
    }
    public void OnEnemyDied(Enemy enemy)
    {
        // Add enemy point value as money
        AdjustMoney(enemy.GetPointValue());
        if (this.EnemyCount-1 > 0)
        {
            this.EnemyCount--;
        }
        else
        {
            this.EnemyCount=0;
            EmitSignal(nameof(AllEnemiesKilled));
        }
    }
    public void OnAlarmCountdownTimeout()
    {
        AlarmTriggered = true;
        MusicPlayer.Play("alert_phase");
    }
    public void OnGoToNextWave(Player player)
    {
        CurrentWaveNumber++;
        if (CurrentWaveNumber > FINAL_WAVE_NUMBER) { 
            // TODO go to you win screen
        }
        StartNewLoop();
    }
}

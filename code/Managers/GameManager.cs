using code.Helpers;
using code.Items.TimeItems;
using Godot;

public class GameManager : Node2D
{
    public const float ALARM_COUNTDOWN_MAX_START = 60;
    public const int START_WAVE_NUMBER = 1;
    public const int FINAL_WAVE_NUMBER = 8;

    public const string NODE_PATH_TO_PLAYER = "Level/Objects/Player";
    public const string PATH_TO_MUSIC = "res://assets/audio/music/";
    public const string PATH_TO_SOUNDS = "res://assets/audio/sounds/events/";

    public Timer AlarmCountdown { get; private set; }
    public WaveManager WaveManager { get; private set; }
    public AudioStreamPlayer MusicStreamPlayer { get; private set; }
    public AudioStreamPlayer SoundStreamPlayer { get; private set; }
    public Node2D Level { get; private set; }
    public Position2D MapCenter { get { return this.GetNode<Position2D>("Level/Map/Center");  } }
    
    public MusicPlayer MusicPlayer;
    public SoundPlayer SoundPlayer;
    public Player Player; // Player gets set between waves and when entering shop
    public PackedScene PackedPlayer;

    public float TotalRunTime { get; private set; }
    public float TotalWaveTime { get; private set; }
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
    public bool AlarmHalfTImeTriggered;
    public int Score;
    public int Money;

    [Signal]
    public delegate void AllEnemiesKilled();
    [Signal]
    public delegate void AlarmCountdownHalfTime();
    [Signal]
    public delegate void GoToWaveCompletedScreen();
    [Signal]
    public delegate void GoToGameCompletedScreen();
    [Signal]
    public delegate void GoToGameOverScreen();

    public override void _Ready()
    {
        AlarmCountdown = this.GetNode<Timer>("AlarmCountdown");
        WaveManager = this.GetNode<WaveManager>("Level/Objects/WaveManager");
        MusicStreamPlayer = this.GetNode<AudioStreamPlayer>("MusicStreamPlayer");
        SoundStreamPlayer = this.GetNode<AudioStreamPlayer>("SoundStreamPlayer");

        Level = this.GetNode<Node2D>("Level");
        MusicPlayer = new MusicPlayer(MusicStreamPlayer, PATH_TO_MUSIC);
        SoundPlayer = new SoundPlayer(SoundStreamPlayer, PATH_TO_SOUNDS);

        StartNewRun();
        StartNewWave();
        
        initialMusicPitchScale = MusicStreamPlayer.PitchScale;
    }
    public override void _Process(float delta)
    {
        TotalRunTime+= delta;
        TotalWaveTime+= delta;

        if (!AlarmTriggered) 
        {
            AdjustMusicAccordingToAlarmCountdown();
            if ((AlarmCountdown.TimeLeft < ALARM_COUNTDOWN_MAX_START/2) && (!AlarmHalfTImeTriggered))
            {
                // Use this to trigger shop open - only trigger once per wave
                EmitSignal(nameof(AlarmCountdownHalfTime));
                AlarmHalfTImeTriggered = true;
            }
        }
    }

    public void AdjustMoney(int adjustmentAmount)
    {
        if(adjustmentAmount > 0)
        {
            this.Money += adjustmentAmount;
            this.Score += adjustmentAmount*adjustmentAmount;
        }
        else
        {
            this.Money += adjustmentAmount;
        }
    }

    private void StartNewRun()
    {
        TotalRunTime = 0;
        CurrentWaveNumber = START_WAVE_NUMBER;
    }
    public void StartNewWave()
    {
        TotalWaveTime= 0;
        AlarmTriggered = false;
        AlarmHalfTImeTriggered = false;
        AlarmCountdown.Start(ALARM_COUNTDOWN_MAX_START);

        WaveManager.LoadWave(CurrentWaveNumber);
    }

    public void AdjustAlarmCountdown(float adjustAmount)
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
            MusicStreamPlayer.PitchScale = initialMusicPitchScale + (adjustTo / 25) * (adjustTo / 25);
        }
        else
        {
            MusicStreamPlayer.PitchScale = initialMusicPitchScale;
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
        AdjustMoney(enemy.GetPointValue()*enemy.GetPointValue());
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
    public void OnNextWavePortalEntered(Player player)
    {
        SoundPlayer.Play("enter_teleport");
        WaveManager.UnLoadWave();
        this.Player = player;
        //PackedPlayer
        // TODO 9 pack player to load on new wave load-not needed?
        CurrentWaveNumber++;
        if (CurrentWaveNumber > FINAL_WAVE_NUMBER) {
            EmitSignal(nameof(GoToGameCompletedScreen));
        }
        else
        {
            EmitSignal(nameof(GoToWaveCompletedScreen));
        }
    }
    public void OnPlayerDied(Player player)
    {
        SoundPlayer.Play("defeat");
        EmitSignal(nameof(GoToGameOverScreen));
    }
    public void OnLoadWaveCompleted()
    {
        EnemyCount = WaveManager.EnemyCount;
    }
}

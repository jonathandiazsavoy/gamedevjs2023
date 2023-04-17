using code.Items.TimeItems;
using Godot;

public class GameManager : Node2D
{
    const float WAVE_COUNTDOWN_MAX_START = 60;

    public Timer WaveCountdown { get; private set; }

    public float TotalRunTime { get; private set; }
    public float WaveClockTotal
    {
        get 
        {
            return Mathf.Abs(WaveCountdown.TimeLeft - WAVE_COUNTDOWN_MAX_START);
        }
    }

    public override void _Ready()
    {
        WaveCountdown = this.GetNode<Timer>("WaveCountdown");
        
        StartNewRun();
        StartNewLoop();
    }
    public override void _Process(float delta)
    {
        TotalRunTime += delta;

        //TODO remove this test stuff
        if (Input.IsActionJustPressed("test1"))
        {
            
        }
        if (Input.IsActionJustPressed("test2"))
        {
            
        }
    }

    private void StartNewRun()
    {
        TotalRunTime = 0;
    }
    private void StartNewLoop()
    {
        WaveCountdown.Start(WAVE_COUNTDOWN_MAX_START);
    }

    private void AdjustWaveCountdown(float adjustAmount)
    {
        WaveCountdown.Start(Mathf.Clamp(WaveCountdown.TimeLeft + adjustAmount, 0, WAVE_COUNTDOWN_MAX_START));
    }

    // Signal listeners
    public void OnCountDownModifierItemUsed(CountDownModifier countdownModifier)
    {
        AdjustWaveCountdown(countdownModifier.CountDownChange);
    }
}

using Godot;
using System;

public class GameplayUIManager : Control
{
    private GameManager gameManager;
    protected RichTextLabel TotalRunTime { get; set; }
    protected RichTextLabel LoopTimeLeft { get; set; }
    protected AnalogueClock WaveClock { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>("/root/GameManager");
        TotalRunTime = this.GetNode<RichTextLabel>("TotalRunTime");
        LoopTimeLeft = this.GetNode<RichTextLabel>("LoopTimeLeft");
        WaveClock = this.GetNode<AnalogueClock>("AnalogueClock");
    }
    public override void _Process(float delta)
    {
        TotalRunTime.Text = "Total run time: "+Math.Floor(gameManager.TotalRunTime).ToString();
        LoopTimeLeft.Text = "Time till next wave: " + Math.Ceiling(gameManager.WaveCountdown.TimeLeft).ToString();

        WaveClock.TimeUnit = gameManager.WaveClockTotal * 60;
    }
}

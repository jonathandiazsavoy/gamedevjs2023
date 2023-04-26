using Godot;

public class WaveCompletedScreen : Control
{
    public bool continuePressed = false;

    private GameManager gameManager;
    private AppendableRichTextLabel currentScore;
    private AppendableRichTextLabel waveCompletionTime;
    private Button continueButton;

    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
        currentScore = this.GetNode<AppendableRichTextLabel>("Results/MarginContainer/VBoxContainer/CurrentScore");
        waveCompletionTime = this.GetNode<AppendableRichTextLabel>("Results/MarginContainer/VBoxContainer/WaveCompletionTime");
        continueButton = this.GetNode<Button>("Advance/Button"); 

        currentScore.AppendToInitialText(gameManager.Score.ToString());
        waveCompletionTime.AppendToInitialText(((int)(gameManager.TotalWaveTime)).ToString() + " sec");
        continueButton.GrabFocus();
        continuePressed = false;

    }

    public void OnContinue()
    {
        continuePressed = true;
    }
}

using Godot;

public class WaveCompletedScreen : Control
{
    private GameManager gameManager;
    private AppendableRichTextLabel CurrentScore;
    private AppendableRichTextLabel WaveCompletionTime;

    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
        CurrentScore = this.GetNode<AppendableRichTextLabel>("Control/Results/MarginContainer/VBoxContainer/CurrentScore");
        WaveCompletionTime = this.GetNode<AppendableRichTextLabel>("Control/Results/MarginContainer/VBoxContainer/WaveCompletionTime");

        CurrentScore.AppendToInitialText(gameManager.Score.ToString());
        WaveCompletionTime.AppendToInitialText(gameManager.TotalWaveTime.ToString());
    }

}

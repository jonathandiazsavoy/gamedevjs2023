using Godot;

public class GameOverMenu : CanvasLayer
{
    [Signal]
    public delegate void GoToTitleScreen();

    public void OnRestart()
    {
        this.GetTree().ReloadCurrentScene();
        this.GetTree().Paused = false;
    }
    public void OnBackToTitle()
    {
        EmitSignal("GoToTitleScreen");
    }
}

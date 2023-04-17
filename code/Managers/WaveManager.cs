using Godot;
using System;

public class WaveManager : YSort
{
    private YSort Enemies;

    public int EnemyCount { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Enemies = this.GetNode<YSort>("Enemies");

        //Enemies.RequestReady();
        EnemyCount = Enemies.GetChildCount();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

using Godot;
using System;

public partial class HudDisplay : CanvasLayer
{
	
	public void UpdateScore(int NewScore) {
		GetNode<Label>("ScoreLabel").Text = "Health: " + NewScore.ToString();
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

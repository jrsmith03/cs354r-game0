using Godot;
using System;

public partial class World : Node2D
{
	private SignalSingleton signal;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		signal = GetNode<SignalSingleton>("/root/SignalSingleton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.Escape)) {
			GetTree().Paused = true;
			GD.Print("Pause");
			signal.EmitSignal(nameof(SignalSingleton.ShowPause)); 
		}

		if (Input.IsKeyPressed(Key.Space)) {
			GetTree().Paused = false;
			GD.Print("Resume");
			signal.EmitSignal(nameof(SignalSingleton.HidePause)); 

		}

	}
}

using Godot;
using System;

public partial class Sword : Area2D
{
	private SignalSingleton signal;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		signal = GetNode<SignalSingleton>("/root/SignalSingleton");
		this.AreaEntered += OnAreaEnter2D;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnAreaEnter2D(Node2D other) {
		signal.EmitSignal(nameof(SignalSingleton.SwordEquipped)); 
		QueueFree();
	}
}

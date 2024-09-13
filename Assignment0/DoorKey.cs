using Godot;
using System;

public partial class DoorKey : Area2D
{
	private SignalSingleton signal;

	public override void _Ready()
	{
		signal = GetNode<SignalSingleton>("/root/SignalSingleton");
		this.AreaEntered += OnAreaEnter2D;

	}

	public override void _Process(double delta)
	{
	}

	private void OnAreaEnter2D(Node2D other) {
		signal.EmitSignal(nameof(SignalSingleton.KeyEquipped)); 
		QueueFree();
	}
}

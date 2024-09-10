using Godot;
using System;

public partial class player : Area2D 
{
	int SPEED = 10;
	public int HEALTH = 3;
	SignalSingleton sig;

	public override void _Ready(){
		sig = new SignalSingleton();
	}
	
	public override void _Process(double delta){
		// Capture a normalized vector of the input
		Vector2 vec = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		this.Position += vec * SPEED;
	}
}

using Godot;
using System;

public partial class player : CharacterBody2D 
{
	int SPEED = 850;
	public int HEALTH = 3;
	SignalSingleton sig;

	public override void _Ready(){
		sig = new SignalSingleton();
	}
	
	public override void _Process(double delta){
		// Capture a normalized vector of the input
		Vector2 velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");
	
		Velocity = velocity * SPEED;
		MoveAndSlide();
	}
}

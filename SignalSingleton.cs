using Godot;
using System;
//


public partial class SignalSingleton : Sprite2D
{
	[Signal]
	public delegate void UpdateHealthEventHandler(int damage_amt);
	public static SignalSingleton Instance { get; private set; }

	public int Health { get; set; }

	public override void _Ready()
	{
		Instance = this;
	}
	
	public void DecreaseHealth(player Player, int amt) {
		Player.HEALTH -= amt;
		if (Player.HEALTH == 0) {
			// Trigger game-over state
			GD.Print("Game over!");
			
		}	
		GD.Print("Decrease health by ", amt);
		Node HUD = GetNode<Node2D>("HUD");
		GD.Print(HUD);
	
	}
}

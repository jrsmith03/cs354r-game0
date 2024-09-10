using Godot;
using System;

public partial class enemy_bullet : Area2D
{

	int DAMAGE_AMOUNT = 5;
	public int ENEMY_HEALTH = 10;
	Vector2 DELTA = new Vector2(0, 10);
	
	SignalSingleton sig;
	
	public override void _Ready(){
		sig = new SignalSingleton();
	}

	public override void _Process(double delta){
		if (this.Position[1] < GetViewportRect().Size[1]) {
			this.Position += DELTA;
		}
		
		this.AreaEntered += OnAreaEnter2D;
		
	}
	private void OnAreaEnter2D(Node2D other) {
		
		GD.Print("DEBUG: Damage dealt.");
		sig.DecreaseHealth((player)other, 1); 
	}
}

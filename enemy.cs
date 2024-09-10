using Godot;
using System;

public partial class enemy : Area2D
{
	public int ENEMY_HEALTH = 10;
	int pos = 0;
	Vector2 DELTA = new Vector2(5, 0);
	bool reverse = false;
	
	SignalSingleton sig;
	PackedScene bullet_tscn;
	
	public override void _Ready(){
		sig = new SignalSingleton();
		bullet_tscn = (PackedScene)GD.Load("res://enemy_bullet.tscn");
		GD.Print(bullet_tscn);
	}

	public override void _Process(double delta){
		if (pos == 0 || pos == 500) {
			var bill = bullet_tscn.Instantiate();
			AddChild(bill);
			
		}
		if (pos <= 0 || (!reverse && pos <= GetViewportRect().Size[0])) {
			reverse = false;
			this.Position += DELTA;
			pos+=5;
		} else {
			reverse = true;
			this.Position -= DELTA;
			pos-=5;
		}
		
		this.AreaEntered += OnAreaEnter2D;
		
	}
	private void OnAreaEnter2D(Node2D other) {
		GD.Print("DEBUG: Player attacked ememy.");
		ENEMY_HEALTH--;
		if (ENEMY_HEALTH == 0) {
			this.QueueFree();
		}
	}
}

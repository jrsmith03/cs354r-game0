using Godot;
using System;

public partial class enemy : Area2D
{
	public int ENEMY_HEALTH = 10;
	int TURNAROUND_POS = 500;
	int pos = 0;
	Vector2 DELTA = new Vector2(5, 0);
	bool reverse = false;
	
	SignalSingleton sig;
	PackedScene bullet_tscn;
	
	public override void _Ready(){
		sig = GetNode<SignalSingleton>("/root/SignalSingleton");
		bullet_tscn = (PackedScene)GD.Load("res://enemy_bullet.tscn");
		GetNode<Label>("EnemyHealthLabel").Text = ENEMY_HEALTH.ToString() + "hp";

	}

	public override void _PhysicsProcess(double delta){
		if (pos == 0 || pos == TURNAROUND_POS / 2 || pos == TURNAROUND_POS) {
			var bill = bullet_tscn.Instantiate();
			AddChild(bill);
			
		}
		//GetViewportRect().Size[0]
		if (pos <= 0 || (!reverse && pos <= TURNAROUND_POS)) {
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
	private void TakeDamage() {
		GD.Print("DAMAGE");
		ENEMY_HEALTH--;
		GetNode<Label>("EnemyHealthLabel").Text = ENEMY_HEALTH.ToString() + " hp";

		if (ENEMY_HEALTH == 0) {
			this.QueueFree();
			
			sig.EmitSignal(nameof(SignalSingleton.EnemyKilled)); 
		}
		sig.SwordAttack -= TakeDamage;

	}
	private void OnAreaEnter2D(Node2D other) {
		GD.Print("enter");
		
		sig.SwordAttack += TakeDamage;
	}
}

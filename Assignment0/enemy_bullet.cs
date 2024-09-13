using Godot;
using System;

public partial class enemy_bullet : Area2D
{

	int DAMAGE_AMOUNT = 5;
	public int ENEMY_HEALTH = 10;
	Vector2 DELTA = new Vector2(0, 10);
	
	private SignalSingleton signal;
	// private AudioStreamPlayer2D BulletShootSfx;
	
	public override void _Ready(){
		signal = GetNode<SignalSingleton>("/root/SignalSingleton");
		this.Position += 15*DELTA;
		GetNode<AudioStreamPlayer2D>("BulletShootSfx").Play();

	}

	public override void _PhysicsProcess(double delta){
		if (this.Position[1] < GetViewportRect().Size[1]) {
			this.Position += DELTA;

		}
		
		this.AreaEntered += OnAreaEnter2D;
		if (this.Position[1] > GetViewportRect().Size[1]) {
			QueueFree();
		}
	}
	private void OnAreaEnter2D(Node2D other) {
		signal.EmitSignal(nameof(SignalSingleton.UpdateHealth), 1); 
	}
}

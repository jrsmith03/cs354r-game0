using Godot;
using System;

public partial class player : CharacterBody2D 
{
	int SPEED = 650;
	int HEALTH = 3;
	private SignalSingleton signal;
	private AnimationPlayer animate;
	private Node2D sword;
	private Timer SwordDisplayTime;
	private AudioStreamPlayer2D HurtSfx;
	private AudioStreamPlayer2D AttackSfx;
	public bool has_key = false;
	public bool has_killed_enemy = false;
	bool sword_equipped = false;

	[Signal]
	public delegate void UpdatePlayerStateEventHandler(int amt);

	public override void _Ready(){
		signal = GetNode<SignalSingleton>("/root/SignalSingleton");
		animate = GetNode<AnimationPlayer>("AnimationPlayer");
		SwordDisplayTime = GetNode<Timer>("SwordDisplay");
		HurtSfx = GetNode<AudioStreamPlayer2D>("../HurtSfx");
		AttackSfx = GetNode<AudioStreamPlayer2D>("../SwordAttackSfx");

		sword = GetNode<Node2D>("Sword");
		sword.Visible = false;

		GD.Print("Signal singleton get node: " + signal);
		signal.UpdateHealth += DecreaseHealth;
		signal.KeyEquipped += SetKeyState;
		signal.SwordEquipped += EquipSword;
		signal.EnemyKilled += SetEnemyState;
	}
	
	public override void _Process(double delta){
		// Capture a normalized vector of the input
		Vector2 velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (Input.IsKeyPressed(Key.S)) {
			animate.Play("walk");
		} else if (Input.IsKeyPressed(Key.W)) {
			animate.Play("walkup");
		} else if (Input.IsKeyPressed(Key.A)) {
			animate.Play("walkleft");
		} else if (Input.IsKeyPressed(Key.D)) {
			animate.Play("walkright");
		} else {
			animate.Play("idle");
		}
	
		Velocity = velocity * SPEED;
		MoveAndSlide();

		if (Input.IsActionJustPressed("attack")) {
			GD.Print("Mouse click");
			Attack();
		}


		signal.EmitSignal(nameof(SignalSingleton.IsKeyEquipped), has_key);
		signal.EmitSignal(nameof(SignalSingleton.HasKilledEnemy), has_killed_enemy);

		SwordDisplayTime.Timeout += OnTimerTimeout;
	}
	private void SetKeyState() {
		this.has_key = true;
		signal.EmitSignal(nameof(SignalSingleton.ShowKeyEquip)); 
	}

	private void SetEnemyState() {
		this.has_killed_enemy = true;
		signal.EmitSignal(nameof(SignalSingleton.ShowEnemyKilled)); 
	}

	private void DecreaseHealth(int DamageAmount) {
		HEALTH -= DamageAmount;
		HurtSfx.Play();
		if (HEALTH <= 0) {
			// Trigger game-over state
			GD.Print("Game over!");
			signal.EmitSignal(nameof(SignalSingleton.ShowGameOver)); 
		}	
		GD.Print("Decrease health by ", DamageAmount);
		signal.EmitSignal(nameof(SignalSingleton.UpdateHud), HEALTH); 


	}
	private void EquipSword() {
		sword_equipped = true;
	}
	private void Attack() {
		AttackSfx.Play();
		SwordDisplayTime.Start();
		GD.Print(SwordDisplayTime);
		if (sword_equipped) {
			sword.Visible = true;
			signal.EmitSignal(nameof(SignalSingleton.SwordAttack)); 

		}
	}
	private void OnTimerTimeout() {
		sword.Visible = false;
	}
}

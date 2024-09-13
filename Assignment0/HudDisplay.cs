using Godot;
using System;

public partial class HudDisplay : CanvasLayer
{
	private SignalSingleton signal;
	private Node2D GameOver;

	private Node2D Pause;
	private Button Quit;

	private Sprite2D KeyObj;
	private AudioStreamPlayer2D DeathSfx;


	
	public void UpdateScore(int amt) {
		GetNode<Label>("HealthBar/HealthLabel").Text = "Health: " + amt.ToString();
		if (amt == 2) {
			GetNode<Sprite2D>("HealthBar/Health3").Visible = false;
		}
		if (amt == 1) {
			GetNode<Sprite2D>("HealthBar/Health2").Visible = false;
		}
		if (amt == 0) {
			GetNode<Sprite2D>("HealthBar/Health1").Visible = false;
			GetNode<Label>("HealthBar/HealthLabel").Text = "Dead!";

		}
	}

	public void ShowPause() {
		Pause.Visible = true;
	}
	public void HidePause() {
		Pause.Visible = false;
	}
	
	public void ShowGameOver() {	
		DeathSfx.Play();
		GameOver.Visible = true;
		GetTree().Paused = true;
	}
	public void KeyEquip() {	
		KeyObj.Visible = true;
		GetNode<Label>("Items/Tip").Text = "You found the key!";

	}
	public void ShowEnemyKilled() {	
		GetNode<Label>("Items/Enemy").Text = "You killed an enemy!";

	}
	public override void _Ready()
	{
		signal = GetNode<SignalSingleton>("/root/SignalSingleton");
		signal.UpdateHud += UpdateScore;
		GameOver = GetNode<Node2D>("GameOver");
		Pause = GetNode<Node2D>("Pause");
		Quit = GetNode<Button>("GameOver/QuitButton");
		KeyObj = GetNode<Sprite2D>("Items/Key");
		DeathSfx = GetNode<AudioStreamPlayer2D>("DeathSfx");

		GameOver.Visible = false;
		Pause.Visible = false;
		KeyObj.Visible = false;

		signal.ShowGameOver += ShowGameOver;
		signal.ShowPause += ShowPause;
		signal.HidePause += HidePause;
		signal.ShowKeyEquip += KeyEquip;
		signal.ShowEnemyKilled += ShowEnemyKilled;
	}

	public override void _Process(double delta)
	{
		if (GetTree().Paused && Input.IsKeyPressed(Key.Q)) {
			GetTree().Quit();
		}
	}



}

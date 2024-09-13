using Godot;
using System;

public partial class GoalArea : Area2D
{
	bool killed_enemy = false;
	bool is_equipped = false;
	private AudioStreamPlayer2D WinSfx;

	private SignalSingleton signal;
	private Timer Time;

	public override void _Ready()
	{
		signal = GetNode<SignalSingleton>("/root/SignalSingleton");
		this.AreaEntered += OnAreaEnter2D;
		WinSfx = GetNode<AudioStreamPlayer2D>("WinSfx");
		Time = GetNode<Timer>("ShowWin");
		GetNode<Label>("WinLabel").Visible = false;
		Time.Timeout += OnTimerTimeout;
	}

    public override void _Process(double delta)
    {
   		signal.IsKeyEquipped += SetEquipped;
		signal.HasKilledEnemy += SetKilledEnemy;
	}
    private void OnAreaEnter2D(Node2D other) {
		// I am going to signal the player to ask it for the current value
		// of the key_equipped bool



		if (is_equipped && killed_enemy) {
			Time.Start();
			GetNode<Label>("WinLabel").Visible = true;
			WinSfx.Play();

			GD.Print("GAME WON");

		} else {
			GD.Print("You don't have the Key!");
		}
	}

	private void OnTimerTimeout() {
		GetTree().Paused = true;
	}
	private void SetKilledEnemy(bool sig_killed_enemy) {
		GD.Print("Set killed enemy....", sig_killed_enemy);
		if (sig_killed_enemy) {
			killed_enemy = true;
		}
		signal.HasKilledEnemy -= SetKilledEnemy;

	}

	
	private void SetEquipped(bool sig_is_equipped) {
		GD.Print("IN set equipped.", sig_is_equipped);
		if (sig_is_equipped) {
			is_equipped = true;
		}
		signal.IsKeyEquipped -= SetEquipped;

	}

	private void TryWinGame(bool is_equipped) {
		
		signal.IsKeyEquipped -= TryWinGame;
	}
}

using Godot;
using System;
//


public partial class SignalSingleton : Sprite2D
{
	// Health Signals
	[Signal]
	public delegate void UpdateHealthEventHandler(int DamageAmount);
	[Signal]
	public delegate void UpdateHudEventHandler(int amt);

	// Gameplay signals
	[Signal]
	public delegate void KeyEquippedEventHandler();
	[Signal]
	public delegate void SwordEquippedEventHandler();
	[Signal]
	public delegate void ShowKeyEquipEventHandler();
	[Signal]
	public delegate void IsKeyEquippedEventHandler(bool equipped);
	[Signal]
	public delegate void SwordAttackEventHandler();
	[Signal]
	public delegate void EnemyKilledEventHandler();

	[Signal]
	public delegate void HasKilledEnemyEventHandler(bool killed);
	[Signal]
	public delegate void ShowEnemyKilledEventHandler();
	
	// HUD signals
	[Signal]
	public delegate void ShowGameOverEventHandler();
	[Signal]
	public delegate void ShowGameWinEventHandler();
	[Signal]
	public delegate void ShowPauseEventHandler();
	[Signal]
	public delegate void HidePauseEventHandler();

	public static SignalSingleton Instance { get; private set; }


	public override void _Ready()
	{
		Instance = this;
	}
	
	
}

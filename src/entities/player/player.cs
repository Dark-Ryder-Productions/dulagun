using Godot;
using System;

public class player : KinematicBody2D
{
	# region Constants
	// Sprite constants
	private const int ARM_RIGHT_Y_STAND = -133;
	private const int ARM_LEFT_Y_STAND = -137;
	private const int ARM_RIGHT_Y_SLIDE = 10;
	private const int ARM_LEFT_Y_SLIDE = 5;
	
	// Movement constants
	private const int JUMP_FORCE = -600;
	private const int GRAVITY = 1200;
	private const int WALL_SLIDE = 300;
	private const int SPEED = 500;
	private const int SPRINT_SPEED = 900;
	private const double IN_AIR_SPEED_MOD = 0.02;
	private const double ACCEL = 0.25;
	
	// Weapon constants
	private const string PISTOL = "pistol";
	private const string SHOTGUN = "shotgun";
	private const string UNARMED = "unarmed";
	# endregion
	
	# region Properties
	// Movement properties
	private Vector2 vel = new Vector2();
	private float prior_x_vel { get; set; }
	private float wall_jump_x_vel { get; set; }
	private bool is_wall_sliding = false;
	
	# endregion

	# region Engine Methods
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	# endregion
	# region Util Methods

	/// <summary>
	/// Gets the resource file associated with a weapon constant
	/// <param name="weapon"></param>
	/// </summary>
	private string GetWeaponRes(string weapon) {
		switch(weapon) {
			case PISTOL: return "res://entities/weapons/pistol/pistol.tscn";
			case SHOTGUN: return "res://entities/weapons/shotgun/shotgun.tscn";
			default: return "res://entities/weapons/unarmed/unarmed.tscn";
		}
	}
	
	# endregion
}

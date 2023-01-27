using Godot;
using System;

/// <summary>
/// Controls for Ella, representing the player
/// </summary>
public class player : KinematicBody2D {
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
	private const float ACCEL = 0.25F;
	
	// Weapon constants
	private const string PISTOL = "pistol";
	private const string SHOTGUN = "shotgun";
	private const string UNARMED = "unarmed";
	# endregion
	
	# region Properties
	// Animation properties
	private AnimatedSprite sprite { get; set; }

	// Movement properties
	private Vector2 vel = new Vector2();

	// Weapon properties
	private Node2D left_weapon { get; set; }
	private Node2D right_weapon { get; set; }
	
	# endregion

	# region Engine Methods
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		sprite = GetNode<AnimatedSprite>("AnimatedSprite");
	}

    /// Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta) {
		int inputXVelocity = 0;
		bool isSprinting = false;
		bool isOnFloor = IsOnFloor();

		// Handle horizontal movement inputs
		if (Input.IsActionPressed("move_left")) inputXVelocity -= 1;
		if (Input.IsActionPressed("move_right")) inputXVelocity += 1;
		if (Input.IsActionPressed("sprint")) isSprinting = true;

		// Handle vertical movement inputs
		if (Input.IsActionJustPressed("jump") && isOnFloor) {
			vel.y = JUMP_FORCE;
		}

		if (!isOnFloor) {
			vel.y += GRAVITY * delta;
		}

		// Make sure sprite is facing right direction
		HandleSpriteDirection();

		int moveSpeed = isSprinting ? SPRINT_SPEED : SPEED;
		vel.x = Mathf.Lerp(vel.x, inputXVelocity * moveSpeed, ACCEL);
		vel = MoveAndSlide(vel, Vector2.Up);
		
	}

	# endregion
	# region Util Methods

	/// <summary>
	/// Determine which direction the sprites should be facing based on cursor position
	/// </summary>
	private void HandleSpriteDirection() {
		if (GetGlobalMousePosition().x < GlobalPosition.x) {
			FlipSprites(true);
		} else {
			FlipSprites(false);
		}
	}

	/// <summary>
	/// Flip the main and connected sprites
	/// </summary>
	private void FlipSprites(bool flip) {
		sprite.FlipH = flip;

		// Add weapon handling here
	}

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

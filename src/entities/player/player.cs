using Godot;
using System;
using Dulagun.Weapons;
using Dulagun.Shared.Enums;

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

	# endregion	
	# region Properties
	// Animation properties
	private AnimatedSprite sprite { get; set; }

	// Movement properties
	private Vector2 vel = new Vector2();

	// Weapon properties
	private BaseWeapon leftWeapon { get; set; }
	private BaseWeapon rightWeapon { get; set; }
	
	# endregion

	# region Engine Methods
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		sprite = GetNode<AnimatedSprite>("AnimatedSprite");
		SwitchBothWeapons(WeaponEnum.Unarmed);
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
		if (Input.IsActionPressed("jump") && isOnFloor) {
			vel.y = JUMP_FORCE;
		}

		// Handle weapon inputs
		if (Input.IsActionJustPressed("equip_pistols")) SwitchBothWeapons(WeaponEnum.Pistol);
		if (Input.IsActionJustPressed("equip_shotguns")) SwitchBothWeapons(WeaponEnum.Shotgun);
		if (Input.IsActionJustPressed("equip_unarmed")) SwitchBothWeapons(WeaponEnum.Unarmed);

		if (Input.IsActionJustPressed("fire_left_weapon")) FireWeapon(leftWeapon);
		if (Input.IsActionJustPressed("fire_right_weapon")) FireWeapon(rightWeapon);

		// Let gravity take effect
		if (!isOnFloor) {
			vel.y += GRAVITY * delta;
		}

		// Make sure sprite is facing right direction
		HandleSpriteDirection();

		// Compute velocity and apply
		int moveSpeed = isSprinting ? SPRINT_SPEED : SPEED;
		vel.x = Mathf.Lerp(vel.x, inputXVelocity * moveSpeed, ACCEL);
		vel = MoveAndSlide(vel, Vector2.Up);
		
	}

	# endregion
	# region Animation Methods

	/// <summary>
	/// Determine which direction the sprites should be facing based on cursor position
	/// </summary>
	private void HandleSpriteDirection() {
		if (GetGlobalMousePosition().x < GlobalPosition.x) {
			FlipSprites(true, false);
			SetArmZIndex(1, -1);
		} else {
			FlipSprites(false, true);
			SetArmZIndex(-1, 1);
		}
	}

	/// <summary>
	/// Flip the main and connected sprites
	/// </summary>
	private void FlipSprites(bool flip, bool facingRight) {
		sprite.FlipH = flip;

		if (leftWeapon != null) {
			AnimatedSprite leftSprite = leftWeapon.GetNode<AnimatedSprite>("AnimatedSprite");
			leftSprite.FlipV = flip;
			leftSprite.Play(facingRight ? BaseWeapon.BACK_IDLE_ANIM : BaseWeapon.FRONT_IDLE_ANIM);
		}

		if (rightWeapon != null) {
			AnimatedSprite rightSprite = rightWeapon.GetNode<AnimatedSprite>("AnimatedSprite");
			rightSprite.FlipV = flip;
			rightSprite.Play(facingRight ? BaseWeapon.FRONT_IDLE_ANIM : BaseWeapon.BACK_IDLE_ANIM);
		}
	}

	/// <summary>
	/// Assign Z-Index values to either arm 
	/// </summary>
	private void SetArmZIndex(int leftIndex, int rightIndex) {
		GetNode<Node2D>("AnimatedSprite/ArmLeft").ZIndex = leftIndex;
		GetNode<Node2D>("AnimatedSprite/ArmRight").ZIndex = rightIndex;
	}

	# endregion
	# region Weapon Handling

	/// <summary>
    /// Handles firing a given weapon
    /// </summary>
	private async void FireWeapon(BaseWeapon weapon) {
		if (weapon == null || (weapon.GetWeaponEnum().GetWeaponType() != WeaponTypeEnum.AutoGun && weapon.isFiring)) {
			return;
		}

		weapon.isFiring = true;
		weapon.Fire();
		// If a weapon has a fire rate, we want to wait for it to cooldown before declaring the weapon is not firing
		if (weapon.fireRate != 0) {
			await ToSignal(GetTree().CreateTimer(weapon.fireRate), "timeout");
		}

		if (IsInstanceValid(weapon)) weapon.isFiring = false;
	}

	/// <summary>
	/// Switches 1 given hand to use 1 given weapon
	/// </summary>
	private void SwitchSingleWeapon(WeaponEnum weapon, bool isLeft) {
		UnequipWeapon(isLeft);
		EquipWeapon(weapon, isLeft);
	}

	/// <summary>
	/// Switches both hands to use 1 given weapon
	/// </summary>
	private void SwitchBothWeapons(WeaponEnum weapon) {
		SwitchSingleWeapon(weapon, true);
		SwitchSingleWeapon(weapon, false);
	}

	/// <summary>
	/// Adds a given weapon to the provided hand
	/// </summary>
	private void EquipWeapon(WeaponEnum weapon, bool isLeft) {
		// Exit out if we are trying to equip the same weapon
		if ((isLeft && leftWeapon?.GetWeaponEnum() == weapon) || (!isLeft && rightWeapon?.GetWeaponEnum() == weapon)) {
			return;
		}

		PackedScene scene = GD.Load<PackedScene>(weapon.GetResourcePath());
		BaseWeapon weaponInstance = scene.Instance() as BaseWeapon;

		// Set the isLeft value then attach the weapon scene to the corresponding arm point
		weaponInstance.isLeft = isLeft;
		GetNode<Node2D>("AnimatedSprite/" + (isLeft ? "ArmLeft" : "ArmRight")).AddChild(weaponInstance);
		if (isLeft) {
			leftWeapon = weaponInstance;
		} else {
			rightWeapon = weaponInstance;
		}
	}

	/// <summary>
	/// Removes the weapon from the provided hand
	/// </summary>
	private void UnequipWeapon(bool isLeft) {
		// Exit out if we are trying to unequip a non-existant weapon
		if ((isLeft && leftWeapon == null) || (!isLeft && rightWeapon == null)) {
			return;
		}

		if (isLeft) {
			leftWeapon.QueueFree();
			leftWeapon = null;
		} else {
			rightWeapon.QueueFree();
			rightWeapon = null;
		}
	}	
	
	# endregion
}
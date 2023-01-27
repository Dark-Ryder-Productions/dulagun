extends CanvasLayer

func _ready():
	pass 

###
# Launch the testing area scene
###
func launchTestingArea():
	get_tree().change_scene("res://testing/debug-levels/test001.tscn")

using UnityEngine;
using System.Collections;

public class ActBasics
{
	private static Act CreateAct (string actName)
	{
		GameObject newObject = new GameObject ();
		if (actName != "")
			newObject.name = actName;

		switch (actName) {
		case "hide":
			// Expects 0 arguments
			return newObject.AddComponent<ActHide> ();

		case "new_chat":
			// Expects 4 arguments
			return newObject.AddComponent<ActNewChat> ();

		case "destroy_chat":
			// Expects 2 arguments
			return newObject.AddComponent<ActDestroyChat> ();

		case "play_music":
			// Expects 1 argument
			return newObject.AddComponent<ActPlayMusic> ();

		case "play_sound_delay":
			// Expects 2 arguments
			return newObject.AddComponent<ActPlaySoundDelay> ();

		case "play_sound":
			// Expects 1 argument
			return newObject.AddComponent<ActPlaySound> ();

		case "spawn_north":
			// Expects 1 argument
			return newObject.AddComponent<ActSpawnNorth> ();

		case "spawn_south":
			// Expects 1 argument
			return newObject.AddComponent<ActSpawnSouth> ();

		case "spawn":
			// Expects 1 argument
			return newObject.AddComponent<ActSpawnLocal> ();

		case "animate_object":////////////////////////////////////////////////////////////////
			// Expects a variable number of arguments (always an even number)
			return newObject.AddComponent<ActAnimate> ();

		case "animate_type":////////////////////////////////////////////////////////////////
			// Expects 2 argument
			return newObject.AddComponent<ActAnimate> ();

		case "animate":
			// Expects 1 argument
			return newObject.AddComponent<ActAnimate> ();

		case "animate_player":
			// Expects 1 argument
			return newObject.AddComponent<ActAnimatePlayer> ();

		case "animate_other":
			// Expects 1 argument
			return newObject.AddComponent<ActAnimateOther> ();

		case "new_object":
			// Expects a variable number of arguments (always an odd number)
			return newObject.AddComponent<ActNewObject> ();

		case "activate_object":
			// Expects a variable number of arguments (always an odd number)
			return newObject.AddComponent<ActActivateObject> ();

		case "deactivate":
			// Expects no argument
			return newObject.AddComponent<ActDeactivateSelf> ();

		case "activate_type":
			// Expects 1 argument
			return newObject.AddComponent<ActActivateAll> ();

		case "destroy_object":
			// Expects a variable number of arguments (always an odd number)
			return newObject.AddComponent<ActDestroyObject> ();

		case "destroy_type":
			// Expects 1 argument
			return newObject.AddComponent<ActDestroyAll> ();

		case "camera_player":
			// Expects no argument
			return newObject.AddComponent<ActCameraFollowPlayer> ();

		case "camera_move":
			// Expects 2 arguments
			return newObject.AddComponent<ActCameraMove> ();
	
		case "camera_zoom":
			// Expects 2 arguments
			return newObject.AddComponent<ActCameraZoom> ();

		case "report":
			// Expects 1 argument
			return newObject.AddComponent<ActReport> ();

		case "delay":
			// Expects 1 argument
			return newObject.AddComponent<ActDelay> ();

		case "grid_delay":
			// Expects 1 argument
			return newObject.AddComponent<ActGridDelay> ();

		case "new_grid":
			// Expects 1 argument
			return newObject.AddComponent<ActLoadGrid> ();

		case "new_dialog":
			// Expects 1 argument
			return newObject.AddComponent<ActCreateDialog> ();

		case "swipe_input":
			// Expects 1 argument
			return newObject.AddComponent<ActSwipeInput> ();

		case "turn_filter":
			// Expects 2 arguments
			return newObject.AddComponent<ActTurnFilter> ();

		case "interact_and_move":
			// Expects 1 argument
			return newObject.AddComponent<ActInteractAndMove> ();

		case "interact_or_move":
			// Expects 1 argument
			return newObject.AddComponent<ActInteractOrMove> ();

		case "move_adjacent":
			// Expects 1 argument
			return newObject.AddComponent<ActMoveAdjacent> ();
		// Player: swipe_input~1 move_or_interact~2 move_adjacent~1 0.2
		// Player: swipe_input~1 action_queue~2 interact_adjacent~0 move_adjacent~1 0.2

		case "action_queue":
			// Expects a variable number of arguments
			return newObject.AddComponent<ActArray> ();

		case "spawn_adjacent":
			// Expects 1 argument
			return newObject.AddComponent<ActSpawnAdjacent> ();

		case "spawn_adjacent_conditional":
			// Expects 3 arguments
			return newObject.AddComponent<ActSpawnAdjacentConditional> ();
		// Fire: spawn_adjacent_conditional~3 32 32 Fire spawn_adjacent~1 Warning_Combustible spawn_adjacent~1 Fire

		case "self_destruct":
			// Expects no argument
			return newObject.AddComponent<ActSelfDestruct> ();

		case "self_destruct_adjacent_conditional":
			// Expects 2 arguments
			return newObject.AddComponent<ActSelfDestructAdjacentConditional> ();
		// Warning: self_destruct~0
		// Warning_Combustible: self_destruct_adjacent_conditional~2 2 37 self_destruct~0

		case "shortest_path":
			// Expects 1 argument
			return newObject.AddComponent<ActShortestPath> ();
		// Enemy: shortest_path~1 move_or_interact~2 move_adjacent~1 0.2

		case "type_choice":
			// Expects a variable number of arguments (always a multiple of 3)
			return newObject.AddComponent<ActObjectPicker> ();
		// Player (interact): type_choice~3 32 -1 move_adjacent~1 0.2

		case "interact_adjacent":
			// Expects 1 argument (bool: stop all acts if succesful?)
			return newObject.AddComponent<ActInteractAdjacent> ();

		case "find_adjacent":
			// Expects 1 argument
			return newObject.AddComponent<ActFindAdjacent> ();

		case "move_other":
			// Expects 1 argument
			return newObject.AddComponent<ActMoveOther> ();

		case "neighbour_internal_interact":
			// Expects no argument
			return newObject.AddComponent<ActNeighbourInternalInteract> ();

		case "all_neighbour_internal_interact":
			// Expects no argument
			return newObject.AddComponent<ActAllNeighbourInternalInteract> ();

		case "set_direction":
			// Expects 1 argument
			return newObject.AddComponent<ActSetDirection> ();

		case "set_direction_other":
			// Expects 1 argument
			return newObject.AddComponent<ActSetDirectionOther> ();

		default:
			GameObject.Destroy (newObject);
			return null;
		}
	}

	public static string ToString (string[] Arguments)
	{
		string result = "";

		if (Arguments != null)
			for (int i = 0; i < Arguments.Length; i++) {
				result += Arguments [i];
				if (i < Arguments.Length - 1)
					result += " ";
			}
		
		return result;
	}

	public static Act ReadActArgument (string[] Commands, int index)
	{
		if (Commands != null && Commands.Length > index) {
			string[] Argument = SplitArgument (Commands [index]);

			if (Argument != null && Argument.Length > 0) {
				Act newAct = CreateAct (Argument [0]);

				if (newAct != null) {
					newAct.arguments = GetArguments (Commands, index);
					newAct.ReadArguments ();
				}

				return newAct;
			}
		}
		return null;
	}

	public static string[] SplitIntoArguments (string Commands)
	{
		char[] splitter = { ' ' };
		return Commands.Split (splitter, System.StringSplitOptions.RemoveEmptyEntries);
	}

	public static string[] SplitArgument (string Argument)
	{
		char[] splitter = { '~' };
		return Argument.Split (splitter, System.StringSplitOptions.RemoveEmptyEntries);
	}

	public static int CountArguments (string[] Commands, int fromIndex)
	{
		if (Commands != null && Commands.Length > fromIndex) {
			string[] Argument = SplitArgument (Commands [fromIndex]);
			fromIndex++;

			int argumentCounter = 0;

			int Counter = 0;
			if (Argument != null && Argument.Length > 1)
				Counter += int.Parse (Argument [1]);
				
			while (Counter > 0 && fromIndex < Commands.Length) {
				Argument = SplitArgument (Commands [fromIndex]);
				fromIndex++;

				argumentCounter++;

				Counter--;
				if (Argument != null && Argument.Length > 1)
					Counter += int.Parse (Argument [1]);
			}
			return argumentCounter;
		} else
			return 0;
	}

	public static string[] GetArguments (string[] Commands, int index)
	{
		int ArgumentCount = CountArguments (Commands, index);
		string[] Arguments = new string[ArgumentCount];
		for (int i = index; i < index + ArgumentCount; i++)
			Arguments [i - index] = Commands [i + 1];
		return Arguments;
	}

	public static string[] GetFullArguments (string[] Commands, int index)
	{
		if (Commands != null && Commands.Length > index) {
			int ArgumentCount = CountArguments (Commands, index);

			string[] Arguments = new string[ArgumentCount + 1];
			for (int i = index; i < index + ArgumentCount + 1; i++)
				Arguments [i - index] = Commands [i];
			return Arguments;
		} else
			return null;
	}
}

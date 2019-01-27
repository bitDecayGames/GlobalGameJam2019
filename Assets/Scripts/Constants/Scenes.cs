using System;
public class Scenes
{
	public const string DontDestroyOnLoad = "DontDestroyOnLoad";
	public const string TitleScreen = "TitleScreen";
	public const string Credits = "Credits";
	public const string BitdecaySplash = "BitdecaySplash";
	public const string GameJamSplash = "GameJamSplash";
	public const string DebugJiggle = "DebugJiggle";
	public const string DebugDash = "DebugDash";
	public const string DebugDoor = "DebugDoor";
	public const string Logan = "Logan";
	public const string DebugPlayerMovement = "DebugPlayerMovement";
	public enum SceneEnum
	{
		TitleScreen = 98,
		Credits = 206,
		BitdecaySplash = 144,
		GameJamSplash = 253,
		DebugJiggle = 57,
		DebugDash = 103,
		DebugDoor = 123,
		Logan = 241,
		DebugPlayerMovement = 159,
	}
	public static string GetSceneNameFromEnum(SceneEnum sceneEnum)
	{
		switch (sceneEnum)
		{
			case SceneEnum.TitleScreen:
				return TitleScreen;
			case SceneEnum.Credits:
				return Credits;
			case SceneEnum.BitdecaySplash:
				return BitdecaySplash;
			case SceneEnum.GameJamSplash:
				return GameJamSplash;
			case SceneEnum.DebugJiggle:
				return DebugJiggle;
			case SceneEnum.DebugDash:
				return DebugDash;
			case SceneEnum.DebugDoor:
				return DebugDoor;
			case SceneEnum.Logan:
				return Logan;
			case SceneEnum.DebugPlayerMovement:
				return DebugPlayerMovement;
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}

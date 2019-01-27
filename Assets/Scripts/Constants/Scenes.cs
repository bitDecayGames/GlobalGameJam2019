using System;
public class Scenes
{
	public const string DontDestroyOnLoad = "DontDestroyOnLoad";
	public const string Logan = "Logan";
	public const string Credits = "Credits";
	public const string TitleScreen = "TitleScreen";
	public const string DefenderLoses = "DefenderLoses";
	public const string DefenderWins = "DefenderWins";
	public const string BitdecaySplash = "BitdecaySplash";
	public const string GameJamSplash = "GameJamSplash";
	public enum SceneEnum
	{
		Logan = 241,
		Credits = 206,
		TitleScreen = 98,
		DefenderLoses = 35,
		DefenderWins = 190,
		BitdecaySplash = 144,
		GameJamSplash = 253,
	}
	public static string GetSceneNameFromEnum(SceneEnum sceneEnum)
	{
		switch (sceneEnum)
		{
			case SceneEnum.Logan:
				return Logan;
			case SceneEnum.Credits:
				return Credits;
			case SceneEnum.TitleScreen:
				return TitleScreen;
			case SceneEnum.DefenderLoses:
				return DefenderLoses;
			case SceneEnum.DefenderWins:
				return DefenderWins;
			case SceneEnum.BitdecaySplash:
				return BitdecaySplash;
			case SceneEnum.GameJamSplash:
				return GameJamSplash;
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}

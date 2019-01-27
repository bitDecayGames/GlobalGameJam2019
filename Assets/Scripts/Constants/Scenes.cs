using System;
public class Scenes
{
	public const string DontDestroyOnLoad = "DontDestroyOnLoad";
	public const string BitdecaySplash = "BitdecaySplash";
	public const string GameJamSplash = "GameJamSplash";
	public const string TitleScreen = "TitleScreen";
	public const string Logan = "Logan";
	public const string Credits = "Credits";
	public const string DefenderLoses = "DefenderLoses";
	public const string DefenderWins = "DefenderWins";
	public enum SceneEnum
	{
		BitdecaySplash = 144,
		GameJamSplash = 253,
		TitleScreen = 98,
		Logan = 241,
		Credits = 206,
		DefenderLoses = 35,
		DefenderWins = 190,
	}
	public static string GetSceneNameFromEnum(SceneEnum sceneEnum)
	{
		switch (sceneEnum)
		{
			case SceneEnum.BitdecaySplash:
				return BitdecaySplash;
			case SceneEnum.GameJamSplash:
				return GameJamSplash;
			case SceneEnum.TitleScreen:
				return TitleScreen;
			case SceneEnum.Logan:
				return Logan;
			case SceneEnum.Credits:
				return Credits;
			case SceneEnum.DefenderLoses:
				return DefenderLoses;
			case SceneEnum.DefenderWins:
				return DefenderWins;
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}

using System;
public class Scenes
{
	public const string DontDestroyOnLoad = "DontDestroyOnLoad";
	public const string Logan = "Logan";
	public const string Credits = "Credits";
	public const string TitleScreen = "TitleScreen";
	public enum SceneEnum
	{
		Logan = 241,
		Credits = 206,
		TitleScreen = 98,
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
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}

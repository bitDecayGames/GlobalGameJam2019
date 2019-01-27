using System;
public class Scenes
{
	public const string DontDestroyOnLoad = "DontDestroyOnLoad";
	public const string Logan = "Logan";
	public const string Credits = "Credits";
	public enum SceneEnum
	{
		Logan = 241,
		Credits = 206,
	}
	public static string GetSceneNameFromEnum(SceneEnum sceneEnum)
	{
		switch (sceneEnum)
		{
			case SceneEnum.Logan:
				return Logan;
			case SceneEnum.Credits:
				return Credits;
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}

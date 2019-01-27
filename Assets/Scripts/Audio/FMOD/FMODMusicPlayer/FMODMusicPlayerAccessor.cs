using System;
using FMOD;
using FMOD.Studio;

public partial class FMODMusicPlayer
{
	public void PlaySongIfNoneAreCurrentlyPlaying(Songs.SongName songName)
	{
		PLAYBACK_STATE playbackState = GetAndUpdatePlaybackStateOfSong();
		if (playbackState == PLAYBACK_STATE.STOPPED)
		{
			SetSong(songName);
			SetPlaybackState(FMODSongState.Play);	
		}
	}
	
	public void SetParameter(String parameter, float parameterValue)
	{
		ParameterInstance parameterInstance;
		RESULT result = _eventInstance.getParameter(parameter, out parameterInstance);
		if (result != RESULT.OK)
		{
			throw new Exception(string.Format("Error returned when GETTING parameter of FMOD EventInstance: {0}", result));
		}
		result = _eventInstance.setParameterValue(parameter, parameterValue);
		if (result != RESULT.OK)
		{
			throw new Exception(string.Format("Error returned when SETTING parameter of FMOD EventInstance: {0}", result));
		}
	}
}

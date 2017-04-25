using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using UnityEngine;

/// <summary>
/// Jacob Quirk, April 22, 2017
/// This file is based from, http://amalgamatelabs.com/Blog/2016/Data_Persistence/
/// The basic idea is that you save your game normaly but check what the platform is before you save
/// If its webgl, then we also have to call some dllimports and make sure that is syncs the files
/// </summary>


[Serializable]
public class SaveHighScore
{
	/// <summary>
	/// The name of the player who got the high score of the current game
	/// </summary>
	public string HighScoreName = "";

	public double OceanScore = 0;

	public double SkyScore = 0;

	public double SpaceScore = 0;

	/// <summary>
	/// The total score that the players gets in all the levels combined
	/// </summary>
	public double TotalScore = 0;
}

public class DataSaving
{
	[DllImport("__Internal")]
	static extern void SyncFiles();

	[DllImport("__Internal")]
	static extern void WindowAlert(string message);

	/// <summary>
	/// Async saves a high score the players computer
	/// </summary>
	/// <param name="hs"></param>
	public void Save(SaveHighScore hs)
	{
		var ppath = Application.persistentDataPath;
		BackgroundWorker worker = new BackgroundWorker();
		worker.DoWork += (sender, args) =>
		{
			string data = string.Format("{0}/GameData.dat", ppath);
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream fileSteam;

			try
			{
				if(File.Exists(data))
				{
					File.WriteAllText(data, string.Empty);
					fileSteam = File.Open(data, System.IO.FileMode.Open);
				}
				else
				{
					fileSteam = File.Create(data);
				}

				binaryFormatter.Serialize(fileSteam, hs);
				fileSteam.Close();

				if(Application.platform == RuntimePlatform.WebGLPlayer)
				{
					SyncFiles();
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		};
		worker.RunWorkerAsync();
	}

	/// <summary>
	/// Will reset all the scores in the location to 0 if you call this with true
	/// </summary>
	public void DeleteScores(bool actuallyDothis = false)
	{
		SaveHighScore s = new SaveHighScore();
		s.SkyScore = 0;
		s.SpaceScore = 0;
		s.HighScoreName = "";
		s.OceanScore = 0;
		s.TotalScore = 0;
		if(actuallyDothis)
		{
			Save(s);
			//Debug.Log("Deleted all saves! I hope you meant to do this!");
		}
	}

	/// <summary>
	/// Returns the saved data from the unity path
	/// </summary>
	/// <returns></returns>
	public SaveHighScore Load()
	{
		var ppath = Application.persistentDataPath;
		SaveHighScore gameDetails = null;
		string dataPath = string.Format("{0}/GameData.dat", ppath);

		try
		{
			if(File.Exists(dataPath))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				FileStream fileStream = File.Open(dataPath, System.IO.FileMode.Open);

				gameDetails = (SaveHighScore) binaryFormatter.Deserialize(fileStream);
				fileStream.Close();
			}
		}
		catch(Exception e)
		{
			PlatformSafeMessage("Failed to Load: " + e.Message);
		}

		return gameDetails;
	}

	static void PlatformSafeMessage(string msg)
	{
		if(Application.platform == RuntimePlatform.WebGLPlayer)
		{
			WindowAlert(msg);
		}
		else
		{
			Debug.Log(msg);
		}
	}

}
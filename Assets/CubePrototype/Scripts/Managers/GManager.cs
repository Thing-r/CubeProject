using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : Singleton<GManager>
{

    public bool waitForFlip = true;


    [SerializeField]
	protected string m_PlayerName;

	public string GetPlayerName ()
	{
		return m_PlayerName;
	}

	public Transform nextSpawnPoint;
	public bool changeXDirection = false;
	
}

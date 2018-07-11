using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenu : MonoBehaviour 
{
	[SerializeField]
	private GameObject LiuKang;

	[SerializeField]
	private GameObject Scorpion;
	
	[SerializeField]
	private GameObject SubZero;

	[SerializeField]
	private GameObject ConfirmButton;

	public void Start()
	{
		ConfirmButton.SetActive(false);
		LiuKang.SetActive(false);
		Scorpion.SetActive(false);
		SubZero.SetActive(false);
	}

	public void ChooseYourCharacter(string aName)
	{
		if(aName == "LiuKang")
		{
			Scorpion.SetActive(false);
			SubZero.SetActive(false);
			LiuKang.SetActive(true);
			ConfirmButton.SetActive(true);
		}
		else if(aName == "Scorpion")
		{
			LiuKang.SetActive(false);
			SubZero.SetActive(false);
			Scorpion.SetActive(true);
			ConfirmButton.SetActive(true);
		}
		else if(aName == "SubZero")
		{
			LiuKang.SetActive(false);
			Scorpion.SetActive(false);
			SubZero.SetActive(true);
			ConfirmButton.SetActive(true);
		}
		GameManager.Instance.SetPlayerName(aName);
	}
	
	/*public void SetActiveParasite()
	{
		Hulk.SetActive(false);
		Parasite.SetActive(true);
		Skeleton.SetActive(false);
		GameManager.Instance.SetPlayerCharacter(1, 2);
		LevelManager.Instance.ChangeLevel("Menu p2");
	}

	public void SetActiveSkeleton()
	{
		Hulk.SetActive(false);
		Parasite.SetActive(false);
		Skeleton.SetActive(true);
		GameManager.Instance.SetPlayerCharacter(1, 3);
		LevelManager.Instance.ChangeLevel("Menu p2");
	}*/
}

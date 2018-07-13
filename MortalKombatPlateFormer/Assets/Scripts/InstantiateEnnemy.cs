using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnnemy : MonoBehaviour 
{
	public List<GameObject> m_Ennemy;
	public List<Transform> m_SpawnTransform;

	private void OnTriggerEnter2D(Collider2D aOther)
	{
		if(aOther.tag == "Player")
		{
			Debug.Log("Instantiate Ennemy");
			for(int i = 0; i < m_Ennemy.Count; i++)
			{		
				Instantiate(m_Ennemy[i], m_SpawnTransform[i].position, Quaternion.identity);
			}
			Destroy(gameObject);
		}

		//if(aOther.gameObject.layer == LayerMask.NameToLayer("Player"))
		//{}
		
	}
}

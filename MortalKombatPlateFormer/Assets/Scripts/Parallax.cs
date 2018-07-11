using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//L'avantage de la struct c'est que nous pouvons l'utiliser comme une variable "multiple". (Donc une variable qui contient plusieurs variables)
[System.Serializable]
public struct Background
{
	public float m_LayerDistance;
	public GameObject m_Visual;
}

public class Parallax : MonoBehaviour 
{
	[SerializeField]
	private Transform m_Camera;
	[SerializeField]
	private float m_Smoothing = 1f;
	[SerializeField]
	private List<Background> m_Backgrounds = new List<Background>();

	private Vector3 m_PreviousCamPos = new Vector3();
	private Vector3 m_BackgroundTargetPos = new Vector3();

	private void Start()
	{
		//On stock la position initial de la caméra.
		m_PreviousCamPos = m_Camera.position;
	}

	private	void LateUpdate()
	{
		float ParallaxValue;
		Background background;

		//On itère dans tout les backgrounds pour les déplacer selon leur distance relative.
		for (int i = 0; i < m_Backgrounds.Count; i++)
		{
			//Içi on stock notre background à l'index i, pour faciliter la réutilisation.
			background = m_Backgrounds[i];

			ParallaxValue = (m_PreviousCamPos.x - m_Camera.position.x) * -background.m_LayerDistance;

			//Içi on initialise notre vecteur avec la position courante de notre visual.
			m_BackgroundTargetPos = background.m_Visual.transform.position;

			//Içi on modifie la valeur de 'X' de notre background.
			m_BackgroundTargetPos.x += ParallaxValue;

			//Içi on interpole linéairement la position actuelle versus la position voulue pour créer notre effet parallaxe.
			background.m_Visual.transform.position = Vector3.Lerp(background.m_Visual.transform.position, m_BackgroundTargetPos, m_Smoothing * Time.deltaTime);
		}

		m_PreviousCamPos = m_Camera.position;
	}
}

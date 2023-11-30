using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SugarMeterScript : MonoBehaviour {

	//Variable que indica donde se mostrarán los puntos
	private TextMeshProUGUI sugarMeter;
	//Variable que guarda el número de puntos
	private int sugarScore = 100;

	private int _sugarScore
	{
		set
		{
			sugarScore = Mathf.Clamp(value, 0, 999);
		}
		get
		{
			return sugarScore;
		}
	}
	
	public int getSugarScore(){
		return sugarScore;
	}


	// Use this for initialization
	void Start () {
        sugarMeter = GetComponent<TextMeshProUGUI>();
        UpdateSugarMeter();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void AddSugar(int sugar){
		
		sugarScore += sugar;
		
		if (sugarScore<0)
		{
			sugarScore=0;
		}
		
		UpdateSugarMeter();
		
	}
	
	void UpdateSugarMeter(){
		sugarMeter.text = sugarScore.ToString();
	}
}

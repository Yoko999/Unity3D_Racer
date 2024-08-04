using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary // лучше в отдельный скрипт вынести
{
	//границы дороги
	public float xMin=1,xMax=13;//границы по ширине
	public float roadMinLen=0,roadMaxLen=1450;//границы позиции дороги по длине
	//границы по скорости автомобилей
	public float vMin = 0,vMax=100;
}

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {
	
	public float speed;//скорость игрока
	public Vector3 movement;
	private GameObject avto;
	private GameObject cam;
	public int scores;//очки за дистанцию
	private const int perDist = 50;//за какую дистанцию увеличиваются очки
	public GUIText ScoreGUI;
	public GUIText SpeedGUI;
	private const float speedIncrease=1;//на сколько увеличивается скорость
	public int bonus;//бонусы

	public Boundary bound;

	void Start(){
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		avto = GameObject.FindGameObjectWithTag ("Player");
		bound.xMin = 1;bound.xMax = 13;
		scores = 0;
		speed = 0;
		bonus = 0;
	}

	void OnTriggerStay(Collider col){
		if (col.tag == "Bonus") {
			++bonus;
			Destroy(col.gameObject);
		}
		if (col.tag == "Enemy") {
			if (speed >= bound.vMin + 10)
				speed = speed - 1;
			else
				speed=0;
		}
	}
	
	void Update () {
		avto.transform.rotation = Quaternion.Euler(0, 0, 0);//пока чтобы не крутился при столкновении
		movement = new Vector3 (//текущие координаты движения
			Input.GetAxis("Horizontal"),
			0.0f, 
			Input.GetAxis ("Vertical"));

		if (avto.transform.localPosition.z <= bound.roadMaxLen) {
			float mx = movement.x, mz = movement.z;

			//чтобы по бокам не выходил за пределы дороги 
			if (avto.transform.localPosition.x > bound.xMin && avto.transform.localPosition.x < bound.xMax)
				mx = movement.x;
			else {
				if (avto.transform.localPosition.x <= bound.xMin)
					mx = movement.x + 1;
				else
				if (avto.transform.localPosition.x >= bound.xMax)
					mx = movement.x - 1;
			}

			//увеличение/уменьшение скорости при нажатии стрелок
			if (Input.GetKey (KeyCode.UpArrow)) {
				if (speed <= bound.vMax)
					speed += speedIncrease;
				else
					speed = bound.vMax;
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				if (speed >= bound.vMin)
					speed -= speedIncrease;
				else
					speed = bound.vMin;
			}
			mz += speed * Time.deltaTime;

			avto.transform.Translate (new Vector3 (mx, movement.y, mz));//передвижение

		} 
		//-----в отдельную функцию------
		//подсчёт очков: +1 за каждые 50 м
		if(avto.transform.position.z/perDist>=scores+1)
			++scores;
		ScoreGUI.text = "Distance: " + scores+"\nBonus: "+bonus;
		SpeedGUI.text = "Speed: "+speed;
	}



}

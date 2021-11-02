using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderRunTo1 : MonoBehaviour
{
 
     public bool b=true;
	 public Slider slider;
	 public float speed=0.5f;

	 public GameObject parent;

	  float time =0f;
	  float timer = 1f;
  
	  void Start()
	  {
	  
		slider = gameObject.GetComponent<Slider>();
	  }
  
		void Update()
		{

			if(slider != null)
			{
				if (b)
				{
					time += Time.deltaTime * speed;
					slider.value = time;

					if (time > 1)
					{
						b = false;
						time = 0;
					}
				}

			}
			else
			{
				slider = gameObject.GetComponent<Slider>();
			}

			if(!b)
			{
				timer -= Time.deltaTime;
				if(timer < 0)
				{ 
					b = true;
					timer = 1f;
				}
			}
		}
	
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderRunTo1 : MonoBehaviour
{
 
     public bool b=true;
	 public Slider slider;
	 public float speed=0.5f;

	  float time =0f;
  
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
		}
	
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponIconManager : MonoBehaviour
{
	public Image gun; 
	public Text gun_i;

	public Image instr;
	public Text instr_i;

    // Start is called before the first frame update
    void Start()
    {
        instr.enabled = false;   
        instr_i.enabled = false;   


    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChooseGun(){
    	instr.enabled = false;
    	instr_i.enabled = false;   

    	gun.enabled = true;
    	gun_i.enabled = true;   

    }

    public void ChooseInstr(){
    	instr.enabled = true;
    	instr_i.enabled = true;   

    	gun.enabled = false;
    	gun_i.enabled = false;   

    }

}

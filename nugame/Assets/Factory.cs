using UnityEngine;
using System.Collections;
using System;
using System.Runtime;

public class Factory : MonoBehaviour {

	public string bits;

	/*void Start()
	{
		long it = 3909;

		string hart = getBit (it);
		int hurt = getNum (hart);

		constructItem (it);
	}*/

	// Use this for item construction
	public /*GameObject*/void constructItem(long id)
	{
		string item_bit = getBit (id); //get bits in string rep

		string btyp, bpre, bsuf, bfla, bmesh; //bit rep of features

		string type, prefix, suffix; //string rep of features

		int ntyp, npre, nsuf, nfla, nmesh; //numerical rep of features

		string getbits; //variable for storing bits
		int numrep; //variable for storing ints

		//set bit reps
		btyp = item_bit.Substring (0, 3);
		bpre = item_bit.Substring (3, 8);
		bsuf = item_bit.Substring (11, 8);
		bfla = item_bit.Substring (19, 13);
		bmesh = item_bit.Substring (32, 32);

		//set numerical reps
		ntyp = getNum (btyp);
		npre = getNum (bpre);
		nsuf = getNum (bsuf);
		nfla = getNum (bfla);
		nmesh = getNum (bmesh);


		//RANGED WEAPON
		if (ntyp == 0) {
			type = "Ranged Weapon";
			Debug.Log (type);

			string subtype;
			bool bossdrop, silencer, lasersight, flashlight;
			int barrel, body, stock, scope_rails, barrelmods;

			getbits = item_bit.Substring (19, 1); //bossdrop
			if (getbits == "0")
				bossdrop = true;
			else
				bossdrop = false;

			getbits = item_bit.Substring (20, 2); //subtype
			if (getbits == "00")
				subtype = "Hand Gun";
			else if (getbits == "01")
				subtype = "Rifle/Shot Gun";
			else if (getbits == "10")
				subtype = "Heavy Weapon";
			else
				subtype = "Shoulder Mount";

			getbits = item_bit.Substring (31, 1); //silencer
			if (getbits == "0")
				silencer = true;
			else
				silencer = false;

			getbits = item_bit.Substring (30, 1); //lasersight
			if (getbits == "0")
				lasersight = true;
			else
				lasersight = false;

			getbits = item_bit.Substring (29, 1); //flashlight
			if (getbits == "0")
				flashlight = true;
			else
				flashlight = false;

			barrel = getNum (item_bit.Substring (32, 8)); //barrel
			body = getNum (item_bit.Substring (40, 8)); //body
			stock = getNum (item_bit.Substring (48, 8)); //stock
			scope_rails = getNum (item_bit.Substring (56, 4)); //scope_rails
			barrelmods = getNum (item_bit.Substring (60, 4)); //barrel mods


		}


		//MELEE WEAPON
		else if (ntyp == 1) {
			type = "Melee Weapon";
			Debug.Log (type);

			string effect;
			bool bossdrop, one_twohand;
			int blade, hilt;

			getbits = item_bit.Substring (19, 1); //bossdrop
			if (getbits == "0")
				bossdrop = true;
			else
				bossdrop = false;

			getbits = item_bit.Substring (20, 1); //one or two handed
			if (getbits == "0")
				one_twohand = true;
			else
				one_twohand = false;

			getbits = item_bit.Substring (21, 3); //effect
			if (getbits == "000")
				effect = "Stun";
			else if (getbits == "001")
				effect = "Heat";
			else if (getbits == "010")
				effect = "Cold";
			else if (getbits == "011")
				effect = "Poison";
			else if (getbits == "100")
				effect = "Weak";
			else if (getbits == "101")
				effect = "AD";
			else
				effect = "Bleed";

			blade = getNum (item_bit.Substring (32, 8)); //blade
			hilt = getNum (item_bit.Substring (40, 8)); //hilt

		}


		//ARMOR/EQUIP
		else if (ntyp == 2) {
			type = "Armor/Equip";
			Debug.Log (type);

			string subtype;
			bool bossdrop;
			int head, torso, pants, shoes;

			getbits = item_bit.Substring (19, 1); //bossdrop
			if (getbits == "0")
				bossdrop = true;
			else
				bossdrop = false;

			getbits = item_bit.Substring (20, 3); //subtype
			if (getbits == "000")
				subtype = "EVA";
			else if (getbits == "001")
				subtype = "Heavy";
			else if (getbits == "010")
				subtype = "Medium";
			else if (getbits == "011")
				subtype = "Light";
			else if (getbits == "100")
				subtype = "Cosmetics";
			else
				subtype = "Unique";

			head = getNum (item_bit.Substring (32, 8)); //head
			torso = getNum (item_bit.Substring (40, 8)); //torso
			pants = getNum (item_bit.Substring (48, 8)); //pants
			shoes = getNum (item_bit.Substring (56, 8)); //shoes

		}


		//NOVELTY/UNIQUE
		else if (ntyp == 3) {
			type = "Novelty/Unique";
			Debug.Log (type);

			bool bossdrop, stackable;
			int stack_max, mesh_id;

			getbits = item_bit.Substring (19, 1); //bossdrop
			if (getbits == "0")
				bossdrop = true;
			else
				bossdrop = false;

			getbits = item_bit.Substring (20, 1); //stackable
			if (getbits == "0")
				stackable = true;
			else
				stackable = false;

			stack_max = getNum (item_bit.Substring (21, 8)); //max stack size
			mesh_id = getNum (item_bit.Substring (32, 32)); //mesh id
		}


		//CONSUMABLE
		else if (ntyp == 4) {
			type = "Consumable";
			Debug.Log (type);

			string subtype;
			int stack_max, mesh_id;

			getbits = item_bit.Substring (19, 3); //subtype
			if (getbits == "000")
				subtype = "Ammo";
			else if (getbits == "001")
				subtype = "Thrown";
			else if (getbits == "010")
				subtype = "Placeable";
			else if (getbits == "011")
				subtype = "Consumable Effect";
			else
				subtype = "Consumable Material";

			stack_max = getNum (item_bit.Substring (22, 7)); //max stack
			mesh_id = getNum (item_bit.Substring (32, 32)); //mesh id

		} 


		//NOPE
		else {
		}
			//return null;



	}

	//Converts ulong into binary rep as a string
	public string getBit(long num)
	{
		bits = Convert.ToString (num, 2);

		while (bits.Length < 64) {
			bits = "0" + bits;
		}

		Debug.Log (bits);

		return bits;
	}

	public int getNum(string num)
	{
		int numb = Convert.ToInt32 (num, 2);
			
		Debug.Log (numb);
			
		return numb;
	}
}

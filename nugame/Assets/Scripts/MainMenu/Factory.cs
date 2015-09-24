using UnityEngine;
using System.Collections;
using System;
using System.Runtime;

public class Factory : MonoBehaviour {

	public string bits;


	//Arrays of possibilities for item's type/prefix/suffix/subtype/etc...
	public string[] type_ind = {"Ranged", "Melee", "Armor", "Novelty", "Consumable"};
	string[] pref_ind = {"Cool", "Great ", "Rusty ", "Slimy ", "Fantastic ", "Deathly ", "Overpowering ", "Corrupted ",
		"Astonishing ", "Poison ", "Harrowing "};
	string[] suff_ind = {"on Crack", " of Doom", " of the Light", " of Terror"};
	//string[] boss_ind = {"", "Kingly "};
	string[] rstype_ind = {"Hand Gun", "Rifle ", "Heavy", "Shoulder"};
	//string[] silen_ind = {"", "Silent "};
	//string[] laser_ind = {"", "Laser "};
	//string[] flash_ind = {"", "Flashlight "};
	//string[] hand_ind = {"one", "two"};
	string[] eff_ind = {"stun", "heat", "cold", "poison", "weak", "AD", "Bleed", ""};
	string[] astype_ind = {"EVA", "Heavy", "Medium", "Light", "Cosmetic", "Unique"};
	//string[] nstackable_ind = {"yes", "no"};
	//string[] nstacksize_ind = {"1", "2", "3", "4"};
	string[] cstype_ind = {"Ammo", "Thrown", "Placeable", "Status Effect", "Consumable Material"};
	//string[] cstacksize_ind = {"1", "2", "3"};
	


	//Once all details of item system known, will return GameObject, currently returns void
	//Takes in item id, constructs "GameItem" with all components of the item,
	//then creates item itself(still in progress)
	public void/*GameObject*/ constructItem(long id)
	{
		GameItem itemized = parseItem (id); //construct GameItem
		//return makeItem (itemized);       //create item GameObject in scene 
	}

	//Once all details of item system known, will return GameObject, currently returns void
	//Uses GameItem to construct item as a GameObject in scene(still in progress)
	public /*GameObject*/ void makeItem(GameItem itemized)
	{
	
	}

	// Use this for item construction
	public GameItem parseItem(long id)
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

		type = type_ind [ntyp];
		prefix = pref_ind [npre];
		suffix = suff_ind [nsuf];



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
			subtype = rstype_ind[getNum (getbits)];

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

			GameItem j = new GameItem( type,  prefix,  suffix,  subtype,  bossdrop,  silencer,  lasersight,  flashlight,
			                barrel,  body,  stock,  scope_rails,  barrelmods,  id);

			return j;
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
			effect = eff_ind[getNum (getbits)];


			blade = getNum (item_bit.Substring (32, 8)); //blade
			hilt = getNum (item_bit.Substring (40, 8)); //hilt

			GameItem j = new GameItem( type,  prefix,  suffix,  bossdrop,  effect,  one_twohand,  blade,  hilt,  id);
			return j;

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
			subtype = astype_ind[getNum (getbits)];

			head = getNum (item_bit.Substring (32, 8)); //head
			torso = getNum (item_bit.Substring (40, 8)); //torso
			pants = getNum (item_bit.Substring (48, 8)); //pants
			shoes = getNum (item_bit.Substring (56, 8)); //shoes

			GameItem j = new GameItem( type,  prefix,  suffix,  subtype,  bossdrop,  head,  torso,  pants,  shoes,  id);
			return j;
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

			GameItem j = new GameItem( type,  prefix,  suffix,  bossdrop,  stackable,  stack_max,  mesh_id,  id);
			return j;
		}


		//CONSUMABLE
		else {
			type = "Consumable";
			Debug.Log (type);

			string subtype;
			int stack_max, mesh_id;

			getbits = item_bit.Substring (19, 3); //subtype
			subtype = cstype_ind[getNum (getbits)];

			stack_max = getNum (item_bit.Substring (22, 7)); //max stack
			mesh_id = getNum (item_bit.Substring (32, 32)); //mesh id

			GameItem j = new GameItem( type,  prefix,  suffix,  subtype,  stack_max,  mesh_id,  id);
			return j;

		} 



	}

	//Converts long into binary rep as a string
	public string getBit(long num)
	{
		bits = Convert.ToString (num, 2);

		while (bits.Length < 64) {
			bits = "0" + bits;
		}

		Debug.Log (bits);

		return bits;
	}

	//Converts binary as a string to its base 10 numerical representation
	public int getNum(string num)
	{
		int numb = Convert.ToInt32 (num, 2);
			
		Debug.Log (numb);
			
		return numb;
	}

	public class GameItem
	{
		string type, prefix, suffix; //string rep of features

		// range
		string subtype;
		bool bossdrop, silencer, lasersight, flashlight;
		int barrel, body, stock, scope_rails, barrelmods;

		// melee
		string effect;
		bool one_twohand; //bossdrop
		int blade, hilt;

		//armor
		int head, torso, pants, shoes; //bossdrop, subtype

		//novelty
		bool stackable; //bossdrop
		int stack_max, mesh_id;

		long id;

		//consumable
		//subtype, stackable, stack_max, meshid

		//range weapon
		public GameItem(string typ, string prefi, string suffi, string subtyp, bool bossdro, bool silence, bool lasersigh, bool flashligh,
		                int barre, int bod, int stoc, int scope_rail, int barrelmod, long i)
		{
			type = typ;
			prefix = prefi;
			suffix = suffi;
			subtype = subtyp;
			bossdrop = bossdro;
			silencer = silence;
			lasersight = lasersigh;
			flashlight = flashligh;
			barrel = barre;
			body = bod;
			stock = stoc;
			scope_rails = scope_rail;
			barrelmods = barrelmod;
			id = i;

		}

		//melee weapon
		public GameItem(string typ, string prefi, string suffi, bool bossdro, string effec, bool one_twohan, int blad, int hil, long i)
		{
			type = typ;
			prefix = prefi;
			suffix = suffi;
			effect = effec;
			bossdrop = bossdro;
			one_twohand = one_twohan;
			blade = blad;
			hilt = hil;
			id = i;
		}

		//armor
		public GameItem(string typ, string prefi, string suffi, string subtyp, bool bossdro, int hea, int tors, int pant, int shoe, long i)
		{
			type = typ;
			prefix = prefi;
			suffix = suffi;
			subtype = subtyp;
			bossdrop = bossdro;
			head = hea;
			torso = tors;
			pants = pant;
			shoes = shoe;
			id = i;
		}

		//novelty
		public GameItem(string typ, string prefi, string suffi, bool bossdro, bool stackabl, int stack_ma, int mesh_i, long i)
		{
			type = typ;
			prefix = prefi;
			suffix = suffi;
			bossdrop = bossdro;
			stackable = stackabl;
			stack_max = stack_ma;
			mesh_id = mesh_i;
			id = i;
		}

		//consumable
		public GameItem(string typ, string prefi, string suffi, string subtyp, int stack_ma, int mesh_i, long i)
		{
			type = typ;
			prefix = prefi;
			suffix = suffi;
			subtype = subtyp;
			stack_max = stack_ma;
			mesh_id = mesh_i;
			id = i;
		}

		public void print()
		{
			Debug.Log ( type + ", " + prefix + ", " + suffix);
		}


	}
}

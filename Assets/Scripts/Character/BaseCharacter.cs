using UnityEngine;
using System.Collections;
using System;         //added to access enum class

public class BaseCharacter : MonoBehaviour { //inherits MonoBehaviour to let us click and drag onto a game object

	private string _name;
	private int _level;
	private uint _freeExp;

	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skill[] _skill;

	public void Awake(){
		_name = string.Empty;
		_level = 1;
		_freeExp = 0;

		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];

		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}

	public string Name {
		get{return _name;}
		set{_name = value;}
	}

	public int Level {
		get{return _level;}
		set{_level = value;}
	}

	public uint FreeExp {
		get{return _freeExp;}
		set{_freeExp = value;}
	}

	public void AddExp(uint exp){
		_freeExp += exp;
		CalculateLevel();
	}
	//take avg of all of players skills and assign that as the player level?
	public void CalculateLevel(){
		
	}
	private void SetupPrimaryAttributes(){
		for(int cnt = 0; cnt < _primaryAttribute.Length; cnt++){
			_primaryAttribute[cnt] = new Attribute();
		}
	}
	private void SetupVitals(){
		for(int cnt = 0; cnt < _vital.Length; cnt++){
			_vital[cnt] = new Vital();
		}
		SetupVitalModifiers();
	}
	private void SetupSkills(){
		for(int cnt = 0; cnt < _skill.Length; cnt++){
			_skill[cnt] = new Skill();
		}
		SetupSkillModifiers();
	}
	public Attribute GetPrimaryAttribute(int index){
		return _primaryAttribute[index];
	}
	public Vital GetVital(int index){
		return _vital[index];
	}
	public Skill GetSkill(int index){
		return _skill[index];
	}
	private void SetupVitalModifiers(){
		//add half of primary attribute to vital
		//health
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Fortitude), ratio = 3});
		//energy
		GetVital((int)VitalName.Energy).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Willpower ), ratio = 2.5f});
		//mana
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Spirit), ratio = 4.5f});
	}
	private void SetupSkillModifiers(){
		//Melee Damage
		GetSkill((int)SkillName.Melee_Damage).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Strength), ratio = .33f});
		//Knockback Resistance
		GetSkill((int)SkillName.Knockback_Resistance).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Strength), ratio = .33f});
		//Physical Resistance
		GetSkill((int)SkillName.Physical_Resistance).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Fortitude), ratio = .33f});
		//Psionic Resistance
		GetSkill((int)SkillName.Psionic_Resistance).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Willpower), ratio = .33f});
		//Psionic Power
		GetSkill((int)SkillName.Psionic_Power).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Willpower), ratio = .33f});
		//Magic Resistance
		GetSkill((int)SkillName.Magic_Resistance).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Spirit), ratio = .33f});
		//Spell Power
		GetSkill((int)SkillName.Spell_Power).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Spirit), ratio = .33f});
		//Skill Points
		GetSkill((int)SkillName.Skill_Points).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Intellect), ratio = .33f});
		//Mastery Chance
		GetSkill((int)SkillName.Mastery_Chance).AddModifier(new ModifyingAttribute{attribute = GetPrimaryAttribute((int)AttributeName.Intellect), ratio = .33f});
	}

	public void StatUpdate(){
		for(int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt].Update();
		for(int cnt = 0; cnt < _skill.Length; cnt++)
			_skill[cnt].Update();
	}

}

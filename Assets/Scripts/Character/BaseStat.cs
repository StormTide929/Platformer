
public class BaseStat{
	private int _baseValue;				//base value of this stat
	private int _buffValue;				//amount of the buff/debuff to this stat
	private int _expToLevel;             //total amount of exp needed to raise this skill
	private float _levelModifier;         //mod applied to the exp needed to raise skill

	public BaseStat(){
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;
		_expToLevel = 100;
	}
#region Basic Setters and Getters
	//Basic Settors and Getters
	public int BaseValue{
			get{return _baseValue;}
			set{ _baseValue = value;}
	}
	public int BuffValue{
			get{return _buffValue;}
			set{ _buffValue = value;}
	}
	public int ExpToLevel{
			get{return _expToLevel;}
			set{ _expToLevel = value;}
	}
	public float LevelModifier{
			get{return _levelModifier;}
			set{ _levelModifier = value;}
	}
#endregion

	private int CalculateExpToLevel(){
		return (int)(_expToLevel * _levelModifier);
	}
	public void LevelUp(){
		_expToLevel = CalculateExpToLevel();
		_baseValue++;
	}
	public int AdjustedBaseValue {
		get { return _baseValue + _buffValue; }
	}
}

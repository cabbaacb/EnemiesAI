using RotaryHeart.Lib.SerializableDictionary;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
	public enum AnimationType : byte
	{
		Move = 0,
		FastAttack = 1,
		StrongAttack = 2,
		Die = 3
	}

	[System.Flags]
	public enum IgnoreAxisType : byte
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 4
	}

	public enum UnitColor : byte
    {
		Red = 1,
		Green = 2,
		Blue = 3		
    }


	[System.Serializable]
	public class AnimationKeyDictionary : SerializableDictionaryBase<AnimationType, string> { }
}

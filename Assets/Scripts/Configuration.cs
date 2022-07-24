using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
	[CreateAssetMenu(fileName = "New Configuration", menuName = "Configuration")]
	public class Configuration : ScriptableObject
	{
		[SerializeField]
		private AnimationKeyDictionary _keys = null;

		public IReadOnlyDictionary<AnimationType, string> GetDictionary => _keys.Clone();
	}
}
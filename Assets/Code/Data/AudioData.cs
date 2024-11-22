using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.GArkanoid
{
	public enum AudioType
	{
		None = 0,
		Hit,
		Destroy
	}

	[CreateAssetMenu(fileName = "AudioData", menuName = "Audio Data")]
	public class AudioData : ScriptableObject
	{
		[System.Serializable]
		public struct ClipData
		{
			public AudioType Type;
			public AudioClip Clip;
		}

		[SerializeField] private ClipData[] _clips = null;

		public AudioClip GetClip(AudioType audioType)
		{
			foreach (ClipData data in _clips)
			{
				if (data.Type == audioType)
				{
					return data.Clip;
				}
			}
			return null;
		}
	}
}

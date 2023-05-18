using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JsonData
{
	JSONObject _json;
	public JsonData(JSONObject json)
	{
		_json = json;
	}

	public int GetInt(string key, int defaultValue = 0)
	{
		if (_json.GetField(key) == null)
		{
			return defaultValue;
		}
		return (int)_json.GetField(key).i;
	}

	public int[] GetInts(string key)
	{
		string array = _json.GetField(key).str;
		array = array.Replace("[", "");
		array = array.Replace("]", "");
		array = array.Replace("n", "");
		array = array.Replace("\\", "");
		Debug.Log("array:" + array);
		string[] arraySplit = array.Split(',');
		int[] Ints = new int[arraySplit.Length];
		for (int i = 0; i < arraySplit.Length; i++)
		{
			int number;
			if (Int32.TryParse(arraySplit[i], out number))
			{
				Ints[i] = Int32.Parse(arraySplit[i]);
			}
		}
		return Ints;
	}

	public string GetString(string key, string defaultValue = "")
	{
		if (_json.GetField(key) == null)
		{
			return defaultValue;
		}
		return _json.GetField(key).str;
	}

	public string[] Get2RowStrings(string key)
	{
		string array = _json.GetField(key).str;
		array = array.Replace("n", "");
		array = array.Replace("\\", "");
		string[] del = { "]," };
		string[] arraySplit = array.Split(del, StringSplitOptions.None); // [1],[1,2],[1,2,3]
		for (int i = 0; i < arraySplit.Length; i++)
		{
			if (i != (arraySplit.Length - 1))
			{
				arraySplit[i] = arraySplit[i] + "]";
			}
		}
		return arraySplit;
	}

	public string[] GetStrings(string key)
	{
		string array = _json.GetField(key).str;
		array = array.Replace("[", "");
		array = array.Replace("]", "");
		array = array.Replace("\\", "");
		string[] arraySplit = array.Split(',');
		return arraySplit;
	}

	public float GetFloat(string key, float defaultValue = 0)
	{
		if (_json.GetField(key) == null)
		{
			return defaultValue;
		}
		return _json.GetField(key).f;
	}

	public float[] GetFloats(string key)
	{
		string array = _json.GetField(key).str;
		array = array.Replace("[", "");
		array = array.Replace("]", "");
		array = array.Replace("n", "");
		array = array.Replace("\\", "");
		string[] arraySplit = array.Split(',');
		float[] Floats = new float[arraySplit.Length];

		for (int i = 0; i < arraySplit.Length; i++)
		{
			float number;
			if (float.TryParse(arraySplit[i], out number))
			{
				Floats[i] = float.Parse(arraySplit[i]);
			}
		}
		return Floats;
	}

	public string GetJsonString()
	{
		return _json.ToString();
	}
}
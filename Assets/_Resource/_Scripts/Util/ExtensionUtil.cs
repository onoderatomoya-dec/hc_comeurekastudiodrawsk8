using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionUtil
{
	public static void SetPositionX(this Transform tran, float x)
	{
		Vector3 pos = tran.transform.position;
		tran.transform.position = new Vector3(x, pos.y, pos.z);
	}
	public static void SetPositionY(this Transform tran, float y)
	{
		Vector3 pos = tran.transform.position;
		tran.transform.position = new Vector3(pos.x, y, pos.z);
	}
	public static void SetPositionZ(this Transform tran, float z)
	{
		Vector3 pos = tran.transform.position;
		tran.transform.position = new Vector3(pos.x, pos.y, z);

	}

	public static void SetLocalPositionX(this Transform tran, float x)
	{
		Vector3 pos = tran.transform.localPosition;
		tran.transform.localPosition = new Vector3(x, pos.y, pos.z);
	}
	public static void SetLocalPositionY(this Transform tran, float y)
	{
		Vector3 pos = tran.transform.localPosition;
		tran.transform.localPosition = new Vector3(pos.x, y, pos.z);
	}
	public static void SetLocalPositionZ(this Transform tran, float z)
	{
		Vector3 pos = tran.transform.localPosition;
		tran.transform.localPosition = new Vector3(pos.x, pos.y, z);

	}


	public static Animator GetAnimator(this GameObject obj)
	{
		if (obj.GetComponent<Animator>() != null)
		{
			return obj.GetComponent<Animator>();
		}
		return null;
	}

	public static BoxCollider GetBoxCollider(this GameObject obj)
	{
		if (obj.GetComponent<BoxCollider>() != null)
		{
			return obj.GetComponent<BoxCollider>();
		}
		return null;
	}
	public static BoxCollider2D GetBoxCollider2D(this GameObject obj)
	{
		if (obj.GetComponent<BoxCollider2D>() != null)
		{
			return obj.GetComponent<BoxCollider2D>();
		}
		return null;
	}

	public static SphereCollider GetSphereCollider(this GameObject obj)
	{
		if (obj.GetComponent<SphereCollider>() != null)
		{
			return obj.GetComponent<SphereCollider>();
		}
		return null;
	}

	public static Rigidbody GetRigidbody(this GameObject obj)
	{
		if (obj.GetComponent<Rigidbody>() != null)
		{
			return obj.GetComponent<Rigidbody>();
		}
		return null;
	}
	public static Rigidbody2D GetRigidbody2D(this GameObject obj)
	{
		if (obj.GetComponent<Rigidbody2D>() != null)
		{
			return obj.GetComponent<Rigidbody2D>();
		}
		return null;
	}

	public static Material GetMaterial(this GameObject obj)
	{
		if (obj.GetComponent<MeshRenderer>() != null)
		{
			return obj.GetComponent<MeshRenderer>().material;
		}
		return null;
	}

	public static void SetChildren(this Transform self, Transform children)
	{
		children.SetParent(self);
	}

	public static bool IsAny<T>(this T self, params T[] values)
	{
		return values.Any(c => self.Equals(c));
	}

	// 偶数かどうか判定
	public static bool IsEven(this int value)
	{
		return value % 2 == 0;
	}

	// 奇数かどうか判定
	public static bool IsOdd(this int value)
	{
		return value % 2 != 0;
	}
}

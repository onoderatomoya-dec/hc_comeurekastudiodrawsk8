using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLAddTimeStump : MonoBehaviour {
	
 	public static string urlAddTimeStump(string str){
 		
 		// ���[�N�ϐ�
 		string ret_str = "";
 		
 		ret_str = str;
 		
 		ret_str += ((str.Contains("?")) ? "&" : "?") + "t=" + DateTime.Now.ToString("yyyyMMddHHmmss");
 		
 		return ret_str;
 		
 	}
	
}
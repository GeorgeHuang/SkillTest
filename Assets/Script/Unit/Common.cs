using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System;
using System.Security.Cryptography;
public class Common  
{
	private static bool m_isEncrypt = false;
	public static bool IsEncrypt
	{
		get
		{
			return m_isEncrypt;
		}
		set
		{
			m_isEncrypt = value;
		}
	}

	public static string Encrypt(string toE)
	{
		
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578906543367877723456789012");
		RijndaelManaged rDel = new RijndaelManaged();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateEncryptor();
		
		byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toE);
		byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray,0,toEncryptArray.Length);
	
		return Convert.ToBase64String(resultArray,0,resultArray.Length);
	}
	
	
	public static string Decrypt(string toD)
	{
	
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12348578906543367877723456789012");
		RijndaelManaged rDel = new RijndaelManaged();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		rDel.Padding = PaddingMode.PKCS7;
		ICryptoTransform cTransform = rDel.CreateDecryptor();
		
		byte[] toEncryptArray = Convert.FromBase64String(toD);
		byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray,0,toEncryptArray.Length);
		
		return UTF8Encoding.UTF8.GetString(resultArray);
	}
	public static string PlayerDataPath
    {
        get
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer
                || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor)
                return Application.dataPath + "/Documents/";
            else
                return Application.persistentDataPath + "/";
        }
    }
	static public GameObject deepFindS(GameObject go, string name)
    {
        foreach (Transform ts in go.transform)
        {
            if (ts.name.Contains(name) && ts.name.Length == name.Length)
                return ts.gameObject;
            else
            {
                GameObject temp = deepFindS(ts.gameObject, name);
                if (temp != null)
                    return temp;
            }
        }
        return null;
    }
	static public void changGOParent(GameObject child, GameObject parent, bool reset = true)
    {
        if (child == null)
            return;
        
        if (parent == null)
        {
            child.transform.parent = null;
            return;
        }
        Vector3 pos = Vector3.zero;   
        Quaternion q = Quaternion.identity;
        if (reset == false)
        {
            pos = child.transform.localPosition;
            q = child.transform.localRotation;
        }
        child.transform.parent = parent.transform;
        child.transform.localPosition = pos;
        child.transform.localRotation = q;
    }
	static public void dicToObject(System.Object obj,Dictionary<string,object> dict)
	{
		
		foreach (KeyValuePair<string,object> item in dict)
		{
			FieldInfo fi = obj.GetType().GetField(item.Key, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi != null)
			{
				MethodInfo mi = fi.FieldType.GetMethod("Parse", new Type[] { typeof(string) });
				string tempStr = item.Value.ToString();
				Debug.Log(tempStr);
                System.Object value = fi.GetValue(obj);
                if (mi != null)
                {
					
					
                    fi.SetValue(obj, mi.Invoke(obj, new object[] { tempStr }));
                }
                else if (value is System.Enum)
                {
                    fi.SetValue(obj, System.Enum.Parse(fi.FieldType, tempStr));
                }
                else if (fi.FieldType == typeof(string))
                {
                    fi.SetValue(obj, tempStr);
                }
				else if (fi.FieldType == typeof(System.Collections.Generic.Dictionary<string,object>))
				{
					fi.SetValue(obj,fromMiniJson(tempStr));
				}
                else if (fi.FieldType == typeof(System.Int32[]))
                {
                    System.Object temp = fromMiniJson(tempStr);
                    if (temp != null)
                    {
                        List<System.Object> objList = (List<System.Object>)temp;
                        List<int> intList = new List<int>();
                        foreach (System.Object sobj in objList)
                        {
                            intList.Add(System.Convert.ToInt32(sobj));
                        }
                        fi.SetValue(obj, intList.ToArray());
                    }
                }
			}
			
			
		}
	}
	static public void fromString(System.Object obj, string input)
    {
        string[] arrays = input.Split('\n');
		
        foreach (string array in arrays)
        {
			//Debug.Log(array);
            string[] subarray = array.Split(new Char[] { ',' }, 2);
            FieldInfo fi = obj.GetType().GetField(subarray[0], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (fi != null)
            {
                MethodInfo mi = fi.FieldType.GetMethod("Parse", new Type[] { typeof(string) });
				
                System.Object value = fi.GetValue(obj);
                if (mi != null)
                {
					//Debug.Log("Hello");
                    fi.SetValue(obj, mi.Invoke(obj, new object[] { subarray[1] }));
                }
                else if (value is System.Enum)
                {
                    fi.SetValue(obj, System.Enum.Parse(fi.FieldType, subarray[1]));
                }
                else if (fi.FieldType == typeof(string))
                {
                    fi.SetValue(obj, subarray[1]);
                }
				else if (fi.FieldType == typeof(System.Collections.Generic.Dictionary<string,object>))
				{
					fi.SetValue(obj,fromMiniJson(subarray[1]));
				}
                else if (fi.FieldType == typeof(System.Int32[]))
                {
                    System.Object temp = fromMiniJson(subarray[1]);
                    if (temp != null)
                    {
                        List<System.Object> objList = (List<System.Object>)temp;
                        List<int> intList = new List<int>();
                        foreach (System.Object sobj in objList)
                        {
                            intList.Add(System.Convert.ToInt32(sobj));
                        }
                        fi.SetValue(obj, intList.ToArray());
                    }
                }
            }
        }
    }
	static public System.Object fromMiniJson(string input)
    {
        return MiniJSON.Json.Deserialize(input);
    }
	static public string TimeStr(float value)
    {
        int min = (int)value / 60;
        int sec = (int)value % 60;
        //int ms = (int)(value * 1000) % 1000;
        return min.ToString("D2") +":"+ sec.ToString("D2") ;
    }
    static public string TimeHrStr(float value)
    {
        int hr = (int)value/3600;
        int min = (int)value%3600/ 60;
        int sec = (int)value % 60;
        //int ms = (int)(value * 1000) % 1000;
        return hr.ToString()+":"+min.ToString("D2") +":"+ sec.ToString("D2") ;
    }
	static public string toString(System.Object obj)
    {
        string result = "";
        foreach (FieldInfo fi in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            System.Object valueObj = fi.GetValue(obj);
            if (!(valueObj is IDictionary || valueObj is IList))
                result = result + fi.Name + "," + valueObj + "\n";
            else
                result = result + fi.Name + "," + toMiniJson(valueObj) + "\n";
        }
        return result;
    }
	static public string toMiniJson(System.Object obj)
    {
        return MiniJSON.Json.Serialize(obj);
    }
	public static void sysPrint(System.Object temp)
    { 
        Debug.Log(temp);
    }
}

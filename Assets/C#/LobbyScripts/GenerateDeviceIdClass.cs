using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class GenerateDeviceIdClass
{
    public static string GenerateUniqueIdentifier1()
    {
        // Get device-specific information
        string deviceModel = SystemInfo.deviceModel;
        string deviceName = SystemInfo.deviceName;
        string uniqueIdentifier = string.Format("{0}_{1}", deviceModel, deviceName);

        // Create a SHA256 hash
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(uniqueIdentifier));
            StringBuilder stringBuilder = new StringBuilder();

            // Convert hash bytes to string
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("x2")); // Convert to hexadecimal string
            }
            PlayerPrefs.SetString("deviceid",stringBuilder.ToString());
            return stringBuilder.ToString();
        }
    }
}

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.Data.SqlClient;

namespace DriverDistributor.Services;
public class SecretEncodeDecode
{
    private static byte[] Key = Encoding.UTF8.GetBytes("6ECA56378DF2BCDCC7E0E1E90E0AA4F3");
    private static byte[] IV = Encoding.UTF8.GetBytes("1E7C13F956A420A7");
    private static string secretPath;

    public SecretEncodeDecode(IConfiguration configuration)
    {
        secretPath = configuration["secretPath"];
    }

    public static void EncodeToJson(Dictionary<string, string> input, string path)
    {

        var output = new Dictionary<string, string>();

        using var aes = Aes.Create();

        aes.Key = Key;
        aes.IV = IV;

        var encryptor = aes.CreateEncryptor();

        foreach (var item in input)
        {
            Byte[] pByte = Encoding.UTF8.GetBytes(item.Value);
            Byte[] cByte = encryptor.TransformFinalBlock(pByte, 0, pByte.Length);
            output[item.Key] = Convert.ToBase64String(cByte);
        }

        File.WriteAllText(path, JsonSerializer.Serialize(output, new JsonSerializerOptions { WriteIndented = false }));
    }

    public static SqlConnectionStringBuilder DecodeToJson(string path)
    {
        var encryptedContents = File.ReadAllText(path);
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        var decryptor = aes.CreateDecryptor();

        var encryptedDict = JsonSerializer.Deserialize<Dictionary<string, string>>(encryptedContents);
        var output = new Dictionary<string, string>();

        foreach (var item in encryptedDict)
        {
            var cbyte = Convert.FromBase64String(item.Value);
            var pbyte = decryptor.TransformFinalBlock(cbyte, 0, cbyte.Length);
            output[item.Key] = Encoding.UTF8.GetString(pbyte);
        }

        SqlConnectionStringBuilder connectionString = new();

        connectionString.DataSource = output["Data Source"];
        connectionString.InitialCatalog = output["Initial Catalog"];
        connectionString.UserID = output["User ID"];
        connectionString.Password = output["Password"];
        connectionString.MultipleActiveResultSets =bool.Parse( output["MultipleActiveResultSets"]);
        connectionString.TrustServerCertificate =bool.Parse( output["TrustServerCertificate"]);

        return connectionString;
    }
}


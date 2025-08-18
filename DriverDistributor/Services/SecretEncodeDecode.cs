using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Data.SqlClient;

namespace DriverDistributor.Services;
public class SecretEncodeDecode
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("6ECA56378DF2BCDCC7E0E1E90E0AA4F3");
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("1E7C13F956A420A7");

    public SecretEncodeDecode(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public void EncodeToJson()
    {
        var dirInfo = Directory.CreateDirectory(Path.Combine(_env.ContentRootPath, "Private"));

        SaveSectionToFile("ConnectionStrings:Remote:Local", Path.Combine(dirInfo.FullName, "secrets.Remote.Local.json"));
        SaveSectionToFile("ConnectionStrings:Remote:Remote", Path.Combine(dirInfo.FullName, "secrets.Remote.Remote.json"));
    }

    private void SaveSectionToFile(string section, string path)
    {
        var input = _configuration.GetSection(section).Get<Dictionary<string, string>>();
        var output = new Dictionary<string, string>();

        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;
        var encryptor = aes.CreateEncryptor();

        foreach (var item in input)
        {
            var pByte = Encoding.UTF8.GetBytes(item.Value);
            var cByte = encryptor.TransformFinalBlock(pByte, 0, pByte.Length);
            output[item.Key] = Convert.ToBase64String(cByte);
        }

        File.WriteAllText(path, JsonSerializer.Serialize(output, new JsonSerializerOptions { WriteIndented = false }));
    }

    public SqlConnectionStringBuilder DecodeToJson(string path)
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

        SqlConnectionStringBuilder cstring = new()
        {
            DataSource = output["Data Source"],
            InitialCatalog = output["Initial Catalog"],
            UserID = output["User ID"],
            Password = output["Password"],
            MultipleActiveResultSets = bool.Parse(output["MultipleActiveResultSets"]),
            Encrypt = bool.Parse(output["Encrypt"]),
        };
        if (!path.Contains(".Remote.Local.json")) 
            cstring.TrustServerCertificate = bool.Parse(output["TrustServerCertificate"]);

        return cstring;
    }
}

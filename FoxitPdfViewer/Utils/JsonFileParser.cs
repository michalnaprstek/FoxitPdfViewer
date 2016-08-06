using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace FoxitPdfViewer.Utils
{
  public class JsonFileParser
  {
    private readonly Uri fileUri;

    public JsonFileParser(string fileUri)
    {
      this.fileUri = new Uri(fileUri);
    }

    public JsonFileParser(Uri fileUri)
    {
      this.fileUri = fileUri;
    }

    /// <summary>
    /// Par
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<T> ParseAsync<T>()
    {
      //var appUri = new Uri(this.fileUri);//File name should be prefixed with 'ms-appx:///Assets/* 
      var file = await StorageFile.GetFileFromApplicationUriAsync(this.fileUri);
      var jsonText = await FileIO.ReadTextAsync(file);
      var jsonSerializer = new DataContractJsonSerializer(typeof(T));
      var jsonObject = JsonObject.Parse(jsonText);
      using (var jsonStream = new MemoryStream(Encoding.Unicode.GetBytes(jsonObject.ToString())))
      {
        return (T)jsonSerializer.ReadObject(jsonStream);
      }
    }
  }
}

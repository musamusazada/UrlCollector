using System.Net;
using Newtonsoft.Json;
using RestSharp;

class Program
{
    static void Main()
    {
       var data=  GetData();
       WriteData(data);
    }
    
//Method to fetch Data
    static List<Photo> GetData()
    {
        var client = new RestClient("http://jsonplaceholder.typicode.com/");
        var request = new RestRequest("photos");
        var response = client.Execute(request);
        List<Photo> result =  new List<Photo>();
        if (response.StatusCode == HttpStatusCode.OK)
        {
            string rawResponse = response.Content;
            return JsonConvert.DeserializeObject<List<Photo>>(rawResponse);
        }
        else
        {
            return result;
        }
        
    }

    //Method to make file with the URLS
    static void WriteData(List<Photo> photos)
    {
        //Default Output
        string filePath = @"C:\Users\User\Desktop\outputAmrah__Photos.txt";
        FileCreator(filePath);
        List<string> lines = new List<string>();
        
        photos.ForEach(el =>
        {
            if (el.id % 99 == 0)
            {
             lines.Add($"Photo id {el.id} :  URL: {el.url}");   
            }
        });
        File.WriteAllLines(filePath, lines);
        foreach (var line in File.ReadLines(filePath).ToList())
        {
            Console.WriteLine(line);
        }
        
        

    }
    
    //Creates a file.
    static void FileCreator(string filepath)
    {
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
        }

        using (StreamWriter sw = File.CreateText(filepath))
        {
            Console.WriteLine("New File Created");
        }   
    }
    
    //Photo class for deserialization 
     class Photo
    {
        public short albumId { get; set; }
        public short id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
    }

}



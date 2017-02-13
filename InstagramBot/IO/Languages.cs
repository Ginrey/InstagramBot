using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InstagramBot.IO
{
    public enum Language
    {
        Russian,
        English
    }
   public class Languages
   {
       static Dictionary<Language, Dictionary<string, string>> Translate =
           new Dictionary<Language, Dictionary<string, string>>();

       static  Dictionary<string, string> ReverseTranslate =
           new Dictionary<string, string>();

       public Languages()
       {
           foreach (Language lang in Enum.GetValues(typeof(Language)))
           {
               string path = "Languages/" + lang + ".lng";
               if (!File.Exists(path)) continue;
               LoadLanguages(lang, File.ReadAllLines(path));
           }
       }

       void LoadLanguages(Language lang, string[] lines)
       {
           
               Dictionary<string, string> translate =
                   lines.Select(line => line.Replace("\\n", "\n"))
                       .Where(l => !string.IsNullOrEmpty(l))
                       .ToDictionary(l => l.Split('|')[0], l => l.Split('|')[1]);
               Translate.Add(lang, translate);

               foreach (var line in lines)
               {
                   var l = line.Replace("\\n", "\n");
                   if (!string.IsNullOrEmpty(l))
                    ReverseTranslate[l.Split('|')[1]] = l.Split('|')[0];
               }
         
       }

       public string Get(Language lang, string key)
       {
           if (!Translate.ContainsKey(lang)) return "";
           if (!Translate[lang].ContainsKey(key)) return key;
           return Translate[lang][key];
       }

       public string GetReverse(string key)
       {
           if (!ReverseTranslate.ContainsKey(key)) return key;
           return ReverseTranslate[key];
       }
    }
}

﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json.Serialization; 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;



namespace CSHttpClientSample
{
    static class Program
    {
        public static string koppelingkey = "MEFBQVRaQnNIcHpFbkhyYVZlQW5xN0tuOGduelowRURsS2ZFa0p5TFhqZEUvUEF6OEdaOFV5OHc2ZlQzVzl5S3gyeG9FaS9mY2o4M09WUkFsd0FRbFFBdldwdGlhYjR1dHcra0kzQ1pWSy9rRnh5RkFDT2NzWngxRm5NeG9oMWFyajQ5Y28xVmJUYVRqbzRLWWREcUxGeXFuaTR2ZU9ic3FZWmlCdWk4UVZnUzlFQUxTSVk1NjFHYkh4RmVYQ3BkdC82dTZxZ2NiYTVlQzg3VTA4dmUrcEo2NFFOS1hkeUZWV3dmL1o5dGVFZHdyTnF0UU1VOTI0VkxaRE5LWkMwNjpvM1lxT2UwSHlwMW1EWDJGMDVBcVllWnp5ZmU4OXBwN0J3eEpLWVRrOU9xOEhuWGo1M0dkSEVTYWlhZnR5UkVQcFhFcUNmdDlWN3pqSWpSUmZlZzcyelF3eXJoekxmM3NXTVFnY1F3U2s1LzZNd3BHYWJrWkI0NlN3MTFySDBoWkZDZ0NtS0NBaXo4QThMZGs0bWVJK2d0bjJjVGhRS1VGS2RKd3NCSW9RTkVJSVB6RmJ5Y1pLNmtMcVNiTVdGNndodkQyTllVS3VlZEdDc0N6dUo5SEdMNEU2TVRXdkd4aU9kUXdreEppZTJsN1pDNWxmbDZxZjE0bk85NnVBTmtM";
        public static string APIkey = "e8c56e54886f4005915425073f127183";
       
        static void Main()
        {
            Decrypt(koppelingkey);
            //Getrelaties();
            //Postrelaties();
            //Putrelaties();
            //Deleterelaties();
            Token();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        static async void Getrelaties()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
                        
            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIkey);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 6NjlcfnqRHdnmQj9KsNzlGsY5Z3phurR-Nz-MhCTY4VCQwJy9kNZXQkbgDShUNutGw_ngtEFXTPOO5z2RFlicE4ZI7kaciFjd83meY-zYF-tCUkuJxTm1Q7vfI0wmVs3XfWL7o_7q8jLqJFDUnaNri6S3_Hsymb-gy7SCpajaSqm5txfR1V7PjhTcfVjrfdhTeh3vdagy30VzqpSEn71-RvaVRB0F9WfzIIAk4ETcoKILhhC3Bi55uCvMpH3KXKErQLfjMRQO8i7iyvZiEp_zRmAhNqskE81Zwch75WXG1JhQ0qiavpSopqKiZN_icNpUQYmDw7WHWJU8N9-HCGmzMcPRtGqk8MlS1WJCJ8Lx5NbKbzyD3wICUbfivXogxf0ZiLEYTQJYpQd8xep9hn1kwbqCe6LBLwjmcq4FpBzhXUtj3a6bNNWWXBW61Fg6YtNmr4nbr0bzH0XbhQL_mo3b-ydMBQqRHe-OjtJwyiXITirL2AB1rGNrpI_Vj0MNXvaV68pW6EFB2AHac69BVOuW9-X07DRw97NwyMYIDRmmOm7GSeoMP7n06adXUmZPRtY8HHZ3Gb3uDWfKWXvEdA_5j41W-FaCqRgTF6HP8xoRFELIrZ5ZJI5oe5W4S_mwAVNp-WbKfSPa4kdXVLdN87ih_TCXRkzs7atj2ZAaM6ATDL_ASgcUGar0Q2AnlqcOHxV-q2JasNWkief0UK-ayHCYj9NOnEol0WvXh_FTvYCV-dFdHsYEqpIT2kLrnt3ZH1c");

            var uri = "https://b2bapi.snelstart.nl/v1/relaties" + queryString;

            using (var response = await client.GetAsync(uri))
            {
                using (HttpContent content = response.Content)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await content.ReadAsStringAsync();
                        System.IO.File.WriteAllText(@"C:\Users\Freddy\Desktop\Minor Systeemontwikkeling\Response\getresponse.txt", Convert.ToString(result));
                    }
                }
            } 
        }
        
        static async void Postrelaties()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIkey);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 6NjlcfnqRHdnmQj9KsNzlGsY5Z3phurR-Nz-MhCTY4VCQwJy9kNZXQkbgDShUNutGw_ngtEFXTPOO5z2RFlicE4ZI7kaciFjd83meY-zYF-tCUkuJxTm1Q7vfI0wmVs3XfWL7o_7q8jLqJFDUnaNri6S3_Hsymb-gy7SCpajaSqm5txfR1V7PjhTcfVjrfdhTeh3vdagy30VzqpSEn71-RvaVRB0F9WfzIIAk4ETcoKILhhC3Bi55uCvMpH3KXKErQLfjMRQO8i7iyvZiEp_zRmAhNqskE81Zwch75WXG1JhQ0qiavpSopqKiZN_icNpUQYmDw7WHWJU8N9-HCGmzMcPRtGqk8MlS1WJCJ8Lx5NbKbzyD3wICUbfivXogxf0ZiLEYTQJYpQd8xep9hn1kwbqCe6LBLwjmcq4FpBzhXUtj3a6bNNWWXBW61Fg6YtNmr4nbr0bzH0XbhQL_mo3b-ydMBQqRHe-OjtJwyiXITirL2AB1rGNrpI_Vj0MNXvaV68pW6EFB2AHac69BVOuW9-X07DRw97NwyMYIDRmmOm7GSeoMP7n06adXUmZPRtY8HHZ3Gb3uDWfKWXvEdA_5j41W-FaCqRgTF6HP8xoRFELIrZ5ZJI5oe5W4S_mwAVNp-WbKfSPa4kdXVLdN87ih_TCXRkzs7atj2ZAaM6ATDL_ASgcUGar0Q2AnlqcOHxV-q2JasNWkief0UK-ayHCYj9NOnEol0WvXh_FTvYCV-dFdHsYEqpIT2kLrnt3ZH1c");

            var uri = "https://b2bapi.snelstart.nl/v1/relaties" + queryString;

             byte[] byteData = Encoding.UTF8.GetBytes("{body}");

             using (var content = new ByteArrayContent(byteData))
             {
                 content.Headers.ContentType = new MediaTypeHeaderValue("< your content type, i.e. application/json >");
                 var response = await client.PostAsync(uri, content);
                 System.IO.File.WriteAllText(@"C:\Users\Freddy\Desktop\Minor Systeemontwikkeling\Response\postresponse.txt", Convert.ToString(response));
             }
        }

        public static string Decrypt(string koppelingkey)
        {
            string decodedkey;
            decodedkey = Encoding.UTF8.GetString(Convert.FromBase64String(koppelingkey));
            return decodedkey; 
        }

        static async void Token()
        {
            using (HttpClient client = new HttpClient())
            {
                var tokenEndpoint = @"https://auth.snelstart.nl/b2b/token";

                string password;
                string username;
                string decoded = Decrypt(koppelingkey);
                
                var splitted = decoded.Split(':');
                password = splitted[1];
                username = splitted[0];

               string postBody = @"grant_type=password &username=" + Convert.ToString(username) + " &password=" + Convert.ToString(password);

                Console.WriteLine(postBody);

                using (var response = await client.PostAsync(tokenEndpoint, new StringContent(postBody, Encoding.UTF8, "application/x-www-form-urlencoded")))
                {
                    Console.WriteLine(response);
                    System.IO.File.WriteAllText(@"C:\Users\Freddy\Desktop\Minor Systeemontwikkeling\Response\tokenresponse.txt", Convert.ToString(response));
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonresult = JObject.Parse(await response.Content.ReadAsStringAsync());
                        var token = (string)jsonresult["access_token"];                       
                    }
                }
            }
        }
      }
    }
   
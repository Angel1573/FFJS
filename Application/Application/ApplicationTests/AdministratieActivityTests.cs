using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Tests
{
    [TestClass()]
    public class AdministratieActivityTests
    {
        [TestMethod()]
        public void DecryptTest()
        {
            string koppelingskey = "MEFBQVRaQnNIcHpFbkhyYVZlQW5xN0tuOGduelowRURsS2ZFa0p5TFhqZEUvUEF6OEdaOFV5OHc2ZlQzVzl5S3gyeG9FaS9mY2o4M09WUkFsd0FRbFFBdldwdGlhYjR1dHcra0kzQ1pWSy9rRnh5RkFDT2NzWngxRm5NeG9oMWFyajQ5Y28xVmJUYVRqbzRLWWREcUxGeXFuaTR2ZU9ic3FZWmlCdWk4UVZnUzlFQUxTSVk1NjFHYkh4RmVYQ3BkdC82dTZxZ2NiYTVlQzg3VTA4dmUrcEo2NFFOS1hkeUZWV3dmL1o5dGVFZHdyTnF0UU1VOTI0VkxaRE5LWkMwNjpvM1lxT2UwSHlwMW1EWDJGMDVBcVllWnp5ZmU4OXBwN0J3eEpLWVRrOU9xOEhuWGo1M0dkSEVTYWlhZnR5UkVQcFhFcUNmdDlWN3pqSWpSUmZlZzcyelF3eXJoekxmM3NXTVFnY1F3U2s1LzZNd3BHYWJrWkI0NlN3MTFySDBoWkZDZ0NtS0NBaXo4QThMZGs0bWVJK2d0bjJjVGhRS1VGS2RKd3NCSW9RTkVJSVB6RmJ5Y1pLNmtMcVNiTVdGNndodkQyTllVS3VlZEdDc0N6dUo5SEdMNEU2TVRXdkd4aU9kUXdreEppZTJsN1pDNWxmbDZxZjE0bk85NnVBTmtM";
            string expected = "0AAATZBsHpzEnHraVeAnq7Kn8gnzZ0EDlKfEkJyLXjdE/PAz8GZ8Uy8w6fT3W9yKx2xoEi/fcj83OVRAlwAQlQAvWptiab4utw+kI3CZVK/kFxyFACOcsZx1FnMxoh1arj49co1VbTaTjo4KYdDqLFyqni4veObsqYZiBui8QVgS9EALSIY561GbHxFeXCpdt/6u6qgcba5eC87U08ve+pJ64QNKXdyFVWwf/Z9teEdwrNqtQMU924VLZDNKZC06:o3YqOe0Hyp1mDX2F05AqYeZzyfe89pp7BwxJKYTk9Oq8HnXj53GdHESaiaftyREPpXEqCft9V7zjIjRRfeg72zQwyrhzLf3sWMQgcQwSk5/6MwpGabkZB46Sw11rH0hZFCgCmKCAiz8A8Ldk4meI+gtn2cThQKUFKdJwsBIoQNEIIPzFbycZK6kLqSbMWF6whvD2NYUKuedGCsCzuJ9HGL4E6MTWvGxiOdQwkxJie2l7ZC5lfl6qf14nO96uANkL";

            string result = Application.AdministratieActivity.Decrypt(koppelingskey);

            Assert.AreEqual(expected, result);
        }
    }
}
using DTO;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF_chesedWroled;

namespace WPFMiniAplication
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {

        //יודע לקרוא לשירותים
        HttpClient client = new HttpClient();
        //הכנת כתובת להתחברות
        string address = "https://localhost:7227/api";
        public login()
        {
            InitializeComponent();
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            // בדיקה אם הכל מלא
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Password))
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }

            try
            {
                //אובביקט אנונימי
                var login = new VolunteerDTO
                {
                   Tz = txtId.Text,
                    Password = txtPass.Password,
                    //לא מקצועי בכלל!!
                    //לא שולחים סתם אובייקט שאין לי שימוש בו
                    //צריכה לשנות וליצור אובייקט חדש מסוג LOGIN שיש בו רק את מה שאני צריכה בשביל הפעולה הזו
                    FullName = "",
                    Email = "",
                    Address = "",
                    PhoneNumber = "",
                    Role = false
                };
                //המרה באותיות קטנות    
                var settings = new JsonSerializerSettings
                {
                    ContractResolver =
                        new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };

                string json = JsonConvert.SerializeObject(login, settings);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                //אם קיים בכלל
                HttpResponseMessage response =
                    client.PostAsync(address + "/Volunteer/IfExist", content).Result;

                string data = response.Content.ReadAsStringAsync().Result;

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("שגיאה: " + data);
                    return;
                }
                //
                bool exists = JsonConvert.DeserializeObject<bool>(data);
                if (!exists)
                {
                    MessageBox.Show("לא נמצא משתמש");
                    return;
                }
                  HttpResponseMessage response2 =
                    client.GetAsync(address + "/Volunteer/Get/login/" + login.Tz).Result;
                MessageBox.Show("נשלח: " + login.Tz);
                string fullData = response2.Content.ReadAsStringAsync().Result;
                MessageBox.Show($"קוד סטטוס: {(int)response2.StatusCode}\nסיבה: {response2.ReasonPhrase}");

                if (!response2.IsSuccessStatusCode)
                {
                    MessageBox.Show("שגיאה בשליפת נתונים");
                    return;
                }

                var current =
                    JsonConvert.DeserializeObject<VolunteerDTO>(fullData);
                //עדכון הנוכחי
                Cach.currentVolunteer = current;
              //אם מנהל
                if (current.Role)
                {
                    NavigationService.Navigate(new AddCallWindow_new());
                }
                else
                {
                    NavigationService.Navigate(new ListCalls());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה: " + ex.Message);
            }
        }
    }
}

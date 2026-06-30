using DTO;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WPFMiniAplication
{
    /// <summary>
    /// Interaction logic for createNewVolunteer.xaml
    /// </summary>
    public partial class createNewVolunteer : Page
    {
        //יודע לקרוא לשירותים
        HttpClient client = new HttpClient();
        //הכנת כתובת להתחברות
        string address = "https://localhost:7227/api";
        public createNewVolunteer()
        {
            InitializeComponent();
        }
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        public static bool IsValidIsraeliAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return false;

            // חייב להכיל לפחות 3 מילים: רחוב, מספר, עיר
            var parts = address.Trim().Split(' ');
            if (parts.Length < 3)
                return false;

            // בדיקה שיש מספר בית
            if (!int.TryParse(parts[parts.Length - 2], out _))
                return false;

            // בדיקה שיש עיר (המילה האחרונה)
            if (string.IsNullOrWhiteSpace(parts[^1]))
                return false;

            return true;
        }
        private void ccreateNewVolunteer(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtAdress.Text) ||
     string.IsNullOrWhiteSpace(txtEmail.Text) ||
     string.IsNullOrWhiteSpace(txtFullName.Text) ||
     string.IsNullOrWhiteSpace(txtMaxDistance.Text) ||
     string.IsNullOrWhiteSpace(txtPassword.Text) ||
     string.IsNullOrWhiteSpace(txtPhonNumber.Text) ||
     string.IsNullOrWhiteSpace(txtRole.Text) ||
     string.IsNullOrWhiteSpace(txtTz.Text))
            {
                MessageBox.Show("Please fill in all fields");
            }
            else
            {
                VolunteerDTO v = new VolunteerDTO()
                {
                    Tz = txtTz.Text,
                    FullName = txtFullName.Text,
                    Password = txtPassword.Text,
                    Address = txtAdress.Text,
                    Email = txtEmail.Text,
                    MaxDistance = Convert.ToDouble(txtMaxDistance.Text),
                    Role = txtRole.Text == "1" ? true : false, 
                    PhoneNumber = txtPhonNumber.Text
                };
                //בדיקות תקינות
                if (!v.PhoneNumber.StartsWith("0") && v.PhoneNumber.Length > 10)
                { MessageBox.Show("the Phone number is not valid");
                    return;

                }
                   
                if (!IsValidEmail(v.Email))
                {
                    MessageBox.Show("Invalid email ");
                    return;
                }
                    
                if (!IsValidIsraeliAddress(v.Address))
                {
                    MessageBox.Show("addres not valid");
                    return;
                }
                   
                try
                {
                    //הפיכת האובייקט להכנה לשליחה
                    string sringParameter = JsonConvert.SerializeObject(v);
                    //מכניסה למזוודה את הפרמטר המשוטח , מגדירה שהנתונים בקידוד לכל השפות, והוא מסוג json
                    StringContent content = new StringContent(sringParameter, Encoding.UTF8, "application/json");
                    //הבקשה נשלחת לשרת
                    HttpResponseMessage response = client.PostAsync(address + "/Volunteer/post", content).Result;

                    if (response.IsSuccessStatusCode)//האם הפעולה הצליחה
                    {
                        string data = response.Content.ReadAsStringAsync().Result;

                        bool isOK = bool.TryParse(data, out bool b);
                        if (isOK)
                        {
                            if (b)
                                MessageBox.Show("השמירה הצליחה");
                            else
                                MessageBox.Show("השמירה לא הצליחה");
                        }
                        else
                            MessageBox.Show("השמירה לא הצליחה");
                    }
                    else
                        MessageBox.Show("השליחה לשירות לא הצליחה");
                }
                catch
                {
                    MessageBox.Show("השליחה לשירות לא הצליחה");
                }
            }
        }
    }
}

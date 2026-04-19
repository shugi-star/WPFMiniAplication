using ChesedEntities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFMiniAplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AddCallWindow : Window
    {
     
        public Call NewCall { get; set; }

        public AddCallWindow()
        {
            InitializeComponent();

            //CategoryBox.ItemsSource = Enum.GetValues(typeof(Category));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            NewCall = new Call
            { 
                // todo: לחבר לפונקציה שממלאת אוטומטית קווי אורך ורוחב
                //category = (Category)CategoryBox.SelectedItem,
                Description = DescriptionBox.Text,
                Address = AddressBox.Text,

                OpenDate = OpenDatePicker.SelectedDate ?? DateTime.Now,
                TargetDate = TargetDatePicker.SelectedDate,
                VolunteerId = int.Parse(VolunteerIdBox.Text)
            };

            sendCallToWebApi();

            MessageBox.Show("Call created!");
          
        }

        private void sendCallToWebApi()
        {
            try
            {
                //יודע לקרוא לשירותים
                HttpClient client = new HttpClient();
                //הכנת כתובת להתחברות
                string address = "https://localhost:7227/";
                //הפיכת האובייקט להכנה לשליחה
                string sringParameter = JsonConvert.SerializeObject(NewCall);
                //מכניסה למזוודה את הפרמטר המשוטח , מגדירה שהנתונים בקידוד לכל השפות, והוא מסוג json
                StringContent content = new StringContent(sringParameter, Encoding.UTF8, "application/json");
                //הבקשה נשלחת לשרת
                HttpResponseMessage response = client.PostAsync(address, content).Result;

                if (response.IsSuccessStatusCode)//האם הפעולה הצליחה
                {
                    string data = response.Content.ReadAsStringAsync().Result;

                    bool isOK = bool.TryParse(data, out bool b);
                    if (isOK && b)
                        MessageBox.Show("השמירה הצליחה");
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
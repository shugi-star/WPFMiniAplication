using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_chesedWroled;

namespace WPFMiniAplication
{
    /// <summary>
    /// Interaction logic for ListCalls.xaml
    /// </summary>
    public partial class ListCalls : Page
    {

        //יודע לקרוא לשירותים
        HttpClient client = new HttpClient();
        //הכנת כתובת להתחברות
        string address = "https://localhost:7227/api";

        //לשמירה של מספר הקטגוריה לפי השם שנבחר שמתאים לה
        public int SelectedCategoryId { get; set; }
        //לשמירה של המתנדב הנוכחי
        int IdCurrentVolunteer;
        public ListCalls()
        {
            InitializeComponent();
            //טעינה של רשימת הקטגוריות
            LoadCategories();
            //טעינה של הקריאות
            LoadAllCallsForStart();
            lblHello.Content += Cach.currentVolunteer.FullName;
            this.IdCurrentVolunteer = Cach.currentVolunteer.Id;
        }
        //טעינה של כל הקריאות להתחלה
        public async void LoadAllCallsForStart()
        {
            HttpResponseMessage response = client.GetAsync("https://localhost:7227/api/Call/AllCall").Result;
            var errorBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)//האם הפעולה הצליחה

            { string json = response.Content.ReadAsStringAsync().Result;

                List<CallDTO> AllCalls =
                    JsonConvert.DeserializeObject<List<CallDTO>>(json);
                //טעינה של הקריאות בפועל על הטבלה
                CallsListView.ItemsSource = AllCalls;
            }

            else
            {
                MessageBox.Show(
                "קוד שגיאה: " + (int)response.StatusCode + "\n" +
                "סיבה: " + response.ReasonPhrase + "\n" +
                "פרטי שגיאה: " + errorBody
            );

            MessageBox.Show("טעינת הקריאות לא הצליחה");
            }
           
            }
        
        private void LoadCategories()
        {
            HttpResponseMessage response = client.GetAsync("https://localhost:7227/api/Call/allCategories").Result;
            if (response.IsSuccessStatusCode)//האם הפעולה הצליחה
            {
                string json = response.Content.ReadAsStringAsync().Result;

                List<CategoryDTO> categories =
                    JsonConvert.DeserializeObject<List<CategoryDTO>>(json);

                //MessageBox.Show(categories.Count.ToString());
                //הקומבבוקס יתחבר לרשימה הזו
                cmbCategory.ItemsSource = categories;
                cmbCategory.DisplayMemberPath = "CategoryName";
                cmbCategory.SelectedValuePath = "IdCategory";

            }
            else
                MessageBox.Show("טעינת הקטגוריות לא הצליחה");
        }
        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortingAndLoadCalls();
        }
        private void AddCallToVolunteer(object sender, System.Windows.RoutedEventArgs e)
        {
            //שמירת הקריאה שנבחרה
            CallDTO SelectedCall = CallsListView.SelectedItem as CallDTO;

            //הוספה לקריאה מתנדב

            //הפיכת האובייקט להכנה לשליחה
            string sringParameter = JsonConvert.SerializeObject(SelectedCall);
            //מכניסה למזוודה את הפרמטר המשוטח , מגדירה שהנתונים בקידוד לכל השפות, והוא מסוג json
            StringContent content = new StringContent(sringParameter, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync("https://localhost:7227/api/Call/" + IdCurrentVolunteer, content).Result;
            if (response.IsSuccessStatusCode)//האם הפעולה הצליחה
            {
                string json = response.Content.ReadAsStringAsync().Result;
                bool isOK = bool.TryParse(json, out bool b);
                if (isOK)
                {
                    if (b)
                    {
                    MessageBox.Show("ההוספה הצליחה");
                        //טעינה מחודשת של הקריאות
                        //עדיין לא מעלים לבד את הקריאה!
                        //צריך לראות למה
                        SortingAndLoadCalls();
                    }
                        
                    else
                        MessageBox.Show("ההוספה לא הצליחה");
                }
                //טעינה מחודשת של הקריאות
                SortingAndLoadCalls();

            }
        }
        public void SortingAndLoadCalls()
        {
            try
            {
                if (cmbCategory.SelectedValue != null)
                {
                    SelectedCategoryId = (int)cmbCategory.SelectedValue;
                    //קבלת רשימת הקריאות  עם סינון לפי הקטגוריה

                    HttpResponseMessage response = client.GetAsync("https://localhost:7227/api/Call/" + SelectedCategoryId).Result;
                    if (response.IsSuccessStatusCode)//האם הפעולה הצליחה
                    {
                        string json = response.Content.ReadAsStringAsync().Result;

                        List<CallDTO> calls =
                            JsonConvert.DeserializeObject<List<CallDTO>>(json);

                        //הקומבבוקס יתחבר לרשימה הזו
                        //cmbCategory.ItemsSource = calls;
                        //  טעינה של הקריאות אחרי המיון
                        CallsListView.ItemsSource = calls;
                        LoadCategories();
                    }
                    else
                        MessageBox.Show("טעינת הקריאות לא הצליחה");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("השליחה לשירות לא הצליחה");
            }
        }


        private void removeCall(object sender, System.Windows.RoutedEventArgs e)
        {
            //שמירת הקריאה שנבחרה
            CallDTO SelectedCall = CallsListView.SelectedItem as CallDTO;

               //מחיקת קריאה
               //בעיקרון צריך להעביר רק למנהל את ההרשאה למחוק קריאה
            HttpResponseMessage response = client.DeleteAsync("https://localhost:7227/api/Call/"+SelectedCall.Id).Result;
            if (response.IsSuccessStatusCode)//האם הפעולה הצליחה
            {
                string json = response.Content.ReadAsStringAsync().Result;
                bool isOK = bool.TryParse(json, out bool b);
                if (isOK)
                {
                    if (b)
                        MessageBox.Show("המחיקה הצליחה");
                    else
                        MessageBox.Show("המחיקה לא הצליחה");
                }
                //טעינה מחודשת של הקריאות
                SortingAndLoadCalls();

            }
        }
    }

}

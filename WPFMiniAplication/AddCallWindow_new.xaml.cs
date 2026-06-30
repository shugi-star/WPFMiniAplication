using DTO;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFMiniAplication
{
    /// <summary>
    /// Interaction logic for AddCallWindow_new.xaml
    /// </summary>
    public partial class AddCallWindow_new : Page
    {
        //לשמירה של מספר הקטגוריה לפי השם שנבחר שמתאים לה
        public int SelectedCategoryId { get; set; }
        //יודע לקרוא לשירותים
        HttpClient client = new HttpClient();
        //הכנת כתובת להתחברות
        string address = "https://localhost:7227/api";
        CallDTO call;
        public AddCallWindow_new()
        {
            InitializeComponent();

            //טעינה של רשימת הקטגוריות
            LoadCategories();
        }
        //עדכון הטבלה מהדטה בייס
        private void LoadCategories()
        {
            HttpResponseMessage response = client.GetAsync("https://localhost:7227/api/Call/allCategories").Result;
            if (response.IsSuccessStatusCode)//האם הפעולה הצליחה
            {
                string json = response.Content.ReadAsStringAsync().Result;

                List<CategoryDTO> categories =
                    JsonConvert.DeserializeObject<List<CategoryDTO>>(json);

                //MessageBox.Show(categories.Count.ToString());
                //MessageBox.Show(categories[0].CategoryName ?? "NULL");
                ////הקומבבוקס יתחבר לרשימה הזו
                cmbCategory.ItemsSource = categories;
                cmbCategory.DisplayMemberPath = "CategoryName";
                cmbCategory.SelectedValuePath = "IdCategory";
            }
            else
                    MessageBox.Show("טעינת הקטגוריות לא הצליחה");
        }
        private void saveNewCall(object sender, RoutedEventArgs e)
        {
            //האם הכל מלא
            if (string.IsNullOrWhiteSpace(txtDescription.Text) ||
                 
                 string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(cmbCategory.Text))
            {
                MessageBox.Show("Please fill in all fields");
            }

                else
                {
                call = new CallDTO()
                {
                    //category = new CategoryDTO() { IdCategory = SelectedCategoryId, CategoryName = ((CategoryDTO)cmbCategory.SelectedItem).CategoryName },
                    IdCategory = SelectedCategoryId,
                    Address = txtAddress.Text,
                    Description = txtDescription.Text,
                    //כרגע זה מה שיש באריך הסופי
                    TargetDate = DateTime.Now,
                    OpenDate = DateTime.Now,
                    IdVolunteer = null
                };
                    //שליחה לשרת
                    sendCallToWebApi();
                }
            }
        
        private async Task sendCallToWebApi()
        {
            try
            {
                string json = JsonConvert.SerializeObject(call);
                MessageBox.Show("json: " + json);
                StringContent content = new StringContent(json,Encoding.UTF8,"application/json");
                HttpResponseMessage response =client.PostAsync("https://localhost:7227/api/Call", content).Result;
                
                MessageBox.Show("address:   " + address + "/Call " + "content: " + content);
                if (!response.IsSuccessStatusCode)
                {
                    return;
                }

                string result = await response.Content.ReadAsStringAsync();

                if (!bool.TryParse(result, out bool success))
                {
                    MessageBox.Show("התקבלה תשובה לא תקינה מהשרת.");
                    return;
                }

                MessageBox.Show(success ? "השמירה הצליחה." : "השמירה נכשלה.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("אירעה שגיאה:\n" + ex.Message);
            }
        }
        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategory.SelectedValue != null)
            {
                SelectedCategoryId = (int)cmbCategory.SelectedValue;
            }
        }
        public static bool IsDateValid(string dateStr)
        {
            // פורמט התאריך הרצוי: יום-חודש-שנה
            string format = "d-M-yyyy";
            DateTime tempDate;

            // מנסה לפרס את המחרוזת לפי הפורמט המדויק
            bool valid = DateTime.TryParseExact(
                dateStr,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out tempDate
            );
            return valid;
        }

        private void GoToCreateVolunteer(object sender, RoutedEventArgs e)
        {
            //מעבר להוספת מתנדב
            createNewVolunteer nc = new createNewVolunteer();
            NavigationService.Navigate(nc);
        }
    }
}

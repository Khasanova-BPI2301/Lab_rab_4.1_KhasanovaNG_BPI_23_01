using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Lab_rab_4._1_KhasanovaNG_BPI_23_01.Model;
using Lab_rab_4._1_KhasanovaNG_BPI_23_01.ViewModel;

namespace Lab_rab_4._1_KhasanovaNG_BPI_23_01
{
    /// <summary>
    /// Логика взаимодействия для WindowInputDialog.xaml
    /// </summary>
    public partial class WindowInputDialog : Window
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string SelectedRole { get; set; }
        public DateTime Birthday { get; set; }
        public List<string> Roles { get; set; }
        public WindowInputDialog(string title, PersonDPO existing = null)
        {
            InitializeComponent();

            Title = title;
            var vmRole = new RoleViewModel();
            Roles = new List<string>();
            foreach (var r in vmRole.ListRole)
                Roles.Add(r.NameRole);

            if (existing != null)
            {
                LastName = existing.LastName;
                FirstName = existing.FirstName;
                SelectedRole = existing.Role;
                Birthday = existing.Birthday;
            }
            else
            {
                Birthday = DateTime.Today;
                SelectedRole = Roles.Count > 0 ? Roles[0] : "";
            }

            DataContext = this;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

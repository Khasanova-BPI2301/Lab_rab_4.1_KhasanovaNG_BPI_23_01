using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Lab_rab_4._1_KhasanovaNG_BPI_23_01.Helper;
using Lab_rab_4._1_KhasanovaNG_BPI_23_01.Model;
using Lab_rab_4._1_KhasanovaNG_BPI_23_01.ViewModel;

namespace Lab_rab_4._1_KhasanovaNG_BPI_23_01.View
{
    /// <summary>
    /// Логика взаимодействия для WindowEmployee.xaml
    /// </summary>
    public partial class WindowEmployee : Window
    {
        private ObservableCollection<PersonDPO> persons;
        private PersonViewModel vmPerson;
        private RoleViewModel vmRole;

        public WindowEmployee()
        {
            InitializeComponent();
            vmPerson = new PersonViewModel();
            vmRole = new RoleViewModel();

            List<Role> roles = new List<Role>();
            foreach (Role r in vmRole.ListRole)
            {
                roles.Add(r);
            }

            persons = new ObservableCollection<PersonDPO>();
            FindRole finder;

            foreach (var p in vmPerson.ListPerson)
            {
                finder = new FindRole(p.RoleId);
                Role rol = roles.Find(new Predicate<Role>(finder.RolePredicate));
                persons.Add(new PersonDPO
                {
                    Id = p.Id,
                    Role = rol.NameRole,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Birthday = p.Birthday
                });
            }

            lvEmployee.ItemsSource = persons;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new WindowInputDialog("Добавление сотрудника");
            if (dialog.ShowDialog() == true)
            {
                int newId = persons.Count > 0 ? persons[persons.Count - 1].Id + 1 : 1;
                persons.Add(new PersonDPO
                {
                    Id = newId,
                    LastName = dialog.LastName,
                    FirstName = dialog.FirstName,
                    Role = dialog.SelectedRole,
                    Birthday = dialog.Birthday
                });

                // Также добавляем в исходный список (если нужно)
                vmPerson.ListPerson.Add(new Person
                {
                    Id = newId,
                    LastName = dialog.LastName,
                    FirstName = dialog.FirstName,
                    RoleId = GetRoleIdByName(dialog.SelectedRole),
                    Birthday = dialog.Birthday
                });
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvEmployee.SelectedItem as PersonDPO;
            if (selected == null)
            {
                MessageBox.Show("Выберите сотрудника для редактирования.");
                return;
            }

            var dialog = new WindowInputDialog("Редактирование", selected);
            if (dialog.ShowDialog() == true)
            {
                selected.LastName = dialog.LastName;
                selected.FirstName = dialog.FirstName;
                selected.Role = dialog.SelectedRole;
                selected.Birthday = dialog.Birthday;

                // Обновляем в исходном списке
                var person = new List<Person>(vmPerson.ListPerson).Find(p => p.Id == selected.Id);
                if (person != null)
                {
                    person.LastName = selected.LastName;
                    person.FirstName = selected.FirstName;
                    person.RoleId = GetRoleIdByName(selected.Role);
                    person.Birthday = selected.Birthday;
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvEmployee.SelectedItem as PersonDPO;
            if (selected == null)
            {
                MessageBox.Show("Выберите сотрудника для удаления.");
                return;
            }

            if (MessageBox.Show($"Удалить {selected.LastName} {selected.FirstName}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                persons.Remove(selected);
                var personToRemove = new List<Person>(vmPerson.ListPerson).Find(p => p.Id == selected.Id);
                if (personToRemove != null)
                {
                    vmPerson.ListPerson.Remove(personToRemove);
                }
            }
        }

        private int GetRoleIdByName(string roleName)
        {
            foreach (var r in vmRole.ListRole)
                if (r.NameRole == roleName)
                    return r.Id;
            return 0;
        }
    }
}
    

    


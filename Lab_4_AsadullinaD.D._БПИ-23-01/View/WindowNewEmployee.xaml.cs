﻿using Lab_4_AsadullinaD.D._БПИ_23_01.Model;
using Lab_4_AsadullinaD.D._БПИ_23_01.ViewModel;
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

namespace Lab_4_AsadullinaD.D._БПИ_23_01.View
{
    /// <summary>
    /// Логика взаимодействия для WindowNewEmployee.xaml
    /// </summary>
    public partial class WindowNewEmployee : Window
    {
        public WindowNewEmployee()
        {
            InitializeComponent();

            // Загрузка списка должностей для ComboBox
            var roleVm = new RoleViewModel();
            CbRole.ItemsSource = roleVm.ListRole;

            // При редактировании можно дополнительно выбрать текущую должность:
            if (DataContext is PersonDpo person)
            {
                CbRole.SelectedValue = person.RoleId; // Добавь RoleId в PersonDpo, если нет
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PersonDpo person)
            {
                if (CbRole.SelectedItem is Role selectedRole)
                {
                    person.RoleId = selectedRole.Id;  // Нужно, чтобы знать ID должности
                    person.RoleName = selectedRole.NameRole;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите должность.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Можно добавить валидацию остальных полей

                DialogResult = true;
            }
        }
    }
}

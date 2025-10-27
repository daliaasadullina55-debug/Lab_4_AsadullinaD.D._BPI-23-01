using Lab_4_AsadullinaD.D._БПИ_23_01.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lab_4_AsadullinaD.D._БПИ_23_01.Helper;
using Lab_4_AsadullinaD.D._БПИ_23_01.View;
using Lab_4_AsadullinaD.D._БПИ_23_01.Model;
using System.Windows;

namespace Lab_4_AsadullinaD.D._БПИ_23_01.ViewModel
{
    public class RoleViewModel : INotifyPropertyChanged
    {
        private Role selectedRole;
        public Role SelectedRole
        {
            get => selectedRole;
            set { selectedRole = value; OnPropertyChanged();  }
        }

        public ObservableCollection<Role> ListRole { get; set; } = new ObservableCollection<Role>();

        public RoleViewModel()
        {
            ListRole.Add(new Role { Id = 1, NameRole = "Директор" });
            ListRole.Add(new Role { Id = 2, NameRole = "Бухгалтер" });
            ListRole.Add(new Role { Id = 3, NameRole = "Менеджер" });
        }

        public int MaxId() 
        {
            int max = 0;
            foreach (var r in this.ListRole)
            {
                if (max < r.Id)
                {
                    max = r.Id;
                };
            }
            return max;

        }

        private RelayCommand addRole;
        public RelayCommand AddRole => addRole ?? (addRole = new RelayCommand(obj => {
            var wnRole = new WindowNewRole { Title = "Новая должность" };
            int max = MaxId() + 1;
            var role = new Role { Id = max };
            wnRole.DataContext = role;
            if (wnRole.ShowDialog() == true) { ListRole.Add(role); SelectedRole = role; }
        }));

        private RelayCommand editRole;
        public RelayCommand EditRole => editRole ?? (editRole = new RelayCommand(obj => {
            if (SelectedRole == null) return;

            var editWindow = new WindowNewRole { Title = "Редактирование должности" };
            var roleCopy = new Role { Id = SelectedRole.Id, NameRole = SelectedRole.NameRole };
            editWindow.DataContext = roleCopy;

            if (editWindow.ShowDialog() == true)
            {
                SelectedRole.NameRole = roleCopy.NameRole;
            }
        }));

        private RelayCommand deleteRole;
        public RelayCommand DeleteRole => deleteRole ?? (deleteRole = new RelayCommand(obj => {
            if (SelectedRole == null) return;

            ListRole.Remove(SelectedRole);
            SelectedRole = ListRole.FirstOrDefault();
        }));

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string p = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}

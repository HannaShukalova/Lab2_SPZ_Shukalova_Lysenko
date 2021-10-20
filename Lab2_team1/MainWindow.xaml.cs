using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Lab2_team1
{
    public partial class MainWindow : Window
    {
        private List<StudentCampus> copiedStudentCampuses = new List<StudentCampus>();
        private StudentCampus studentCampus = new StudentCampus();

        public MainWindow()
        {
            InitializeComponent();

            TBUniName.Text = studentCampus.UniversityName;
            TBCampusAddress.Text = studentCampus.Address;
            LbRoomCount.Content = studentCampus.RoomCount;
            LbStaffCount.Content = studentCampus.RoomCount;
            LbStudentCount.Content = studentCampus.StudentCount;
            LbStudentDebt.Content = studentCampus.StudentDebt;
            LbCalculate.Content = $"Посчитать (тариф {studentCampus.PayPerMonthPerStudent} дол. в мес. на чел.)";
        }
        private void TBUniName_TextChanged(object sender, TextChangedEventArgs e) => studentCampus.UniversityName = TBUniName.Text;
        private void TBCampusAddress_TextChanged(object sender, TextChangedEventArgs e) => studentCampus.Address = TBCampusAddress.Text;

        private void BAddRooms_Click(object sender, RoutedEventArgs e)
        {
            if (TBAddRooms.Text.Length == 0) return;

            int tryParseResult;
            int.TryParse(TBAddRooms.Text, out tryParseResult);
            TBAddRooms.Text = "";

            if (tryParseResult <= 0)
            {
                MessageBox.Show("Не бывает такого числа комнат!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                
            studentCampus.AddRooms(tryParseResult);
            LbRoomCount.Content = studentCampus.RoomCount;
        }
        private void BOkStudents_Click(object sender, RoutedEventArgs e)
        {
            if (TBStudentMove.Text.Length == 0) return;

            int tryParseResult;
            int.TryParse(TBStudentMove.Text, out tryParseResult);
            TBStudentMove.Text = "";

            if (tryParseResult <= 0)
            {
                MessageBox.Show("Не бывает такого числа студентов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(RBMoveIn.IsChecked == true)
                studentCampus.MoveStudents(tryParseResult, StudentCampus.StudentMove.MoveIn);
            else
                studentCampus.MoveStudents(tryParseResult, StudentCampus.StudentMove.MoveOut);

            LbStudentDebt.Content = studentCampus.StudentDebt;
            LbStudentCount.Content = studentCampus.StudentCount;
        }
        private void BCampusInfo_Click(object sender, RoutedEventArgs e) => TBCampusInfo.Text = studentCampus.ToString();
        private void BCloneObject_Click(object sender, RoutedEventArgs e)
        {
            copiedStudentCampuses.Add(studentCampus.Clone() as StudentCampus);
            MessageBox.Show("Копия успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void BAddCafeteria_Click(object sender, RoutedEventArgs e)
        {
            MyExtentions.AddCafeteria(studentCampus);
            MessageBox.Show("Столовая успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            LbStaffCount.Content = studentCampus.StaffCount;
            LbCalculate.Content = $"Посчитать (тариф {studentCampus.PayPerMonthPerStudent} дол. в мес. на чел.)";
            LbStudentDebt.Content = studentCampus.StudentDebt;
        }
        private void BСCalculateProfit_Click(object sender, RoutedEventArgs e)
        {
            switch (ComboBox.SelectedIndex)
            {
                case 0: LbStudentDebtForPeriod.Content = studentCampus.CalculateProfit(StudentCampus.Period.Month); break;
                case 1: LbStudentDebtForPeriod.Content = studentCampus.CalculateProfit(StudentCampus.Period.SixthMonths); break;
                case 2: LbStudentDebtForPeriod.Content = studentCampus.CalculateProfit(StudentCampus.Period.Year); break;
            }
        }
    }
}

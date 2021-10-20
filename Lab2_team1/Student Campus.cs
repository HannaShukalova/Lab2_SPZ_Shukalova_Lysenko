using System;

namespace Lab2_team1
{
    class StudentCampus: ICloneable
    {
        private string _universityName = "";
        private string _address = "";
        private int _roomCount = 0;
        private int _staffCount = 0;
        private int _studentCount = 0;
        private int _studentDebt = 0; //оплата за проживание
        
        private int _payPerMonthPerStudent;
        public enum Period { Month, SixthMonths, Year};
        public enum StudentMove { MoveIn, MoveOut };
        private enum PriceLimits { MinPrice = 0, MaxPrice = 1000 };

        public string UniversityName
        {
            get { return _universityName; }
            set { _universityName = value.Length > 0 ? value : "University name"; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value.Length > 0 ? value : "Campus address address"; }
        }
        public int RoomCount
        {
            get { return _roomCount; }
            set { _roomCount = value > 0 ? value : 0; }
        }
        public int StaffCount
        {
            get { return _staffCount; }
            set { _staffCount = value > 0 ? value : 0; }
        }
        public int StudentCount
        {
            get { return _studentCount; }
            set { _studentCount = value > 0 ? value : 0; Countdebt(); }
        }
        public int StudentDebt
        {
            get { return _studentDebt; }
            set { _studentDebt = value > 0 ? value : 0; }
        }
        public int PayPerMonthPerStudent
        {
            get { return _payPerMonthPerStudent; }
            set { _payPerMonthPerStudent = value > 0 ? (value > (int)PriceLimits.MaxPrice ? (int)PriceLimits.MaxPrice : value) : 0; Countdebt(); }
        }

        public void AddRooms(int roomCount) => RoomCount += roomCount;
        public void MoveStudents(int numberOfStudents, StudentMove studentMove)
        {
            switch(studentMove)
            {
                case StudentMove.MoveIn: StudentCount += numberOfStudents; break;
                case StudentMove.MoveOut: StudentCount -= numberOfStudents; break;
            }
        }
        public StudentCampus(string universityName = "", string address = "", int roomCount = 0, int staffCount = 0, int studentCount = 0)
        {
            UniversityName = universityName;
            Address = address;
            RoomCount = roomCount;
            StaffCount = staffCount;
            StudentCount = studentCount;

            Random random = new Random();
            PayPerMonthPerStudent = random.Next((int)PriceLimits.MinPrice, (int)PriceLimits.MaxPrice);
        }
        public override string ToString() =>
            $"Student campus is named {UniversityName}, its address - {Address}.\n" +
            $"It has {RoomCount} rooms, {StudentCount} students, and {StaffCount} staff members.\n" +
            $"Students owe {StudentDebt} dollars to the university.";
        public object Clone() => base.MemberwiseClone();
        public int CalculateProfit(Period period)
        {
            int sumOfMoney = 0;

            switch(period)
            {
                case Period.Month: sumOfMoney = PayPerMonthPerStudent * StudentCount; break;
                case Period.SixthMonths: sumOfMoney = 6 * PayPerMonthPerStudent * StudentCount; break;
                case Period.Year: sumOfMoney = 12 * PayPerMonthPerStudent * StudentCount; break;
            }

            return sumOfMoney;
        }

        private void Countdebt() => StudentDebt = StudentCount * PayPerMonthPerStudent;
    }
    static class MyExtentions
    {
        public static void AddCafeteria(this StudentCampus studentCampus)
        {
            studentCampus.PayPerMonthPerStudent = (int)(1.2 * studentCampus.PayPerMonthPerStudent);
            studentCampus.StaffCount += 5;
        }
    }
}

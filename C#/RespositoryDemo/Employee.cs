using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespositoryDemo
{
    /// <summary>
    /// 实体类Employee
    /// </summary>
    public class Employee
    {
        #region 私有字段
        private int _reportsTo;
        private int _employeeID;
        private string _lastName;
        private string _firstName;
        private string _title;
        private string _titleOfCourtesy;
        private DateTime _birthDate;
        private DateTime _hireDate;
        private string _address;
        private string _city;
        private string _region;
        private string _postalCode;
        private string _country;
        private string _homePhone;
        private string _extension;
        private byte[] _photo;
        private string _notes;
        private string _photoPath;
        #endregion

        #region 公开属性
        public int ReportsTo
        {
            get { return _reportsTo; }
            set { _reportsTo = value; }
        }
        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        public string TitleOfCourtesy
        {
            get { return _titleOfCourtesy; }
            set { _titleOfCourtesy = value; }
        }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }
        public DateTime HireDate
        {
            get { return _hireDate; }
            set { _hireDate = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public string Region
        {
            get { return _region; }
            set { _region = value; }
        }
        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string HomePhone
        {
            get { return _homePhone; }
            set { _homePhone = value; }
        }
        public string Extension
        {
            get { return _extension; }
            set { _extension = value; }
        }
        public byte[] Photo
        {
            get { return _photo; }
            set { _photo = value; }
        }
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }
        public string PhotoPath
        {
            get { return _photoPath; }
            set { _photoPath = value; }
        }
        #endregion
    }
}

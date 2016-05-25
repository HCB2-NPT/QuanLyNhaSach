using QuanLyNhaSach.Managers;
using QuanLyNhaSach.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Adapters
{
    public class AccessLevelAdapter
    {
        private static ObservableCollection<AccessLevel> _accessLevels = null;

        public static ObservableCollection<AccessLevel> GetAll()
        {
            if (_accessLevels == null)
            {
                try
                {
                    var reader = DataConnector.ExecuteQuery(@"SELECT MaPhanQuyen, TenPhanQuyen FROM PhanQuyen");
                    if (reader != null)
                    {
                        _accessLevels = new ObservableCollection<AccessLevel>();
                        while (reader.Read())
                        {
                            _accessLevels.Add(new AccessLevel(reader.GetInt32(0), (string)reader.GetValueDefault(1, null)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorManager.Current.DataCantBeRead.Call(ex.Message);
                }
            }
            return _accessLevels;
        }

        public static AccessLevel GetAcessLevelById(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }
    }
}

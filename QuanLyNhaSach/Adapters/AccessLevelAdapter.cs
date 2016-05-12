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
        //=========================
        private static ObservableCollection<AccessLevel> _accessLevels;
        public static ObservableCollection<AccessLevel> AccessLevels
        {
            get
            {
                if (_accessLevels == null)
                    _accessLevels = Adapters.AccessLevelAdapter.GetAll();
                return _accessLevels;
            }
        }
        //=========================

        public static ObservableCollection<AccessLevel> GetAll()
        {
            ObservableCollection<AccessLevel> result = null;
            try
            {
                var reader = DataConnector.ExecuteQuery(@"select maphanquyen, tenphanquyen from phanquyen");
                if (reader != null)
                {
                    result = new ObservableCollection<AccessLevel>();
                    while (reader.Read())
                    {
                        result.Add(new AccessLevel(reader.GetInt32(0))
                        {
                            Name = (string)reader.GetValueDefault(1, null)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.Current.DataCantBeRead.Call(ex.Message);
            }
            return result;
        }

        public static AccessLevel GetAcessLevelById(int id)
        {
            return AccessLevels.FirstOrDefault(x => x.ID == id);
        }
    }
}

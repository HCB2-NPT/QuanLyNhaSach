using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Assets.Scripts
{
    public class Cursor
    {
        public static System.Drawing.Point GetCursorPosition(){
            return System.Windows.Forms.Cursor.Position;
        }
    }
}

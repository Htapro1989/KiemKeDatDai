using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.EntitiesDb
{
    public class DM_BieuMau
    {
        public int Id { get; set; }
        public string KyHieu { get; set; }
        public string NoiDung { get; set; }
        public bool CapTrungUong { get; set; }
        public bool CapTinh { get; set; }
        public bool CapHuyen { get; set; }
        public bool CapXa { get; set; }
        public bool Activated { get; set; }
    }
}

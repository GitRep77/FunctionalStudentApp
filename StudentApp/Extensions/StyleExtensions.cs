using System.Windows.Forms;
using System.Drawing;
using static StudentApp_FunctionalProgramming_.Prelude;

namespace StudentApp_FunctionalProgramming_.Extensions
{
    public static class StyleExtensions
    {
        public static DataGridView ApplyStyle(this DataGridView dgv)
        {
            return dgv.With(o => {
                o.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold, GraphicsUnit.Point);
                o.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDark;
                o.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                o.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                o.DefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Regular, GraphicsUnit.Point);
                o.DefaultCellStyle.BackColor = Color.Empty;
                o.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.ControlLight;
                o.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                o.GridColor = SystemColors.ControlDarkDark;
            });
        }
    }
}

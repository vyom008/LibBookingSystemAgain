
namespace LibBooking.Models
{
    public class EmailValidationViewModel
    {
        public string Room { get; set; }
        public int ID { get; set; }  // 修改为ID，匹配数据库字段
        public int Time { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
    }

}

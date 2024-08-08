namespace HospitalApp.DLL.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Image { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}

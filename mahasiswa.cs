namespace Model.Mahasiswa
{
    public class Mahasiswa
    {
        public int Id { get; set; }
        private String nama { get; set; }
        private String nim { get; set; }

        public String Nama
        {
            get { return nama; }
            set { nama = value; }
        }

        public String Nim
        {
            get { return nim; }
            set { nim = value; }
        }
    }
}
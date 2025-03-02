namespace Dynamic_WebForm_Generator.Models
{
    public class FormHTMLModel
    {
        public int FormID { get; set; }
        public int UserID { get; set; }
        public string FormHTML{ get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}

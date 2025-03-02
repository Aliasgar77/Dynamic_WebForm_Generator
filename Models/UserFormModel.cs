namespace Dynamic_WebForm_Generator.Models
{
    public class UserFormModel
    {
        public int FieldID { get; set; }
        public int UserID { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string FieldLabel { get; set; }
        public string Options { get; set; }
        public int IsRequired { get; set; }
        public string FieldValue { get; set; }
    }
}

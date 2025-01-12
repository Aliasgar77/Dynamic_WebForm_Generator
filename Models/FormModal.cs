namespace Dynamic_WebForm_Generator.Models
{
    public class FormModal
    {
        public int formId { get; set; } = 0;
        public int userId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string FieldLabel { get; set; }
        public string Options { get; set; }
        public int isRequired { get; set; }



    }
}

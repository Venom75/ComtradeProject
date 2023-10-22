using System.ComponentModel.DataAnnotations.Schema;

namespace ComtradeProject.Model
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public string Reward { get; set; }
    }
}

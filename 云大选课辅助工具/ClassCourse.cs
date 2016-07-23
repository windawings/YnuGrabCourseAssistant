namespace 云大选课辅助工具
{
    public class ClassCourse
    {
        public ClassCourse(string id, string name, string remark)
        {
            Id = id;
            Name = name;
            Remark = remark;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }
    }
}


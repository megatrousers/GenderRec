using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderRec01.Model
{
    class SubjectBatch
    {
        int id;
        string name;
        Dictionary<string, Subject> subjects;
        BatchType type;

        public SubjectBatch()
        {
            subjects = new Dictionary<string, Subject>();
            type = BatchType.TESTING;
        }

        public bool Add(Subject s)
        {
            bool success = false; 
            if(!subjects.Keys.Contains(s.Name))
            { 
                subjects.Add(s.Name, s);
                success = true;
            }
            return success;
        }

        #region Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Dictionary<string, Subject> Subjects
        {
            get { return subjects; }
        }

        public BatchType Type
        {
            get { return type; }
            set { type = value; }
        }

#endregion Properties
    }
}
